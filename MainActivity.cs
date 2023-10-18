
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using Renci.SshNet;
using System;
using System.Threading.Tasks;

/* 
 * _=================================================================_
 * | The code is super basic and can be optimised further.           |
 * | I will leave it as is, feel free to modify and improve.         |
 * | Ensure changes are made on a seperate branch from main.         |
 * |                                                                 |
 * | If not working, ensure OpenSSH Server and Client are installed  |
 * | Make sure win10 account has password.                           |
 * ===================================================================
 */

namespace Remote_Shutdown
{
    [Activity(Label = "RSD", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Initiate shutdown.
            // Replace with your local ip and username/pass to login with SSH.
            SendRemoteShutdown("192.168.0.XX", "USERNAME", "PASSWORD");

            base.OnCreate(savedInstanceState);
        }

        private async void SendRemoteShutdown(string ip, string name, string password)
        {
            // Connect via SSH and execute a force shutdown.
            using (SshClient client = new SshClient(ip, name, password))
            {
                try
                {
                    client.Connect();
                }
                catch (Exception e)
                {
                    // Notify of failure.
                    SendNotification("Shutdown failed!",
                                     e.Message);

                    // Allow notifications to send.
                    await Task.Delay(50);

                    KillApplication();
                    return;
                }

                using SshCommand cmd = client.CreateCommand("shutdown /p /f");
                cmd.Execute();

                client.Disconnect();
            }

            // Success, notify.
            SendNotification("PC Shutdown!",
                             "Your computer has been shutdown!");

            // Allow notifications to send.
            await Task.Delay(50);

            KillApplication();
        }

        /// <summary>
        ///  Kill the app.
        /// </summary>
        private void KillApplication()
        {
            FinishAndRemoveTask();
            Process.KillProcess(Process.MyPid());
        }

        /// <summary>
        /// Send a notification (single-use as app should terminate after).
        /// </summary>
        private void SendNotification(string title,
                                      string description)
        {
            // Config.
            int notification_ID = 1100;
            string channel_ID = "RSD";

            // Obtain the notif manager.
            NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);

            // Register the application channel.
            manager.CreateNotificationChannel(new NotificationChannel(channel_ID, "RSD_NOTIFICATION", NotificationImportance.High));

            // Build the notification.
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channel_ID)
                                                 .SetContentTitle(title)
                                                 .SetContentText(description)
                                                 .SetSmallIcon(Resource.Drawable.icon);

            // Push the notification.
            manager.Notify(notification_ID, builder.Build());
        }
    }
}