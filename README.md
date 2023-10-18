# How To

1. Clone the repo or download the source.

2. Find your local IP (CMD > ipconfig > ipv4 [192.168.0.??])

3. Edit this string with your address:
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
      // Replace with your local ip and username/pass to login with SSH
      SendRemoteShutdown("192.168.0.XX", "USERNAME", "PASSWORD");
}
```
4. Build for release or keep it in Debug and hit F5.

---
# Bixby Support?

Yes! Open your Bixby app and locate the 'Quick Commands' tab, from there add a new command, for example 'Shutdown My Computer', the following action would be 'Open RSD'. You can add as many different varients of the command as you need or don't use Bixby at all.

---
# Doesn't Work?

[How To Install SSH On Windows 10/11](https://www.howtogeek.com/336775/how-to-enable-and-use-windows-10s-built-in-ssh-commands/)

You will want to make sure SSH Server is installed and starts when your computer starts, you can do this by opening Services and finding 'OpenSSH SSH Server', right-click, properties and change 'Startup type' to 'Automatic'.

---
# Can I Contribute?
Yes, you're free to repurpose the source, contribute, fork or do whatever you need with it.
