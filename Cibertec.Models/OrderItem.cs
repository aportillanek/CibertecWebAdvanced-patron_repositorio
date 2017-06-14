﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.Models
{

    public class OrderItem
    {
        public int Id { get; set; }
        public virtual Order OrderId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
