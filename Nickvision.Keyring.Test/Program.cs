using Nickvision.Keyring;
using System;
using System.Threading.Tasks;

namespace Nickvision.Keyring.Test;

public class Program
{
    public static async Task Main(string[] args) => await new Program().MainViewAsync();
    
    private async Task MainViewAsync()
    {
        var isRunning = true;
        while(isRunning)
        {
            Console.Clear();
            Console.WriteLine("===Nickvision.Keyring Testing Program===");
            Console.WriteLine("1. Access a keyring");
            Console.WriteLine("\t-> Creates a keyring if one with the name doesn't exist, otherwise logs in to the keyring with the provided name");
            Console.WriteLine("2. Check if keyring exists");
            Console.WriteLine("3. Exit");
            Console.Write("Select an Option: ");
            var input = Console.ReadLine();
            if(input == "1")
            {
                Console.Write("\nName: ");
                var name = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                await KeyringViewAsync(name, password);
            }
            else if(input == "2")
            {
                Console.Write("\nName: ");
                var name = Console.ReadLine();
                Console.WriteLine(Keyring.Exists(name) ? "The keyring exists." : "The keyring does not exist.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if(input == "3")
            {
                isRunning = false;
            }
        }   
    }
    
    private async Task KeyringViewAsync(string name, string password)
    {
        var keyring = Keyring.Access(name, password);
        if(keyring == null)
        {
            Console.WriteLine("Unable to access the keyring.\n\nPress any key to continue...");
            Console.ReadKey();
            return;
        }
        var isRunning = true;
        while(isRunning)
        {
            Console.Clear();
            Console.WriteLine($"===Keyring: {keyring.Name}===");
            Console.WriteLine("1. List all credentials");
            Console.WriteLine("2. Add new credential");
            Console.WriteLine("3. Edit credential");
            Console.WriteLine("4. Delete credential");
            Console.WriteLine("5. Destroy keyring");
            Console.WriteLine("6. Logout");
            Console.Write("Select an Option: ");
            var input = Console.ReadLine();
            if(input == "1")
            {
                var credentials = await keyring.GetAllCredentialsAsync();
                Console.Clear();
                Console.WriteLine($"===Credentials in {keyring.Name}===");
                foreach(var credential in credentials)
                {
                    Console.WriteLine($"{credential.Name}: {credential.Id} => {credential.Uri} = {credential.Username} | {credential.Password}");
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if(input == "2")
            {
                Console.Write("\nName (Required): ");
                var credentialName = Console.ReadLine();
                while(string.IsNullOrEmpty(credentialName))
                {
                    Console.Write("Name (Required): ");
                    credentialName = Console.ReadLine();
                }
                Console.Write("Uri (Optional): ");
                var credentialUri = Console.ReadLine();
                Uri? credentialUriReal = null;
                if(!string.IsNullOrEmpty(credentialUri))
                {
                    try
                    {
                        credentialUriReal = new Uri(credentialUri);
                    }
                    catch
                    {
                        Console.WriteLine("Uri format invalid. Skipping...");
                    }
                }
                Console.Write("Username (Optional): ");
                var credentialUsername = Console.ReadLine();
                Console.Write("Password (Optional): ");
                var credentialPassword = Console.ReadLine();
                await keyring.AddCredentialAsync(new Credential(credentialName, credentialUriReal, credentialUsername, credentialPassword));
            }
            else if(input == "3")
            {
                Console.Write("\nId: ");
                var id = Console.ReadLine();
                int? idReal = null;
                if(!string.IsNullOrEmpty(id))
                {
                    try
                    {
                        idReal = int.Parse(id);
                    }
                    catch { }
                }
                if(idReal == null)
                {
                    Console.WriteLine("No credential found with the provided id.\n\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Credential? credential = await keyring.LookupCredentialAsync(idReal.Value);
                    if(credential == null)
                    {
                        Console.WriteLine("No credential found with the provided id.\n\nPress any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("\nNew Name (Required): ");
                        var credentialName = Console.ReadLine();
                        while(string.IsNullOrEmpty(credentialName))
                        {
                            Console.Write("New Name (Required): ");
                            credentialName = Console.ReadLine();
                        }
                        Console.Write("New Uri (Optional): ");
                        var credentialUri = Console.ReadLine();
                        Uri? credentialUriReal = null;
                        if(!string.IsNullOrEmpty(credentialUri))
                        {
                            try
                            {
                                credentialUriReal = new Uri(credentialUri);
                            }
                            catch
                            {
                                Console.WriteLine("Uri format invalid. Skipping...");
                            }
                        }
                        Console.Write("New Username (Optional): ");
                        var credentialUsername = Console.ReadLine();
                        Console.Write("New Password (Optional): ");
                        var credentialPassword = Console.ReadLine();
                        credential.Name = credentialName;
                        credential.Uri = credentialUriReal;
                        credential.Username = credentialUsername;
                        credential.Password = credentialPassword;
                        await keyring.UpdateCredentialAsync(credential);
                    }
                }
            }
            else if(input == "4")
            {
                Console.Write("\nId: ");
                var id = Console.ReadLine();
                int? idReal = null;
                if(!string.IsNullOrEmpty(id))
                {
                    try
                    {
                        idReal = int.Parse(id);
                    }
                    catch { }
                }
                if(idReal == null)
                {
                    Console.WriteLine("No credential found with the provided id.\n\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Credential? credential = await keyring.LookupCredentialAsync(idReal.Value);
                    if(credential == null)
                    {
                        Console.WriteLine("No credential found with the provided id.\n\nPress any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write($"Are you sure you want to delete credential: {credential.Name}? (y/n) ");
                        var response = Console.ReadLine().ToLower();
                        if(response == "y" || response == "yes")
                        {
                            await keyring.DeleteCredentialAsync(credential.Id);
                        }
                    }
                }
            }
            else if(input == "5")
            {
                keyring.Destroy();
                isRunning = false;
            }
            else if(input == "6")
            {
                isRunning = false;
            }
        }
    }
}