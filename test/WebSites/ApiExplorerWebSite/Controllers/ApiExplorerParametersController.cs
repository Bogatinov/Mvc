﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Mvc;

namespace ApiExplorerWebSite.Controllers
{
    [Route("ApiExplorerParameters/[action]")]
    public class ApiExplorerParametersController : Controller
    {
        public void SimpleParameters(int i, string s)
        {
        }

        public void SimpleParametersWithBinderMetadata([FromQuery] int i, [FromRoute] string s)
        {
        }

        public void Product([ModelBinder] Product p)
        {
        }
    }
}