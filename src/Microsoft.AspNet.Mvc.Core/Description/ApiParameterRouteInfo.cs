// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNet.Routing;

namespace Microsoft.AspNet.Mvc.Description
{
    public class ApiParameterRouteInfo
    {
        public IEnumerable<IRouteConstraint> Constraints { get; set; }

        public object DefaultValue { get; set; }

        public bool IsOptional { get; set; }
    }
}