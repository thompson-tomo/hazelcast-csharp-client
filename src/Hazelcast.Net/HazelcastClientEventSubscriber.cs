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
using System;
using Hazelcast.Core;
using Hazelcast.Exceptions;

namespace Hazelcast
{
    internal class HazelcastClientEventSubscriber : IHazelcastClientEventSubscriber
    {
        private readonly Action<HazelcastClientEventHandlers> _build;
        private readonly Type _type;
        private readonly string _typename;
        private readonly IHazelcastClientEventSubscriber _subscriber;

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelcastClientEventSubscriber"/> class.
        /// </summary>
        /// <param name="build">A build method.</param>
        public HazelcastClientEventSubscriber(Action<HazelcastClientEventHandlers> build)
        {
            _build = build ?? throw new ArgumentNullException(nameof(build));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelcastClientEventSubscriber"/> class.
        /// </summary>
        /// <param name="type">A subscriber class type.</param>
        public HazelcastClientEventSubscriber(Type type)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelcastClientEventSubscriber"/> class.
        /// </summary>
        /// <param name="typename">A subscriber class type name.</param>
        public HazelcastClientEventSubscriber(string typename)
        {
            if (string.IsNullOrWhiteSpace(typename)) throw new ArgumentException(ExceptionMessages.NullOrEmpty, nameof(typename));
            _typename = typename;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelcastClientEventSubscriber"/> class.
        /// </summary>
        /// <param name="subscriber">A subscriber class instance.</param>
        public HazelcastClientEventSubscriber(IHazelcastClientEventSubscriber subscriber)
        {
            _subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
        }

        /// <inheritdoc />
        public void Build(HazelcastClientEventHandlers events)
        {
            if (_build != null)
            {
                _build(events);
            }
            else
            {
                var subscriber = _subscriber ?? (_type == null
                    ? ServiceFactory.CreateInstance<IHazelcastClientEventSubscriber>(_typename, null)
                    : ServiceFactory.CreateInstance<IHazelcastClientEventSubscriber>(_type, null));

                subscriber.Build(events);
            }
        }
    }
}
