namespace ConsoleServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;

    public class Program
    {
        static public void Configure(IApplicationBuilder app)
        {
            app.Run(async (context) => {
                Console.WriteLine(context.Request.Path);

                if (context.Request.ContentType != null)
                {
                    Console.WriteLine("Content-Type: {0}", context.Request.ContentType);
                }
                Console.WriteLine("Content-Length: {0}", context.Request.ContentLength);

                string inputBody;
                using (var reader = new StreamReader(context.Request.Body, System.Text.Encoding.UTF8))
                {
                    inputBody = await reader.ReadToEndAsync();
                }

                Console.WriteLine("-----START-BODY-CONTENT-----");
                Console.WriteLine(inputBody);
                Console.WriteLine("-----END-BODY-CONTENT-----");

                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("OK");
            });
        }
        static void Main()
        {
            new WebHostBuilder().UseKestrel().Configure(Configure).UseUrls("http://+:80").Build().Run();
        }
    }
}
