using System;
using Android.App;
using Android.Content;
using Java.Lang;

namespace Client
{
    [Application]
    public class MainApplication : Application, Java.Lang.Thread.IUncaughtExceptionHandler
    {
        public MainApplication(
            IntPtr javaReference,
            Android.Runtime.JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Java.Lang.Thread.DefaultUncaughtExceptionHandler = this;
        }

        public void UncaughtException(Thread thread, Throwable exception)
        {
            
        }
    }
}