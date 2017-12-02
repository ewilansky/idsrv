// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
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
                        listenOptions.UseHttps("../mac.local.pfx", "pass");
                    });
                });
            }

            return builder.Build();
        }
    }
}