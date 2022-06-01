using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Services.Model
{
    public class CourierPriceData
    {
        public int? From { get; set; }

        public int? To { get; set; }

        public bool IsWeight { get; set; }

        public decimal Price { get; set; }

        public int CourierId { get; set; }
    }
}
