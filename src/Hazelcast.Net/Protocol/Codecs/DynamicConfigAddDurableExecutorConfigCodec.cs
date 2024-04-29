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

// <auto-generated>
//   This code was generated by a tool.
//   Hazelcast Client Protocol Code Generator @c89bc95
//   https://github.com/hazelcast/hazelcast-client-protocol
//   Change to this file will be lost if the code is regenerated.
// </auto-generated>

#pragma warning disable IDE0051 // Remove unused private members
// ReSharper disable UnusedMember.Local
// ReSharper disable RedundantUsingDirective
// ReSharper disable CheckNamespace

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hazelcast.Protocol.BuiltInCodecs;
using Hazelcast.Protocol.CustomCodecs;
using Hazelcast.Core;
using Hazelcast.Messaging;
using Hazelcast.Clustering;
using Hazelcast.Serialization;
using Microsoft.Extensions.Logging;

namespace Hazelcast.Protocol.Codecs
{
    /// <summary>
    /// Adds a new durable executor configuration to a running cluster.
    /// If a durable executor configuration with the given {@code name} already exists, then
    /// the new configuration is ignored and the existing one is preserved.
    ///</summary>
#if SERVER_CODEC
    internal static class DynamicConfigAddDurableExecutorConfigServerCodec
#else
    internal static class DynamicConfigAddDurableExecutorConfigCodec
#endif
    {
        public const int RequestMessageType = 1771776; // 0x1B0900
        public const int ResponseMessageType = 1771777; // 0x1B0901
        private const int RequestPoolSizeFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestDurabilityFieldOffset = RequestPoolSizeFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestCapacityFieldOffset = RequestDurabilityFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestStatisticsEnabledFieldOffset = RequestCapacityFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestInitialFrameSize = RequestStatisticsEnabledFieldOffset + BytesExtensions.SizeOfBool;
        private const int ResponseInitialFrameSize = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// durable executor name
            ///</summary>
            public string Name { get; set; }

            /// <summary>
            /// executor thread pool size
            ///</summary>
            public int PoolSize { get; set; }

            /// <summary>
            /// executor's durability
            ///</summary>
            public int Durability { get; set; }

            /// <summary>
            /// capacity of executor tasks per partition
            ///</summary>
            public int Capacity { get; set; }

            /// <summary>
            /// name of an existing configured split brain protection to be used to determine the minimum number of members
            /// required in the cluster for the lock to remain functional. When {@code null}, split brain protection does not
            /// apply to this lock configuration's operations.
            ///</summary>
            public string SplitBrainProtectionName { get; set; }

            /// <summary>
            /// {@code true} to enable gathering of statistics, otherwise {@code false}
            ///</summary>
            public bool StatisticsEnabled { get; set; }

            /// <summary>
            /// Name of the User Code Namespace applied to this instance.
            ///</summary>
            public string UserCodeNamespace { get; set; }

            /// <summary>
            /// <c>true</c> if the statisticsEnabled is received from the client, <c>false</c> otherwise.
            /// If this is false, statisticsEnabled has the default value for its type.
            /// </summary>
            public bool IsStatisticsEnabledExists { get; set; }

            /// <summary>
            /// <c>true</c> if the userCodeNamespace is received from the client, <c>false</c> otherwise.
            /// If this is false, userCodeNamespace has the default value for its type.
            /// </summary>
            public bool IsUserCodeNamespaceExists { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(string name, int poolSize, int durability, int capacity, string splitBrainProtectionName, bool statisticsEnabled, string userCodeNamespace)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = false,
                OperationName = "DynamicConfig.AddDurableExecutorConfig"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteIntL(RequestPoolSizeFieldOffset, poolSize);
            initialFrame.Bytes.WriteIntL(RequestDurabilityFieldOffset, durability);
            initialFrame.Bytes.WriteIntL(RequestCapacityFieldOffset, capacity);
            initialFrame.Bytes.WriteBoolL(RequestStatisticsEnabledFieldOffset, statisticsEnabled);
            clientMessage.Append(initialFrame);
            StringCodec.Encode(clientMessage, name);
            CodecUtil.EncodeNullable(clientMessage, splitBrainProtectionName, StringCodec.Encode);
            CodecUtil.EncodeNullable(clientMessage, userCodeNamespace, StringCodec.Encode);
            return clientMessage;
        }

#if SERVER_CODEC
        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var request = new RequestParameters();
            var initialFrame = iterator.Take();
            request.PoolSize = initialFrame.Bytes.ReadIntL(RequestPoolSizeFieldOffset);
            request.Durability = initialFrame.Bytes.ReadIntL(RequestDurabilityFieldOffset);
            request.Capacity = initialFrame.Bytes.ReadIntL(RequestCapacityFieldOffset);
            if (initialFrame.Bytes.Length >= RequestStatisticsEnabledFieldOffset + BytesExtensions.SizeOfBool)
            {
                request.StatisticsEnabled = initialFrame.Bytes.ReadBoolL(RequestStatisticsEnabledFieldOffset);
                request.IsStatisticsEnabledExists = true;
            }
            else request.IsStatisticsEnabledExists = false;
            request.Name = StringCodec.Decode(iterator);
            request.SplitBrainProtectionName = CodecUtil.DecodeNullable(iterator, StringCodec.Decode);
            if (iterator.Current?.Next != null)
            {
                request.UserCodeNamespace = CodecUtil.DecodeNullable(iterator, StringCodec.Decode);
                request.IsUserCodeNamespaceExists = true;
            }
            else request.IsUserCodeNamespaceExists = false;
            return request;
        }
#endif

        public sealed class ResponseParameters
        {
        }

#if SERVER_CODEC
        public static ClientMessage EncodeResponse()
        {
            var clientMessage = new ClientMessage();
            var initialFrame = new Frame(new byte[ResponseInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, ResponseMessageType);
            clientMessage.Append(initialFrame);
            return clientMessage;
        }
#endif

        public static ResponseParameters DecodeResponse(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var response = new ResponseParameters();
            iterator.Take(); // empty initial frame
            return response;
        }

    }
}