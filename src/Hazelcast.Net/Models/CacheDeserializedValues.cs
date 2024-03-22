﻿// Copyright (c) 2008-2024, Hazelcast, Inc. All Rights Reserved.
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

using Hazelcast.Core;

namespace Hazelcast.Models;

/// <summary>
/// Defines the caching control of de-serialized values.
/// </summary>
public enum CacheDeserializedValues
{
    /// <summary>
    /// Never cache de-serialized values.
    /// </summary>
    [Enums.JavaName("NEVER")] Never,

    /// <summary>
    /// Cache values only when using search indexes.
    /// </summary>
    [Enums.JavaName("INDEX_ONLY")] IndexOnly,

    /// <summary>
    /// Always cache de-serialized values.
    /// </summary>
    [Enums.JavaName("ALWAYS")] Always,
}