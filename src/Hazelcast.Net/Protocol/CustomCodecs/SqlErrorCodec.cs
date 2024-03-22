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
using System.Collections.Generic;
using Hazelcast.Protocol.BuiltInCodecs;
using Hazelcast.Protocol.CustomCodecs;
using Hazelcast.Core;
using Hazelcast.Messaging;
using Hazelcast.Clustering;
using Hazelcast.Serialization;
using Microsoft.Extensions.Logging;

namespace Hazelcast.Protocol.CustomCodecs
{
    internal static class SqlErrorCodec
    {
        private const int CodeFieldOffset = 0;
        private const int OriginatingMemberIdFieldOffset = CodeFieldOffset + BytesExtensions.SizeOfInt;
        private const int InitialFrameSize = OriginatingMemberIdFieldOffset + BytesExtensions.SizeOfCodecGuid;

        public static void Encode(ClientMessage clientMessage, Hazelcast.Sql.SqlError sqlError)
        {
            clientMessage.Append(Frame.CreateBeginStruct());

            var initialFrame = new Frame(new byte[InitialFrameSize]);
            initialFrame.Bytes.WriteIntL(CodeFieldOffset, sqlError.Code);
            initialFrame.Bytes.WriteGuidL(OriginatingMemberIdFieldOffset, sqlError.OriginatingMemberId);
            clientMessage.Append(initialFrame);

            CodecUtil.EncodeNullable(clientMessage, sqlError.Message, StringCodec.Encode);
            CodecUtil.EncodeNullable(clientMessage, sqlError.Suggestion, StringCodec.Encode);
            CodecUtil.EncodeNullable(clientMessage, sqlError.CauseStackTrace, StringCodec.Encode);

            clientMessage.Append(Frame.CreateEndStruct());
        }

        public static Hazelcast.Sql.SqlError Decode(IEnumerator<Frame> iterator)
        {
            // begin frame
            iterator.Take();

            var initialFrame = iterator.Take();
            var code = initialFrame.Bytes.ReadIntL(CodeFieldOffset);

            var originatingMemberId = initialFrame.Bytes.ReadGuidL(OriginatingMemberIdFieldOffset);
            var message = CodecUtil.DecodeNullable(iterator, StringCodec.Decode);
            var isSuggestionExists = false;
            string suggestion = default;
            if (iterator.NextIsNotTheEnd())
            {
                suggestion = CodecUtil.DecodeNullable(iterator, StringCodec.Decode);
                isSuggestionExists = true;
            }
            var isCauseStackTraceExists = false;
            string causeStackTrace = default;
            if (iterator.NextIsNotTheEnd())
            {
                causeStackTrace = CodecUtil.DecodeNullable(iterator, StringCodec.Decode);
                isCauseStackTraceExists = true;
            }

            iterator.SkipToStructEnd();
            return new Hazelcast.Sql.SqlError(code, message, originatingMemberId, isSuggestionExists, suggestion, isCauseStackTraceExists, causeStackTrace);
        }
    }
}

#pragma warning restore IDE0051 // Remove unused private members
