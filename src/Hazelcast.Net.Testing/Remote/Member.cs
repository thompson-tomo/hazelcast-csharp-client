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

  public partial class Member : TBase
  {
    private string _uuid;
    private string _host;
    private int _port;

    public string Uuid
    {
      get
      {
        return _uuid;
      }
      set
      {
        __isset.uuid = true;
        this._uuid = value;
      }
    }

    public string Host
    {
      get
      {
        return _host;
      }
      set
      {
        __isset.host = true;
        this._host = value;
      }
    }

    public int Port
    {
      get
      {
        return _port;
      }
      set
      {
        __isset.port = true;
        this._port = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool uuid;
      public bool host;
      public bool port;
    }

    public Member()
    {
    }

    public Member DeepCopy()
    {
      var tmp10 = new Member();
      if((Uuid != null) && __isset.uuid)
      {
        tmp10.Uuid = this.Uuid;
      }
      tmp10.__isset.uuid = this.__isset.uuid;
      if((Host != null) && __isset.host)
      {
        tmp10.Host = this.Host;
      }
      tmp10.__isset.host = this.__isset.host;
      if(__isset.port)
      {
        tmp10.Port = this.Port;
      }
      tmp10.__isset.port = this.__isset.port;
      return tmp10;
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
                Uuid = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.String)
              {
                Host = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.I32)
              {
                Port = await iprot.ReadI32Async(cancellationToken);
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
        var tmp11 = new TStruct("Member");
        await oprot.WriteStructBeginAsync(tmp11, cancellationToken);
        var tmp12 = new TField();
        if((Uuid != null) && __isset.uuid)
        {
          tmp12.Name = "uuid";
          tmp12.Type = TType.String;
          tmp12.ID = 1;
          await oprot.WriteFieldBeginAsync(tmp12, cancellationToken);
          await oprot.WriteStringAsync(Uuid, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Host != null) && __isset.host)
        {
          tmp12.Name = "host";
          tmp12.Type = TType.String;
          tmp12.ID = 2;
          await oprot.WriteFieldBeginAsync(tmp12, cancellationToken);
          await oprot.WriteStringAsync(Host, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if(__isset.port)
        {
          tmp12.Name = "port";
          tmp12.Type = TType.I32;
          tmp12.ID = 3;
          await oprot.WriteFieldBeginAsync(tmp12, cancellationToken);
          await oprot.WriteI32Async(Port, cancellationToken);
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
      if (!(that is Member other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.uuid == other.__isset.uuid) && ((!__isset.uuid) || (global::System.Object.Equals(Uuid, other.Uuid))))
        && ((__isset.host == other.__isset.host) && ((!__isset.host) || (global::System.Object.Equals(Host, other.Host))))
        && ((__isset.port == other.__isset.port) && ((!__isset.port) || (global::System.Object.Equals(Port, other.Port))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Uuid != null) && __isset.uuid)
        {
          hashcode = (hashcode * 397) + Uuid.GetHashCode();
        }
        if((Host != null) && __isset.host)
        {
          hashcode = (hashcode * 397) + Host.GetHashCode();
        }
        if(__isset.port)
        {
          hashcode = (hashcode * 397) + Port.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var tmp13 = new StringBuilder("Member(");
      int tmp14 = 0;
      if((Uuid != null) && __isset.uuid)
      {
        if(0 < tmp14++) { tmp13.Append(", "); }
        tmp13.Append("Uuid: ");
        Uuid.ToString(tmp13);
      }
      if((Host != null) && __isset.host)
      {
        if(0 < tmp14++) { tmp13.Append(", "); }
        tmp13.Append("Host: ");
        Host.ToString(tmp13);
      }
      if(__isset.port)
      {
        if(0 < tmp14++) { tmp13.Append(", "); }
        tmp13.Append("Port: ");
        Port.ToString(tmp13);
      }
      tmp13.Append(')');
      return tmp13.ToString();
    }
  }

}
