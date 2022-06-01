using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Data.Database.Cargo4You.Model
{
    public class CourierPrice
    {
        [Key]
        public int Id { get; set; }

        public int? From { get; set; }

        public int? To { get; set; }
        public bool IsWeight { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Courier")]
        public int CourierId { get; set; }

        public Courier Courier { get; set; }

    }
}
