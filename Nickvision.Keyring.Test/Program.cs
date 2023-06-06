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

            }
            else if(input == "3")
            {

            }
            else if(input == "4")
            {

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