﻿using AMSupplement.Domain.AuditEntityInterfaces;

namespace AMSupplement.Domain.Entities
{
   public  class OrderItem : AuditableEntity
    {
        public Guid Id { get; set; }
        public int  Quantity { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
