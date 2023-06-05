using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    /// <summary>
    /// Lookups a credential by id
    /// </summary>
    /// <param name="id">The id of the credential</param>
    /// <returns>The Credential object if found, else null</returns>
    public async Task<Credential?> LookupCredentialAsync(int id) => await _store.LookupCredentialAsync(id);

    /// <summary>
    /// Lookups credentials by name
    /// </summary>
    /// <param name="name">The name of the credentials to find</param>
    /// <returns>The list of Credential objects found</returns>
    public async Task<List<Credential>> LookupCredentialsAsync(string name) => await _store.LookupCredentialsAsync(name);

    /// <summary>
    /// Adds a Credential to the Keyring
    /// </summary>
    /// <param name="credential">The Credential object to add</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> AddCredentialAsync(Credential credential) => await _store.AddCredentialAsync(credential);

    /// <summary>
    /// Updates a Credential in the Keyring
    /// </summary>
    /// <param name="credential">The Credential object to update</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> UpdateCredentialAsync(Credential credential) => await _store.UpdateCredentialAsync(credential);

    /// <summary>
    /// Removes a Credential from the Keyring
    /// </summary>
    /// <param name="id">The id of the credential to remove</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> DeleteCredentialAsync(int id) => await _store.DeleteCredentialAsync(id);
}