using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRunApp.Droid
{
    public class SomeTaskRepository
    {
        public SomeTaskRepository(IExecutorService exe, Handler handler)
        {
            Exe = exe;
            Handler = handler;
        }

        private readonly IExecutorService Exe;
        private readonly Handler Handler; // for UI Update

        private bool IsRunning { get; set; } = false;
        private bool IsEnabled { get; set; } = true;


        public void StartLongTask(IRepositoryCallback callback)
        {
            if (IsRunning)
                return;

            IsEnabled = true;
            IsRunning = true;
            Exe.Execute(new Runnable(() =>
            {
                while (IsEnabled)
                {
                    // TODO : run your task
                    Thread.Sleep(1000);
                    // if you want progress, using callback
                    // callback.OnComplete(new Result<int>.Error(new Java.Lang.Exception()));
                }

                IsRunning = false;
            }));
        }

        public void StopLongTask()
        {
            IsEnabled = false;
        }
    }
}