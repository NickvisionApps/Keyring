using System;

namespace Nickvision.Keyring;
    
/// <summary>
/// A model of a credential stored in the keyring
/// </summary>    
public class Credential
{
    /// <summary>
    /// The id of the credential
    /// </summary>    
    public Guid Id { get; init; }
    /// <summary>
    /// The name of the credential
    /// </summary>    
    public string Name { get; init; }
    /// <summary>
    /// The uri of the credential
    /// </summary>    
    public Uri? Uri { get; init; }
    /// <summary>
    /// The username of the credential
    /// </summary>    
    public string? Username { get; init; }
    /// <summary>
    /// The password of the credential
    /// </summary>    
    public string? Password { get; init; }
    
    /// <summary>
    /// Constructs a Credential
    /// </summary>
    /// <param name="name">The name of the credential</param>
    /// <param name="uri">The uri of the credential</param>
    /// <param name="username">The username of the credential</param>
    /// <param name="password">The password of the credential</param>
    public Credential(string name, Uri? uri, string? username, string? password)
    {
        Id = new Guid();
        Name = name;
        Uri = uri;
        Username = username;
        Password = password;
    }
    
    /// <summary>
    /// Constructs a Credential
    /// </summary>
    /// <param name="id">The id of the credential</param>
    /// <param name="name">The name of the credential</param>
    /// <param name="uri">The uri of the credential</param>
    /// <param name="username">The username of the credential</param>
    /// <param name="password">The password of the credential</param>
    internal Credential(Guid id, string name, Uri? uri, string? username, string? password)
    {
        Id = id;
        Uri = uri;
        Name = name;
        Username = username;
        Password = password;
    }
}