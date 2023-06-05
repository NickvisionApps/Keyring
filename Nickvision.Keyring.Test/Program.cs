using Nickvision.Keyring;
using System;

namespace Nickvision.Keyring.Test;

public class Program
{
    public static void Main(string[] args) => new Program().MainView();
    
    private void MainView()
    {
        var isRunning = true;
        while(isRunning)
        {
            Console.Clear();
            Console.WriteLine("===Nickvision.Keyring Testing Program===");
            Console.WriteLine("1. Create/Load a Keyring");
            Console.WriteLine("2. Exit");
            Console.Write("Select an Option: ");
            var input = Console.ReadLine();
            if(input == "1")
            {
                Console.WriteLine("\n=Keyring Login=");
                Console.Write("Name: ");
                var name = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                KeyringView(name, password);
            }
            else if(input == "2")
            {
                isRunning = false;
            }
        }   
    }
    
    private void KeyringView(string name, string password)
    {
        var keyring = Keyring.Access(name, password);
        if(keyring == null)
        {
            Console.WriteLine("\nUnable to access the keyring.\nPress any key to continue...");
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