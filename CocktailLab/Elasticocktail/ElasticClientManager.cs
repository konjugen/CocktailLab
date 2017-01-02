using System;
using Nest;

namespace ElastiCoctail
{
    public class ElasticClientManager
    {
        private const string Url = "https://7k46qzf4nh:wc0173c581@ahmet-an-ls-7302804534.eu-west-1.bonsaisearch.net";

        public ElasticClient Client => _client ?? (_client = Build());

        private ElasticClient _client { get; set; }

        private static ElasticClient Build()
        {
            var node = new Uri(Url);
            var settings = new ConnectionSettings(node);
           return new ElasticClient(settings);

        }

        private static readonly Lazy<ElasticClientManager> LazyInstance = new Lazy<ElasticClientManager>(()=> new ElasticClientManager());

        public static ElasticClientManager Instance => LazyInstance.Value;


        private ElasticClientManager()
        {
            
        }
    }
}
