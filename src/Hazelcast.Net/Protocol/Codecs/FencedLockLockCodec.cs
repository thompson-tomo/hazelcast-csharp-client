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
    /// Acquires the given FencedLock on the given CP group. If the lock is
    /// acquired, a valid fencing token (positive number) is returned. If not
    /// acquired because of max reentrant entry limit, the call returns -1.
    /// If the lock is held by some other endpoint when this method is called,
    /// the caller thread is blocked until the lock is released. If the session
    /// is closed between reentrant acquires, the call fails with
    /// {@code LockOwnershipLostException}.
    ///</summary>
#if SERVER_CODEC
    internal static class FencedLockLockServerCodec
#else
    internal static class FencedLockLockCodec
#endif
    {
        public const int RequestMessageType = 459008; // 0x070100
        public const int ResponseMessageType = 459009; // 0x070101
        private const int RequestSessionIdFieldOffset = Messaging.FrameFields.Offset.PartitionId + BytesExtensions.SizeOfInt;
        private const int RequestThreadIdFieldOffset = RequestSessionIdFieldOffset + BytesExtensions.SizeOfLong;
        private const int RequestInvocationUidFieldOffset = RequestThreadIdFieldOffset + BytesExtensions.SizeOfLong;
        private const int RequestInitialFrameSize = RequestInvocationUidFieldOffset + BytesExtensions.SizeOfCodecGuid;
        private const int ResponseResponseFieldOffset = Messaging.FrameFields.Offset.ResponseBackupAcks + BytesExtensions.SizeOfByte;
        private const int ResponseInitialFrameSize = ResponseResponseFieldOffset + BytesExtensions.SizeOfLong;

#if SERVER_CODEC
        public sealed class RequestParameters
        {

            /// <summary>
            /// CP group id of this FencedLock instance
            ///</summary>
            public Hazelcast.CP.CPGroupId GroupId { get; set; }

            /// <summary>
            /// Name of this FencedLock instance
            ///</summary>
            public string Name { get; set; }

            /// <summary>
            /// Session ID of the caller
            ///</summary>
            public long SessionId { get; set; }

            /// <summary>
            /// ID of the caller thread
            ///</summary>
            public long ThreadId { get; set; }

            /// <summary>
            /// UID of this invocation
            ///</summary>
            public Guid InvocationUid { get; set; }
        }
#endif

        public static ClientMessage EncodeRequest(Hazelcast.CP.CPGroupId groupId, string name, long sessionId, long threadId, Guid invocationUid)
        {
            var clientMessage = new ClientMessage
            {
                IsRetryable = true,
                OperationName = "FencedLock.Lock"
            };
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], (FrameFlags) ClientMessageFlags.Unfragmented);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.MessageType, RequestMessageType);
            initialFrame.Bytes.WriteIntL(Messaging.FrameFields.Offset.PartitionId, -1);
            initialFrame.Bytes.WriteLongL(RequestSessionIdFieldOffset, sessionId);
            initialFrame.Bytes.WriteLongL(RequestThreadIdFieldOffset, threadId);
            initialFrame.Bytes.WriteGuidL(RequestInvocationUidFieldOffset, invocationUid);
            clientMessage.Append(initialFrame);
            RaftGroupIdCodec.Encode(clientMessage, groupId);
            StringCodec.Encode(clientMessage, name);
            return clientMessage;
        }

#if SERVER_CODEC
        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            using var iterator = clientMessage.GetEnumerator();
            var request = new RequestParameters();
            var initialFrame = iterator.Take();
            request.SessionId = initialFrame.Bytes.ReadLongL(RequestSessionIdFieldOffset);
            request.ThreadId = initialFrame.Bytes.ReadLongL(RequestThreadIdFieldOffset);
            request.InvocationUid = initialFrame.Bytes.ReadGuidL(RequestInvocationUidFieldOffset);
            request.GroupId = RaftGroupIdCodec.Decode(iterator);
            request.Name = StringCodec.Decode(iterator);
            return request;
        }
#endif

        public sealed class ResponseParameters
        {

            /// <summary>
            /// a valid fencing token (positive number) if the lock
            /// is acquired, otherwise -1.
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
