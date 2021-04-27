using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using System;
using System.Threading.Tasks;

namespace AutoRunApp.Droid
{
    // [Register("keyple.keyple.LONG_RUNNING_TASK_SERVICE")]
    [Service(Name = "com.test.BOOT_FOREGROUND_SERVICE")]
    public class BootForegroundService : Service
    {
        private static readonly string TAG = typeof(BootForegroundService).ToString();

        bool isStarted;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CheckOrCreateNotificationChannel();
            var notification = CreateNotifcation("앱이 실행중입니다.");
            StartForeground(Constants.FsId, notification);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            CheckOrCreateNotificationChannel();

            if (!isStarted)
            {
                // Nothing to do
                isStarted = true;
                if (intent.Action == Constants.FsActionBoot)
                {
                    RefreshForegroundService("앱이 재실행했습니다.");
                }
            }
            else
            {
                if (intent.Action == Constants.FsActionBoot)
                {
                    RefreshForegroundService("앱이 재실행했습니다.");
                }
                else
                {
                    RefreshForegroundService("앱이 새로고침했습니다.");
                }
            }

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {

            base.OnDestroy();
            // clean code
            //if (isStarted)
            //{
            //    var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            //    notificationManager.Cancel(1);
            //}
            isStarted = false;
        }

        private Notification CreateNotifcation(string content)
        {
            var notification = new NotificationCompat.Builder(this, Constants.FsNotificationChannelId)
               .SetSmallIcon(Resource.Drawable.notification_icon_background)
               .SetContentText(content)
               .SetVibrate(null)
               .SetContentIntent(BuildIntentToShowMainActivity())
               .SetOngoing(true)
               .SetPriority(NotificationCompat.PriorityHigh);


            return notification.Build();
        }

        private void RefreshForegroundService(string content)
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);

            var notification = new NotificationCompat.Builder(this, Constants.FsNotificationChannelId)
              .SetSmallIcon(Resource.Drawable.notification_icon_background)
              .SetContentText(content)
              .SetVibrate(null)
              .SetContentIntent(BuildIntentToShowMainActivity())
              .SetOngoing(true)
              .SetPriority(NotificationCompat.PriorityHigh)
              .Build();

            notificationManager.Notify(Constants.FsId, notification);
        }

        private void CheckOrCreateNotificationChannel()
        {
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var channel = notificationManager.GetNotificationChannel(Constants.FsNotificationChannelId);

            if (channel == default(NotificationChannel))
            {
                // Create
                channel = new NotificationChannel(Constants.FsNotificationChannelId, Constants.FsNotificationChannelName, NotificationImportance.Default);
                channel.EnableVibration(false);
                channel.SetShowBadge(false);
                channel.SetVibrationPattern(default);
                notificationManager.CreateNotificationChannel(channel);
            }
        }

        private PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(Application.Context, typeof(MainActivity));
            notificationIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);

            var pendingIntent = PendingIntent.GetActivity(Application.Context, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }
    }
}