using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElastiCoctail
{
    public class SearchResultModel
    {
        public double Score { get; set; }

        public CoctailModel Coctail { get; set; }
    }
}
