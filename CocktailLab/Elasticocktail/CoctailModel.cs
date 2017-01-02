using System.Collections.Generic;
using Nest;

namespace ElastiCoctail
{
    [ElasticsearchType(Name = "content")]

    public class CoctailModel
    {
        public string Content { get; set; }

        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public List<string> Items { get; set; }

        public List<string> itemmeasures { get; set; }


    }
}
