// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;

namespace Microsoft.AspNet.Mvc.Description
{
    [DebuggerDisplay("Source: {Id}")]
    public class ApiParameterSource : IEquatable<ApiParameterSource>
    {
        public static readonly ApiParameterSource Body = new ApiParameterSource("Body");

        public static readonly ApiParameterSource Header = new ApiParameterSource("Header");

        public static readonly ApiParameterSource Hidden = new ApiParameterSource("Hidden");

        public static readonly ApiParameterSource ModelBinding = new ApiParameterSource("ModelBinding");

        public static readonly ApiParameterSource Path = new ApiParameterSource("Path");

        public static readonly ApiParameterSource Query = new ApiParameterSource("Query");

        public static readonly ApiParameterSource Unknown = new ApiParameterSource("Unknown");

        public ApiParameterSource([NotNull] string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public bool Equals(ApiParameterSource other)
        {
            return other == null ? false : string.Equals(other.Id, Id, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ApiParameterSource);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(ApiParameterSource s1, ApiParameterSource s2)
        {
            if (object.ReferenceEquals(s1, null))
            {
                return object.ReferenceEquals(s2, null);;
            }

            return s1.Equals(s2);
        }

        public static bool operator !=(ApiParameterSource s1, ApiParameterSource s2)
        {
            return !(s1 == s2);
        }
    }
}