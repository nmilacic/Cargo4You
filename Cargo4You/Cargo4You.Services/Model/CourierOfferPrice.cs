using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Services.Model
{
    public class CourierOfferPrice
    {
        public decimal Price { get; set; }
        public CourierData Courier { get; set; }
    }
}
