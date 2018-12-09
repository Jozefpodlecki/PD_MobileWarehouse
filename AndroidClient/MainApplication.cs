﻿using System;

using Android.App;
using Android.Content;

namespace Client
{
    [Application]
    public class MainApplication : Application
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
        }
    }
}