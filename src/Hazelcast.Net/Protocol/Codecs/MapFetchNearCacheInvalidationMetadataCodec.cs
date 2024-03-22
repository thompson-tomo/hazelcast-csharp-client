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
    /// Fetches invalidation metadata from partitions of map.
    ///</summary>
#if SERVER_CODEC
    internal static class MapFetchNearCacheInvalidationMetadataServerCodec
#else
    internal static class MapFetchNearCacheInvalidationMetadataCodec
#endif
    {
        public const int RequestMessageType = 81152; // 0x013D00
        public const int ResponseMessageType = 81153; // 0x013D01
        private const int RequestUuidFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestInitialFrameSize = RequestUuidFieldOffset + BytesExtensions.SizeOfCodecGuid;
        private const int ResponseInitialFrameSize = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// names of the maps
            ///</summary>
            public IList<string> Names { get; set; }

            /// <summary>
            /// The uuid of the member to fetch the near cache invalidation meta data
            ///</summary>
            public Guid Uuid { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(ICollection<string> names, Guid uuid)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = false,
                OperationName = "Map.FetchNearCacheInvalidationMetadata"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteGuidL(RequestUuidFieldOffset, uuid);
            clientMessage.Append(initialFrame);
            ListMultiFrameCodec.Encode(clientMessage, names, StringCodec.Encode);
            return clientMessage;
        }

#if SERVER_CODEC
        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var request = new RequestParameters();
            var initialFrame = iterator.Take();
            request.Uuid = initialFrame.Bytes.ReadGuidL(RequestUuidFieldOffset);
            request.Names = ListMultiFrameCodec.Decode(iterator, StringCodec.Decode);
            return request;
        }
#endif

        public sealed class ResponseParameters
        {

            /// <summary>
            /// Map of partition ids and sequence number of invalidations mapped by the map name.
            ///</summary>
            public IList<KeyValuePair<string, IList<KeyValuePair<int, long>>>> NamePartitionSequenceList { get; set; }

            /// <summary>
            /// Map of member UUIDs mapped by the partition ids of invalidations.
            ///</summary>
            public IList<KeyValuePair<int, Guid>> PartitionUuidList { get; set; }
        }

#if SERVER_CODEC
        public static ClientMessage EncodeResponse(ICollection<KeyValuePair<string, ICollection<KeyValuePair<int, long>>>> namePartitionSequenceList, ICollection<KeyValuePair<int, Guid>> partitionUuidList)
        {
            var clientMessage = new ClientMessage();
            var initialFrame = new Frame(new byte[ResponseInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, ResponseMessageType);
            clientMessage.Append(initialFrame);
            EntryListCodec.Encode(clientMessage, namePartitionSequenceList, StringCodec.Encode, EntryListIntegerLongCodec.Encode);
            EntryListIntegerUUIDCodec.Encode(clientMessage, partitionUuidList);
            return clientMessage;
        }
#endif

        public static ResponseParameters DecodeResponse(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var response = new ResponseParameters();
            iterator.Take(); // empty initial frame
            response.NamePartitionSequenceList = EntryListCodec.Decode(iterator, StringCodec.Decode, EntryListIntegerLongCodec.Decode);
            response.PartitionUuidList = EntryListIntegerUUIDCodec.Decode(iterator);
            return response;
        }

    }
}
