using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using System.Threading;

namespace Nickvision.Keyring;

/// <summary>
/// A store object for credentials. Backed by SQLCipher
/// </summary>
internal class Store : IDisposable
{
    private bool _disposed;
    private SqliteConnection _database;
    
    /// <summary>
    /// Constructs a Store
    /// </summary>
    /// <param name="database">The database connection for the store</param>
    private Store(SqliteConnection database)
    {
        _disposed = false;
        _database = database;
        _database.Open();
        using var cmdTableCredentials = _database!.CreateCommand();
        cmdTableCredentials.CommandText = "CREATE TABLE IF NOT EXISTS credentials (id TEXT PRIMARY KEY, name TEXT, uri TEXT, username TEXT, password TEXT)";
        cmdTableCredentials.ExecuteNonQuery();
        _database.Close();
    }
    
    /// <summary>
    /// Creates a new Store
    /// </summary>
    /// <param name="path">The path at which to store the Store</param>
    /// <param name="password">The password to use to encrypt the Store</param>
    /// <param name="overwrite">Whether or not to overwrite an existing Store at the path</param>
    /// <exception cref="FileFormatException">Thrown if the path does not end in the .nring extension</exception>
    /// <exception cref="IOException">Thrown if a Store exists at the provided path and overwrite is false</exception>
    public static Store Create(string path, string password, bool overwrite)
    {
        if(Path.GetExtension(path) != ".nring")
        {
            throw new FileFormatException("The path must have the file extension: .nring");
        }
        if(File.Exists(path))
        {
            if(overwrite)
            {
                File.Delete(path);
            }
            else
            {
                throw new IOException("A Store already exists at the provided path.");
            }
        }
        return new Store(new SqliteConnection(new SqliteConnectionStringBuilder()
        {
            DataSource = path,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Pooling = false,
            Password = password
        }.ConnectionString));
    }
    
    /// <summary>
    /// Loads an existing Store
    /// </summary>
    /// <param name="path">The path at which to access the Store</param>
    /// <param name="password">The password to use to access the Store</param>
    /// <exception cref="FileFormatException">Thrown if the path does not end in the .nring extension</exception>
    /// <exception cref="FileNotFoundException">Thrown if a Store does not exist at the provided path</exception>
    /// <exception cref="ArgumentException">Thrown if the Store connection cannot be established</exception>
    public static Store Load(string path, string password)
    {
        if(Path.GetExtension(path) != ".nring")
        {
            throw new FileFormatException("The path must have the file extension: .nring");
        }
        if(!File.Exists(path))
        {
            throw new FileNotFoundException("A Store is not found at the provided path.");
        }
        try
        {
            return new Store(new SqliteConnection(new SqliteConnectionStringBuilder()
            {
                DataSource = path,
                Mode = SqliteOpenMode.ReadWrite,
                Pooling = false,
                Password = password
            }.ConnectionString));            
        }
        catch
        {
            throw new ArgumentException("Unable to access the Store. Make sure the path and password are correct.");
        }
    }
    
    /// <summary>
    /// Frees resources used by the Store object
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Frees resources used by the Store object
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            if (_database != null)
            {
                if(_database.State == ConnectionState.Open)
                {
                    _database.Close();   
                }
                _database.Dispose();
            }
        }
        _disposed = true;
    }

    
}