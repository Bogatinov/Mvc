// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.TestHost;
using Xunit;

namespace Microsoft.AspNet.Mvc.FunctionalTests
{
    public class XmlSerializerOutputFormatterTest
    {
        private readonly IServiceProvider _services = TestHelper.CreateServices("XmlSerializerWebSite");
        private readonly Action<IApplicationBuilder> _app = new XmlSerializerWebSite.Startup().Configure;

        [Fact]
        public async Task XmlSerializerOutputFormatter_CanWriteIEnumerable()
        {
            // Arrange
            var server = TestServer.Create(_services, _app);
            var client = server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Home/Values");
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/xml-xmlser; charset=utf-8"));

            // Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                         "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><string>value1</string>" +
                         "<string>value2</string></ArrayOfString>",
                         result);
        }

        [Fact]
        public async Task XmlSerializerOutputFormatter_CanWriteIQueryable()
        {
            // Arrange
            var server = TestServer.Create(_services, _app);
            var client = server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Home/ValuesQueryable");
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/xml-xmlser; charset=utf-8"));

            // Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                         "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><string>value1</string>" +
                         "<string>value2</string></ArrayOfString>",
                         result);
        }
    }
}