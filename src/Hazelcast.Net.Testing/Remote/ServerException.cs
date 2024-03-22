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

/**
 * <auto-generated>
 * Autogenerated by Thrift Compiler (0.18.0)
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 * </auto-generated>
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE0017  // object init can be simplified
#pragma warning disable IDE0028  // collection init can be simplified
#pragma warning disable IDE1006  // parts of the code use IDL spelling
#pragma warning disable CA1822   // empty DeepCopy() methods still non-static
#pragma warning disable IDE0083  // pattern matching "that is not SomeType" requires net5.0 but we still support earlier versions

namespace Hazelcast.Testing.Remote
{

  public partial class ServerException : TException, TBase
  {
    private string _message;

    public string Message
    {
      get
      {
        return _message;
      }
      set
      {
        __isset.message = true;
        this._message = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool message;
    }

    public ServerException()
    {
    }

    public ServerException DeepCopy()
    {
      var tmp20 = new ServerException();
      if((Message != null) && __isset.message)
      {
        tmp20.Message = this.Message;
      }
      tmp20.__isset.message = this.__isset.message;
      return tmp20;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String)
              {
                Message = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var tmp21 = new TStruct("ServerException");
        await oprot.WriteStructBeginAsync(tmp21, cancellationToken);
        var tmp22 = new TField();
        if((Message != null) && __isset.message)
        {
          tmp22.Name = "message";
          tmp22.Type = TType.String;
          tmp22.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp22, cancellationToken);
          await oprot.WriteStringAsync(Message, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      if (!(that is ServerException other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.message == other.__isset.message) && ((!__isset.message) || (global::System.Object.Equals(Message, other.Message))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Message != null) && __isset.message)
        {
          hashcode = (hashcode * 397) + Message.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp23 = new StringBuilder("ServerException(");
      int tmp24 = 0;
      if((Message != null) && __isset.message)
      {
        if(0 < tmp24++) { tmp23.Append(", "); }
        tmp23.Append("Message: ");
        Message.ToString(tmp23);
      }
      tmp23.Append(')');
      return tmp23.ToString();
    }
  }

}
