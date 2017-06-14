using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
