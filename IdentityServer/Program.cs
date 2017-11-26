﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            string launch = Environment.GetEnvironmentVariable("LAUNCH_PROFILE");

            var builder = WebHost.CreateDefaultBuilder(args);
            builder.UseStartup<Startup>();

            if (launch == "Kestrel")
            {
                builder.UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 44304, listenOptions =>
                    {
                        listenOptions.UseHttps("../mini.local.pfx", "pass");
                    });
                });
            }

            return builder.Build();
        }
    }
}
