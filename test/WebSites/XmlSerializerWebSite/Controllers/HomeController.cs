// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace XmlSerializerWebSite
{
    public class HomeController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromBody]DummyClass dummyObject)
        {
            return Content(dummyObject.SampleInt.ToString());
        }

        public IEnumerable<string> Values()
        {
            return new[] { "value1", "value2" };
        }

        public IQueryable<string> ValuesQueryable()
        {
            return Enumerable.Range(1, 2).Select(i => "value" + i).AsQueryable();
        }
    }
}