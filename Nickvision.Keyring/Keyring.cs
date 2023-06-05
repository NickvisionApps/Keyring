using System;

namespace Nickvision.Keyring;

/// <summary>
/// The Keyring object
/// </summary>
public class Keyring
{
    private readonly Store _store;
    
    /// <summary>
    /// Constructs a Keyring. The Keyring will first attempt to load the Store. If the Store doesn't exist, it will create a new Store
    /// </summary>
    /// <param name="name">The name of the Store</param>
    /// <param name="password">The password of the Store</param>
    /// <exception cref="ArgumentException">Thrown if the Store exists and the connection cannot be established</exception>
    public Keyring(string name, string password)
    {
        try
        {
            _store = Store.Load(name, password);
        }
        catch (ArgumentException e)
        {
           throw;
        }
        catch
        {
            _store = Store.Create(name, password, true);
        }
    }
}