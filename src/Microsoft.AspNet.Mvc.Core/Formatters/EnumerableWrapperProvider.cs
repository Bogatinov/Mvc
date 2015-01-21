// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Microsoft.AspNet.Mvc
{
    public class EnumerableWrapperProvider : IWrapperProvider
    {
        private static readonly ConcurrentDictionary<Type, Type> _delegatingEnumerableCache = new ConcurrentDictionary<Type, Type>();
        private static ConcurrentDictionary<Type, ConstructorInfo> _delegatingEnumerableConstructorCache = new ConcurrentDictionary<Type, ConstructorInfo>();

        /// <inheritdoc />
        public bool TryGetWrappingType(Type originalType, out Type wrappingType)
        {
            return TryGetDelegatingTypeForIEnumerableGenericOrSame(originalType, out wrappingType);
        }

        /// <inheritdoc />
        public object Unwrap(Type declaredType, object obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object Wrap(Type declaredType, object obj)
        {
            Type delegatingType = null;
            if (TryGetDelegatingTypeForIEnumerableGenericOrSame(declaredType, out delegatingType))
            {
                return GetTypeRemappingConstructor(delegatingType).Invoke(new object[] { obj });
            }
            return obj;

        }

        private static bool TryGetDelegatingTypeForIEnumerableGenericOrSame(Type originalType, out Type wrappingType)
        {
            return TryGetDelegatingType(FormattingUtilities.EnumerableInterfaceGenericType, originalType, out wrappingType);
        }

        private static bool TryGetDelegatingType(Type interfaceType, Type originalType, out Type wrappingType)
        {
            wrappingType = null;
            if (originalType != null && originalType.IsInterface() && originalType.IsGenericType())
            {
                Type genericType = originalType.ExtractGenericInterface(interfaceType);

                if (genericType != null)
                {
                    wrappingType = GetOrAddDelegatingType(originalType, genericType);
                    return true;
                }
            }

            return false;
        }

        private static Type GetOrAddDelegatingType(Type originalType, Type genericType)
        {
            return _delegatingEnumerableCache.GetOrAdd(
                originalType,
                (typeToRemap) =>
                {
                    // This retrieves the T type of the IEnumerable<T> interface.
                    Type elementType = genericType.GetGenericArguments()[0];
                    Type delegatingType = FormattingUtilities.DelegatingEnumerableGenericType.MakeGenericType(elementType);
                    ConstructorInfo delegatingConstructor = delegatingType.GetConstructor(new Type[] { FormattingUtilities.EnumerableInterfaceGenericType.MakeGenericType(elementType) });
                    _delegatingEnumerableConstructorCache.TryAdd(delegatingType, delegatingConstructor);

                    return delegatingType;
                });
        }

        private static ConstructorInfo GetTypeRemappingConstructor(Type type)
        {
            ConstructorInfo constructorInfo;
            _delegatingEnumerableConstructorCache.TryGetValue(type, out constructorInfo);
            return constructorInfo;
        }
    }
}