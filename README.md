# NewsAPI


**appsettings.json structure:**
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "mySuperSecretKeyThatIsAtLeast32CharsLong!",
    "Issuer": "https://github.com/Luka-Pervan/NewsAPI",
    "Audience": "https://github.com/Luka-Pervan/NewsAPI",
    "ExpiresInMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "YOUR CONNECTION TO DATABASE"
  },
  "AllowedHosts": "*"
}
