﻿using System;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Tests.TestConsole.Settings;

namespace BindOpen.Framework.Tests.TestConsole.Services
{
    public class TestService : TAppService<AppConfiguration>, ITAppHosted<AppConfiguration>
    {
        private TestAppSettings Settings { get => Host?.Settings as TestAppSettings; }

        public ITAppHost<AppConfiguration> Host { get; set; }

        public override ITAppService<AppConfiguration> Start(ILog log = null)
        {
            base.Start(log);

            Console.WriteLine("Test service: Hello, I'm working");
            Console.WriteLine("Settings:" + this.Settings?.TestFolderPath);

            return this;
        }
    }
}