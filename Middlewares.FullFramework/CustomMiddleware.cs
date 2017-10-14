using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Owin;

namespace Middlewares.FullFramework
{
    public class CustomMiddleware: OwinMiddleware
    {
        public CustomMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            if (context.Request.Headers.TryGetValue("device", out var values))
            {
                if (values.Contains("mobile"))
                {
                    return Task.CompletedTask;
                }
            }
            return Next.Invoke(context);
        }
    }
    public static class CustomMiddlewareExtensions
    {
        public static IAppBuilder UseCustomMiddleware(this IAppBuilder builder)
        {
            return builder.Use(typeof(CustomMiddleware));
        }
    }
}