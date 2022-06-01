using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Services.Model
{
    public class AddCourierData
    {
        public string Name { get; set; }

        public int? DimensionFrom { get; set; }

        public int? DimensionTo { get; set; }

        public int? WeightFrom { get; set; }

        public int? WeightTo { get; set; }

    }
}
