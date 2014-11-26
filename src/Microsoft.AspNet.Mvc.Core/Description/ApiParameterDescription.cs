// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Microsoft.AspNet.Mvc.Description
{
    public class ApiParameterDescription
    {
        public ModelMetadata ModelMetadata { get; set; }

        public string Name { get; set; }

        public ParameterDescriptor ParameterDescriptor { get; set; }

        public ApiParameterRouteInfo RouteInfo { get; set; }

        public ApiParameterSource Source { get; set; }

        public Type Type { get; set; }
    }
}