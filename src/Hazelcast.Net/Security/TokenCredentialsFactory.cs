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
using System;
using System.Text;
using Hazelcast.Configuration;

namespace Hazelcast.Security
{
    /// <summary>
    /// Provides an implementation of <see cref="ICredentialsFactory"/> that returns a static token <see cref="ICredentials"/>.
    /// </summary>
    internal class TokenCredentialsFactory : StaticCredentialsFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCredentialsFactory"/>.
        /// </summary>
        /// <param name="token">A token.</param>
        public TokenCredentialsFactory(byte[] token)
            : base(new TokenCredentials(token))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCredentialsFactory"/>.
        /// </summary>
        /// <param name="token">A token.</param>
        public TokenCredentialsFactory(string token)
            : this(Encoding.UTF8.GetBytes(token))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCredentialsFactory"/>.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="encoding">Data encoding ('none' or 'base64').</param>
        public TokenCredentialsFactory(string data, string encoding)
          : this(GetTokenBytes(encoding ?? "none", data))
        { }

        private static byte[] GetTokenBytes(string encoding, string data)
        {
            if (encoding.Equals("base64", StringComparison.OrdinalIgnoreCase)) return Convert.FromBase64String(data);
            if (encoding.Equals("none", StringComparison.OrdinalIgnoreCase)) return Encoding.UTF8.GetBytes(data);
            throw new ConfigurationException($"Invalid token bytes encoding \"{encoding}\".");
        }
    }
}
