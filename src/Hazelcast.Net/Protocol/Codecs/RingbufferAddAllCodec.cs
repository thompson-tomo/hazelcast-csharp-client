// Copyright (c) 2008-2021, Hazelcast, Inc. All Rights Reserved.
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
//     Hazelcast Client Protocol Code Generator
//     https://github.com/hazelcast/hazelcast-client-protocol
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
    /// Adds all the items of a collection to the tail of the Ringbuffer. A addAll is likely to outperform multiple calls
    /// to add(Object) due to better io utilization and a reduced number of executed operations. If the batch is empty,
    /// the call is ignored. When the collection is not empty, the content is copied into a different data-structure.
    /// This means that: after this call completes, the collection can be re-used. the collection doesn't need to be serializable.
    /// If the collection is larger than the capacity of the ringbuffer, then the items that were written first will be
    /// overwritten. Therefor this call will not block. The items are inserted in the order of the Iterator of the collection.
    /// If an addAll is executed concurrently with an add or addAll, no guarantee is given that items are contiguous.
    /// The result of the future contains the sequenceId of the last written item
    ///</summary>
#if SERVER_CODEC
    internal static class RingbufferAddAllServerCodec
#else
    internal static class RingbufferAddAllCodec
#endif
    {
        public const int RequestMessageType = 1509376; // 0x170800
        public const int ResponseMessageType = 1509377; // 0x170801
        private const int RequestOverflowPolicyFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestInitialFrameSize = RequestOverflowPolicyFieldOffset + BytesExtensions.SizeOfInt;
        private const int ResponseResponseFieldOffset = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;
        private const int ResponseInitialFrameSize = ResponseResponseFieldOffset + BytesExtensions.SizeOfLong;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// Name of the Ringbuffer
            ///</summary>
            public string Name { get; set; }

            /// <summary>
            /// the batch of items to add
            ///</summary>
            public IList<IData> ValueList { get; set; }

            /// <summary>
            /// the overflowPolicy to use
            ///</summary>
            public int OverflowPolicy { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(string name, ICollection<IData> valueList, int overflowPolicy)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = false,
                OperationName = "Ringbuffer.AddAll"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteIntL(RequestOverflowPolicyFieldOffset, overflowPolicy);
            clientMessage.Append(initialFrame);
            StringCodec.Encode(clientMessage, name);
            ListMultiFrameCodec.Encode(clientMessage, valueList, DataCodec.Encode);
            return clientMessage;
        }

#if SERVER_CODEC
        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var request = new RequestParameters();
            var initialFrame = iterator.Take();
            request.OverflowPolicy = initialFrame.Bytes.ReadIntL(RequestOverflowPolicyFieldOffset);
            request.Name = StringCodec.Decode(iterator);
            request.ValueList = ListMultiFrameCodec.Decode(iterator, DataCodec.Decode);
            return request;
        }
#endif

        public sealed class ResponseParameters
        {

            /// <summary>
            /// the CompletionStage to synchronize on completion.
            ///</summary>
            public long Response { get; set; }
        }

#if SERVER_CODEC
        public static ClientMessage EncodeResponse(long response)
        {
            var clientMessage = new ClientMessage();
            var initialFrame = new Frame(new byte[ResponseInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, ResponseMessageType);
            initialFrame.Bytes.WriteLongL(ResponseResponseFieldOffset, response);
            clientMessage.Append(initialFrame);
            return clientMessage;
        }
#endif

        public static ResponseParameters DecodeResponse(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var response = new ResponseParameters();
            var initialFrame = iterator.Take();
            response.Response = initialFrame.Bytes.ReadLongL(ResponseResponseFieldOffset);
            return response;
        }

    }
}