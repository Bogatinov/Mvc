// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.Mvc
{
    /// <summary>
    /// Helper class to serialize <see cref="IEnumerable{T}"/> types by delegating them through a concrete implementation."/>.
    /// </summary>
    /// <typeparam name="T">The interface implementing <see cref="IEnumerable{T}"/> to proxy.</typeparam>
    public sealed class DelegatingEnumerable<T> : IEnumerable<T>
    {
        private IEnumerable<T> _source;

        /// <summary>
        /// Initialize a DelegatingEnumerable.
        /// </summary>
        public DelegatingEnumerable()
        {
            _source = Enumerable.Empty<T>();
        }

        /// <summary>
        /// Initialize a DelegatingEnumerable with an <see cref="IEnumerable{T}"/>. This is a helper class to proxy <see cref="IEnumerable{T}"/> interfaces for <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}"/> instance to get the enumerator from.</param>
        public DelegatingEnumerable(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            _source = source;
        }

        /// <summary>
        /// Get the enumerator of the associated <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>The enumerator of the <see cref="IEnumerable{T}"/> source.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        /// <summary>
        /// This method is not implemented but is required method for serialization to work. Do not use.
        /// </summary>
        /// <param name="item">The item to add. Unused.</param>
        public void Add(object item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the enumerator of the associated <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>The enumerator of the <see cref="IEnumerable{T}"/> source.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _source.GetEnumerator();
        }
    }
}