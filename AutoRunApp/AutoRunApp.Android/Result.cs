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
    public abstract class Result<T>
    {
        private Result() { }

        public class Success : Result<T>
        {
            public T Data;
            public Success(T data)
            {
                Data = data;
            }
        }

        public class Error : Result<T>
        {
            public Java.Lang.Exception Exception;
            public Error(Java.Lang.Exception exception)
            {
                Exception = exception;
            }
        }
    }
}