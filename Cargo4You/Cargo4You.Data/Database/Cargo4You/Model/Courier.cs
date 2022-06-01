﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Data.Database.Cargo4You.Model
{
    public class Courier
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? DimensionFrom { get; set; }

        public int? DimensionTo { get; set; }

        public int? WeightFrom { get; set; }

        public int? WeightTo { get; set; }
    }
}
