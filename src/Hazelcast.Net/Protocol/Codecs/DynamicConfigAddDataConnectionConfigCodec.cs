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
//   Hazelcast Client Protocol Code Generator @0a5719d
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
    /// Adds a data connection configuration.
    /// If a data connection configuration with the given {@code name} already exists, then
    /// the new configuration is ignored and the existing one is preserved.
    ///</summary>
#if SERVER_CODEC
    internal static class DynamicConfigAddDataConnectionConfigServerCodec
#else
    internal static class DynamicConfigAddDataConnectionConfigCodec
#endif
    {
        public const int RequestMessageType = 1773824; // 0x1B1100
        public const int ResponseMessageType = 1773825; // 0x1B1101
        private const int RequestSharedFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestInitialFrameSize = RequestSharedFieldOffset + BytesExtensions.SizeOfBool;
        private const int ResponseInitialFrameSize = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// Name of this data connection, must be unique.
            ///</summary>
            public string Name { get; set; }

            /// <summary>
            /// Type of the data connection as specified in the DataConnectionRegistration.
            ///</summary>
            public string Type { get; set; }

            /// <summary>
            /// {@code true} if an instance of the data connection will be shared.
            /// Depending on the implementation of the data connection the shared instance may be
            /// single a thread-safe instance, or not thread-safe, but a pooled instance.
            /// {@code false} when on each usage a new instance of the underlying resource
            /// should be created.
            /// The default is {@code true}.
            ///</summary>
            public bool Shared { get; set; }

            /// <summary>
            /// Properties of the data connection configuration.
            ///</summary>
            public IDictionary<string, string> Properties { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(string name, string type, bool shared, IDictionary<string, string> properties)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = false,
                OperationName = "DynamicConfig.AddDataConnectionConfig"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteBoolL(RequestSharedFieldOffset, shared);
            clientMessage.Append(initialFrame);
            StringCodec.Encode(clientMessage, name);
            StringCodec.Encode(clientMessage, type);
            MapCodec.Encode(clientMessage, properties, StringCodec.Encode, StringCodec.Encode);
            return clientMessage;
        }

#if SERVER_CODEC
        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var request = new RequestParameters();
            var initialFrame = iterator.Take();
            request.Shared = initialFrame.Bytes.ReadBoolL(RequestSharedFieldOffset);
            request.Name = StringCodec.Decode(iterator);
            request.Type = StringCodec.Decode(iterator);
            request.Properties = MapCodec.Decode(iterator, StringCodec.Decode, StringCodec.Decode);
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
