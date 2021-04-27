using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRunApp.Droid
{
    [BroadcastReceiver(Name = "com.test.BOOT_RECEIVER",
         Enabled = true,
         Exported = false)]
    [IntentFilter(new[] {
        Intent.ActionBootCompleted,
        "android.intent.action.QUICKBOOT_POWERON" },
         Categories = new[] { Intent.CategoryDefault })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();

            Intent it = new Intent(context, typeof(BootForegroundService));
            it.SetAction(Constants.FsActionBoot);
            context.StartForegroundService(it);
        }
    }
}