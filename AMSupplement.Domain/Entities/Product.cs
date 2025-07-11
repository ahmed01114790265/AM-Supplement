﻿using AM_Supplement.Shared.Enums;
using AMSupplement.Domain.AuditEntityInterfaces;
namespace AMSupplement.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Taste { get; set; }
        public string ImageUrl {  get; set; }
        public double Weight { get; set; }
        public int Discount { get; set; }
        public ProductType Type { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
