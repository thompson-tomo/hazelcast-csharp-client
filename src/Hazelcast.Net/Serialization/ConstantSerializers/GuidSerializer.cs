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

using System;
using Hazelcast.Core;

namespace Hazelcast.Serialization.ConstantSerializers
{
    internal class GuidSerializer : SingletonSerializerBase<Guid>
    {
        public override int TypeId => SerializationConstants.ConstantTypeUuid;

        public override Guid Read(IObjectDataInput input)
        {
            if (input is ObjectDataInput concrete)
            {
                var guid = concrete.Buffer.ReadGuid(concrete.Position, concrete.Endianness, false);
                concrete.Position += BytesExtensions.SizeOfGuid;
                return guid;
            }

            // that should never happens, and if it happens one day, we'll deal with it
            throw new NotSupportedException("Input is not ObjectDataInput.");
        }

        public override void Write(IObjectDataOutput output, Guid obj)
        {
            if (output is ObjectDataOutput concrete)
            {
                concrete.Buffer.WriteGuid(concrete.Position, obj, concrete.Endianness, false);
                concrete.Position += BytesExtensions.SizeOfGuid;
                return;
            }

            // that should never happens, and if it happens one day, we'll deal with it
            throw new NotSupportedException("Output is not ObjectDataOutput.");
        }
    }
}
