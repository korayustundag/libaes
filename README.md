# LibAES
Simplified C# AES encryption and decryption library.
## Requirements
[.NET Framework 4.8+](https://dotnet.microsoft.com/en-us/download/dotnet-framework)
## How to use?
### Encryption
```csharp
string mytext = "This is my text!";
string encrypted = AesLib.EncryptText(mytext, "your password");
```
### Decryption
```csharp
string mytext = "*****..."; // Base64
string encrypted = AesLib.DecryptText(mytext, "your password");
```
## Simple App
```csharp
using System;
using System.IO;
using libAES;

namespace DemoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Encrypt text and save to file
            Console.Write("Enter Text:");
            string mytext = Console.ReadLine();
            Console.Write("Password:");
            string password = Console.ReadLine();
            File.WriteAllText("C:\\path\\encryptedTextFile.txt", AesLib.EncryptText(mytext, password));

            // Read inside file and decrypt text
            string encryptedText = File.ReadAllText("C:\\path\\encryptedTextFile.txt");
            Console.Write("Password:");
            password = Console.ReadLine();
            Console.WriteLine(AesLib.DecryptText(encryptedText, password));

            Console.ReadLine();
        }
    }
}
```
