$ErrorActionPreference = "Stop"
Invoke-Expression "dotnet build"
Invoke-Expression "dotnet publish BarcodeScanner -o artifacts"
