﻿// Copyright (c) 2008-2025, Hazelcast, Inc. All Rights Reserved.
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
using Hazelcast.DistributedObjects;

namespace Hazelcast.Linq
{
    /// <summary>
    /// A key value pair for <see cref="IHMap{TKey,TValue}"/> entries on LINQ.
    /// </summary>
    /// <typeparam name="TKey">Type of Key</typeparam>
    /// <typeparam name="TValue">Type of Value</typeparam>
    public struct MapEntry<TKey, TValue>
    {
        public MapEntry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Key of the entry.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Value of the entry.
        /// </summary>
        public TValue Value { get; set; }


        /// <summary>
        /// Deconstructs the current <see cref="MapEntry{TKey,TValue}."/>
        /// </summary>
        public void Deconstruct(out TKey key, out TValue value)
        {
            key = Key;
            value = Value;
        }
    }
}
