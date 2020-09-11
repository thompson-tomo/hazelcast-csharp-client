﻿// Copyright (c) 2008-2020, Hazelcast, Inc. All Rights Reserved.
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
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hazelcast.Tests.Networking
{
    [TestFixture]
    [Category("enterprise")]
    public class ClientSslMutualAuthTests : ClientSslTestBase
    {
        [Test]
        public async Task TestSSLEnabled_mutualAuthRequired_Server1KnowsClient1()
        {
            await using var client = await Setup(Resources.Cluster_MA_Required,
                true,
                true,
                null,
                null,
                null,
                Resources.Cert_Client1,
                Password);

            await client.StartAsync(); // succeeds
        }

        [Test]
        public async Task TestSSLEnabled_mutualAuthRequired_Server1KnowsClient1_clientDoesNotProvideCerts()
        {
            await using var client = await Setup(Resources.Cluster_MA_Required,
                true,
                true,
                null,
                null,
                null,
                null,
                null,
                true);

            Assert.ThrowsAsync<InvalidOperationException>(() => client.StartAsync());
        }

        [Test]
        public async Task TestSSLEnabled_mutualAuthRequired_Server1NotKnowsClient2()
        {
            await using var client = await Setup(Resources.Cluster_MA_Required,
                true,
                true,
                null,
                null,
                null,
                Resources.Cert_Client2,
                Password,
                true);

            Assert.ThrowsAsync<InvalidOperationException>(() => client.StartAsync());
        }

        [Test]
        public async Task TestSSLEnabled_mutualAuthOptional_Server1KnowsClient1()
        {
            await using var client = await Setup(Resources.Cluster_MA_Optional,
                true,
                true,
                null,
                null,
                null,
                Resources.Cert_Client1,
                Password);

            await client.StartAsync(); // succeeds
        }

        [Test]
        public async Task TestSSLEnabled_mutualAuthOptional_Server1KnowsClient1_clientDoesNotProvideCerts()
        {
            await using var client = await Setup(Resources.Cluster_MA_Optional,
                true,
                true,
                null,
                null,
                null,
                null,
                null);

            await client.StartAsync(); // succeeds
        }

        [Test]
        public async Task TestSSLEnabled_mutualAuthOptional_Server1NotKnowsClient2()
        {
            await using var client = await Setup(Resources.Cluster_MA_Optional,
                true,
                true,
                null,
                null,
                null,
                Resources.Cert_Client2,
                Password,
                true);

            Assert.ThrowsAsync<InvalidOperationException>(() => client.StartAsync());
        }

        [Test]
        public async Task TestSSLEnabled_mutualAuthDisabled_Client1()
        {
            await using var client = await Setup(Resources.Cluster_Ssl_Signed,
                true,
                true,
                null,
                null,
                null,
                Resources.Cert_Client1,
                Password);

            await client.StartAsync(); // succeeds
        }
    }
}