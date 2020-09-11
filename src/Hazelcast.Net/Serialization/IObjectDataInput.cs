// Copyright (c) 2008-2020, Hazelcast, Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Hazelcast.Serialization
{
    /// <summary>
    /// Extends <see cref="IDataInput"/> with support for <see cref="IData"/> and objects.
    /// </summary>
    public interface IObjectDataInput : IDataInput
    {
        /// <summary>
        /// Reads an <see cref="IData"/> instance.
        /// </summary>
        /// <returns>The <see cref="IData"/> instance.</returns>
        IData ReadData();

        /// <summary>
        /// Reads an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>The object.</returns>
        T ReadObject<T>();
    }
}
