// Copyright (c) 2008-2017, Hazelcast, Inc. All Rights Reserved.
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
using System.Collections.Generic;
using Hazelcast.Client.Protocol;
using Hazelcast.Client.Protocol.Util;
using Hazelcast.IO;
using Hazelcast.IO.Serialization;

// Client Protocol version, Since:1.4 - Update:1.4
namespace Hazelcast.Client.Protocol.Codec
{
    internal sealed class MapProjectCodec
    {

        public static readonly MapMessageType RequestType = MapMessageType.MapProject;
        public const int ResponseType = 119;
        public const bool Retryable = true;

        //************************ REQUEST *************************//

        public class RequestParameters
        {
            public static readonly MapMessageType TYPE = RequestType;
            public string name;
            public IData projection;

            public static int CalculateDataSize(string name, IData projection)
            {
                var dataSize = ClientMessage.HeaderSize;
                dataSize += ParameterUtil.CalculateDataSize(name);
                dataSize += ParameterUtil.CalculateDataSize(projection);
                return dataSize;
            }
        }

        public static ClientMessage EncodeRequest(string name, IData projection)
        {
            var requiredDataSize = RequestParameters.CalculateDataSize(name, projection);
            var clientMessage = ClientMessage.CreateForEncode(requiredDataSize);
            clientMessage.SetMessageType((int)RequestType);
            clientMessage.SetRetryable(Retryable);
            clientMessage.Set(name);
            clientMessage.Set(projection);
            clientMessage.UpdateFrameLength();
            return clientMessage;
        }

        //************************ RESPONSE *************************//
        public class ResponseParameters
        {
            public IList<IData> response;
        }

        public static ResponseParameters DecodeResponse(IClientMessage clientMessage)
        {
            var parameters = new ResponseParameters();
            if (clientMessage.IsComplete())
            {
                return parameters;
            }
            var response = new List<IData>();
            var responseSize = clientMessage.GetInt();
            for (var responseIndex = 0; responseIndex<responseSize; responseIndex++)
            {
                var responseItemIsNull = clientMessage.GetBoolean();
                if (!responseItemIsNull)
                {
            var responseItem = clientMessage.GetData();
                    response.Add(responseItem);
                }
                else
                {
                    response.Add(null);
                }
            }
    parameters.response = response;
            return parameters;
        }

    }
}