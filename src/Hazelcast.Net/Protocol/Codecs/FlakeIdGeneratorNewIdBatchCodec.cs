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
    /// Fetches a new batch of ids for the given flake id generator.
    ///</summary>
#if SERVER_CODEC
    internal static class FlakeIdGeneratorNewIdBatchServerCodec
#else
    internal static class FlakeIdGeneratorNewIdBatchCodec
#endif
    {
        public const int RequestMessageType = 1835264; // 0x1C0100
        public const int ResponseMessageType = 1835265; // 0x1C0101
        private const int RequestBatchSizeFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestInitialFrameSize = RequestBatchSizeFieldOffset + BytesExtensions.SizeOfInt;
        private const int ResponseBaseFieldOffset = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;
        private const int ResponseIncrementFieldOffset = ResponseBaseFieldOffset + BytesExtensions.SizeOfLong;
        private const int ResponseBatchSizeFieldOffset = ResponseIncrementFieldOffset + BytesExtensions.SizeOfLong;
        private const int ResponseInitialFrameSize = ResponseBatchSizeFieldOffset + BytesExtensions.SizeOfInt;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// Name of the flake id generator.
            ///</summary>
            public string Name { get; set; }

            /// <summary>
            /// Number of ids that will be fetched on one call.
            ///</summary>
            public int BatchSize { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(string name, int batchSize)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = true,
                OperationName = "FlakeIdGenerator.NewIdBatch"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteIntL(RequestBatchSizeFieldOffset, batchSize);
            clientMessage.Append(initialFrame);
            StringCodec.Encode(clientMessage, name);
            return clientMessage;
        }

#if SERVER_CODEC
        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var request = new RequestParameters();
            var initialFrame = iterator.Take();
            request.BatchSize = initialFrame.Bytes.ReadIntL(RequestBatchSizeFieldOffset);
            request.Name = StringCodec.Decode(iterator);
            return request;
        }
#endif

        public sealed class ResponseParameters
        {

            /// <summary>
            /// First id in the batch.
            ///</summary>
            public long Base { get; set; }

            /// <summary>
            /// Increment for the next id in the batch.
            ///</summary>
            public long Increment { get; set; }

            /// <summary>
            /// Number of ids in the batch.
            ///</summary>
            public int BatchSize { get; set; }
        }

#if SERVER_CODEC
        public static ClientMessage EncodeResponse(long @base, long increment, int batchSize)
        {
            var clientMessage = new ClientMessage();
            var initialFrame = new Frame(new byte[ResponseInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, ResponseMessageType);
            initialFrame.Bytes.WriteLongL(ResponseBaseFieldOffset, @base);
            initialFrame.Bytes.WriteLongL(ResponseIncrementFieldOffset, increment);
            initialFrame.Bytes.WriteIntL(ResponseBatchSizeFieldOffset, batchSize);
            clientMessage.Append(initialFrame);
            return clientMessage;
        }
#endif

        public static ResponseParameters DecodeResponse(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var response = new ResponseParameters();
            var initialFrame = iterator.Take();
            response.Base = initialFrame.Bytes.ReadLongL(ResponseBaseFieldOffset);
            response.Increment = initialFrame.Bytes.ReadLongL(ResponseIncrementFieldOffset);
            response.BatchSize = initialFrame.Bytes.ReadIntL(ResponseBatchSizeFieldOffset);
            return response;
        }

    }
}
