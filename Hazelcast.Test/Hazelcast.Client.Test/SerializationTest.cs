﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazelcast.Client.Example;
using Hazelcast.Config;
using Hazelcast.Core;
using Hazelcast.IO.Serialization;
using Hazelcast.Util;

namespace Hazelcast.Client.Test
{
    class SerializationTest
    {

        static void Main(string[] args)
        {
            var clientConfig = new ClientConfig();
            clientConfig.GetNetworkConfig().AddAddress("127.0.0.1");
            clientConfig.GetSerializationConfig().AddDataSerializableFactory(1, new MyDataSerializableFactory());


            IHazelcastInstance client = HazelcastClient.NewHazelcastClient(clientConfig);
            //All cluster operations that you can do with ordinary HazelcastInstance
            IMap<string, DataSerializableType> map = client.GetMap<string, DataSerializableType>("imap");

            ISerializationService service = ((HazelcastClientProxy)client).GetSerializationService();

            object obj=new DataSerializableType(1000,1000);
            long start = Clock.CurrentTimeMillis();
            Data data = service.ToData(obj);

            var dataSerializableType = service.ToObject<DataSerializableType>(data);

            long diff = Clock.CurrentTimeMillis()- start;

            Console.WriteLine("Serialization time:"+diff);

            Console.ReadKey();
        }
    }
}