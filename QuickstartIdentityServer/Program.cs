﻿using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace QuickstartIdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer";

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
                        listenOptions.UseHttps("../mac.my.pfx", "pass");
                    });
                });
            }

            return builder.Build();
        }
    }
}