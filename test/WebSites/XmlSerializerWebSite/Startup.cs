// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace XmlSerializerWebSite
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var configuration = app.GetTestConfiguration();

            // Set up application services
            app.UseServices(services =>
            {
                // Add MVC services to the services container
                services.AddMvc(configuration);

                services.Configure<MvcOptions>(options =>
                    {
                        options.InputFormatters.Clear();
                        options.InputFormatters.Insert(0, new XmlSerializerInputFormatter());

                        options.OutputFormatters.Clear();
                        var xmlSerializerOutputFormatter = new XmlSerializerOutputFormatter();
                        xmlSerializerOutputFormatter.SupportedMediaTypes.Clear();
                        xmlSerializerOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml-xmlser"));
                        xmlSerializerOutputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml-xmlser"));
                        xmlSerializerOutputFormatter.WrapperProviders.Add(new EnumerableWrapperProvider());
                        options.OutputFormatters.Add(xmlSerializerOutputFormatter);
                    });
            });

            app.UseErrorReporter();

            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute("ActionAsMethod", "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" });

            });
        }
    }
}
