using Nickvision.Keyring.Controllers;
using Nickvision.Keyring.Models;
using System.Threading.Tasks;

namespace Nickvision.Keyring.Controls;

/// <summary>
/// An interface of a KeyringDialog
/// </summary>
public interface IKeyringDialog
{
    /// <summary>
    /// Occurs when the enable switch is toggled
    /// </summary>
    protected Task ToggleEnableAsync();
    /// <summary>
    /// Loads the Home page
    /// </summary>
    protected Task LoadHomePageAsync();
    /// <summary>
    /// Loads the AddCredential page
    /// </summary>
    protected void LoadAddCredentialPage();
    /// <summary>
    /// Loads the EditCredential page
    /// </summary>
    /// <param name="credential">The Credential model</param>
    protected void LoadEditCredentialPage(Credential credential);
    /// <summary>
    /// Adapts the UI to the current validation status
    /// </summary>
    /// <param name="checkStatus">CredentialCheckStatus</param>
    protected void SetValidation(CredentialCheckStatus checkStatus);
    /// <summary>
    /// Occurs when the add button is clicked
    /// </summary>
    protected void OnAddCredential();
    /// <summary>
    /// Occurs when the apply button is clicked
    /// </summary>
    protected void OnEditCredential();
    /// <summary>
    /// Occurs when the delete button is clicked
    /// </summary>
    protected void OnDeleteCredential();
}