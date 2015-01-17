using System;

namespace Microsoft.AspNet.Mvc
{
    public interface IWrapperProvider
    {
        bool TryGetWrappingType(Type originalType, out Type wrappingType);

        object Wrap(Type declaredType, object obj);

        object Unwrap(object obj);
    }
}