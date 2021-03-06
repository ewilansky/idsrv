﻿using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MvcClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "MVC Client Hybrid OIDC";

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
                    options.Listen(IPAddress.Any, 44399, listenOptions =>
                    {
                        listenOptions.UseHttps("../mac.my.pfx", "pass");
                    });
                });
            }

            return builder.Build();
        }
    }
}