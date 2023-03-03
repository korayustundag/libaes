# LibAES
Simplified C# AES encryption and decryption library.
## Requirements
[.NET Framework 4.8+](https://dotnet.microsoft.com/en-us/download/dotnet-framework)
## How to use?
### Text Encryption
```csharp
string mytext = "This is my text!";
string encrypted = AesLib.EncryptText(mytext, "your password");
```
### Text Decryption
```csharp
string mytext = "*****..."; // Base64
string encrypted = AesLib.DecryptText(mytext, "your password");
```
### File Encryption
```csharp
string file = "C:\\dir\\file.docx";
string encryptedFile = "C:\\dir\\encrypted.docx";
AesLib.EncryptFile(file, encryptedFile, "your password");
```
### File Decryption
```csharp
string encryptedFile = "C:\\dir\\encrypted.docx";
string file = "C:\\dir\\file.docx";
AesLib.DecryptFile(encryptedFile, file, "your password");
```
