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
    /// Adds a new scheduled executor configuration to a running cluster.
    /// If a scheduled executor configuration with the given {@code name} already exists, then
    /// the new configuration is ignored and the existing one is preserved.
    ///</summary>
#if SERVER_CODEC
    internal static class DynamicConfigAddScheduledExecutorConfigServerCodec
#else
    internal static class DynamicConfigAddScheduledExecutorConfigCodec
#endif
    {
        public const int RequestMessageType = 1772032; // 0x1B0A00
        public const int ResponseMessageType = 1772033; // 0x1B0A01
        private const int RequestPoolSizeFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestDurabilityFieldOffset = RequestPoolSizeFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestCapacityFieldOffset = RequestDurabilityFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestMergeBatchSizeFieldOffset = RequestCapacityFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestStatisticsEnabledFieldOffset = RequestMergeBatchSizeFieldOffset + BytesExtensions.SizeOfInt;
        private const int RequestCapacityPolicyFieldOffset = RequestStatisticsEnabledFieldOffset + BytesExtensions.SizeOfBool;
        private const int RequestInitialFrameSize = RequestCapacityPolicyFieldOffset + BytesExtensions.SizeOfByte;
        private const int ResponseInitialFrameSize = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// name of scheduled executor
            ///</summary>
            public string Name { get; set; }

            /// <summary>
            /// number of executor threads per member for the executor
            ///</summary>
            public int PoolSize { get; set; }

            /// <summary>
            /// durability of the scheduled executor
            ///</summary>
            public int Durability { get; set; }

            /// <summary>
            /// maximum number of tasks that a scheduler can have at any given point in time per partition or per node
            /// according to the capacity policy
            ///</summary>
            public int Capacity { get; set; }

            /// <summary>
            /// name of an existing configured split brain protection to be used to determine the minimum number of members
            /// required in the cluster for the lock to remain functional. When {@code null}, split brain protection does not
            /// apply to this lock configuration's operations.
            ///</summary>
            public string SplitBrainProtectionName { get; set; }

            /// <summary>
            /// Name of a class implementing SplitBrainMergePolicy that handles merging of values for this cache
            /// while recovering from network partitioning.
            ///</summary>
            public string MergePolicy { get; set; }

            /// <summary>
            /// Number of entries to be sent in a merge operation.
            ///</summary>
            public int MergeBatchSize { get; set; }

            /// <summary>
            /// {@code true} to enable gathering of statistics, otherwise {@code false}
            ///</summary>
            public bool StatisticsEnabled { get; set; }

            /// <summary>
            /// Capacity policy for the configured capacity value
            ///</summary>
            public byte CapacityPolicy { get; set; }

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
            /// <c>true</c> if the capacityPolicy is received from the client, <c>false</c> otherwise.
            /// If this is false, capacityPolicy has the default value for its type.
            /// </summary>
            public bool IsCapacityPolicyExists { get; set; }

            /// <summary>
            /// <c>true</c> if the userCodeNamespace is received from the client, <c>false</c> otherwise.
            /// If this is false, userCodeNamespace has the default value for its type.
            /// </summary>
            public bool IsUserCodeNamespaceExists { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(string name, int poolSize, int durability, int capacity, string splitBrainProtectionName, string mergePolicy, int mergeBatchSize, bool statisticsEnabled, byte capacityPolicy, string userCodeNamespace)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = false,
                OperationName = "DynamicConfig.AddScheduledExecutorConfig"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteIntL(RequestPoolSizeFieldOffset, poolSize);
            initialFrame.Bytes.WriteIntL(RequestDurabilityFieldOffset, durability);
            initialFrame.Bytes.WriteIntL(RequestCapacityFieldOffset, capacity);
            initialFrame.Bytes.WriteIntL(RequestMergeBatchSizeFieldOffset, mergeBatchSize);
            initialFrame.Bytes.WriteBoolL(RequestStatisticsEnabledFieldOffset, statisticsEnabled);
            initialFrame.Bytes.WriteByteL(RequestCapacityPolicyFieldOffset, capacityPolicy);
            clientMessage.Append(initialFrame);
            StringCodec.Encode(clientMessage, name);
            CodecUtil.EncodeNullable(clientMessage, splitBrainProtectionName, StringCodec.Encode);
            StringCodec.Encode(clientMessage, mergePolicy);
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
            request.MergeBatchSize = initialFrame.Bytes.ReadIntL(RequestMergeBatchSizeFieldOffset);
            if (initialFrame.Bytes.Length >= RequestStatisticsEnabledFieldOffset + BytesExtensions.SizeOfBool)
            {
                request.StatisticsEnabled = initialFrame.Bytes.ReadBoolL(RequestStatisticsEnabledFieldOffset);
                request.IsStatisticsEnabledExists = true;
            }
            else request.IsStatisticsEnabledExists = false;
            if (initialFrame.Bytes.Length >= RequestCapacityPolicyFieldOffset + BytesExtensions.SizeOfByte)
            {
                request.CapacityPolicy = initialFrame.Bytes.ReadByteL(RequestCapacityPolicyFieldOffset);
                request.IsCapacityPolicyExists = true;
            }
            else request.IsCapacityPolicyExists = false;
            request.Name = StringCodec.Decode(iterator);
            request.SplitBrainProtectionName = CodecUtil.DecodeNullable(iterator, StringCodec.Decode);
            request.MergePolicy = StringCodec.Decode(iterator);
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
