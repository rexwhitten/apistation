using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apistation.owin.Depends
{
    public class RedisChannel : IChannel
    {
        static IConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        private ISubscriber _sub = redis.GetSubscriber();

        public RedisChannel()
        {

        }

        public void Emit(string uri, IDictionary<string, object> stateArg)
        {
            _sub.Publish(uri, JsonConvert.SerializeObject(stateArg));
        }

        public void Handle(string uri, Action<IDictionary<string, object>> handler)
        {
            _sub.Subscribe(uri, (c, v) =>
            {
                IDictionary<string, object> args = JsonConvert.DeserializeObject<IDictionary<string, object>>(v.ToString());
                handler.Invoke(args);
            });
        }
    }
}
