﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSupplement.Domain.Entities
{
   public  class OrderItem
    {
        public Guid Id { get; set; }
        public double Pricepurchase { get; set; }
        public int  Quantity { get; set; }
        public Guid OredeId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
