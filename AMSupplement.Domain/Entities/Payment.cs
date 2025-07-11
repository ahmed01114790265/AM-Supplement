﻿using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.AuditEntityInterfaces;

namespace AMSupplement.Domain.Entities
{
   public class Payment : AuditableEntity
    {  
        public Guid Id { get; set; }
        public DateTime Paydate { get; set; } = DateTime.Now;
        public PaymentStatus Paymentstaus {  get; set; }
        public string PaymentMethod { get; set; }
        public Guid OrderId { get; set; }    
        public Order Order { get; set; }
    }
}
