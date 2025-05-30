﻿namespace AMSupplement.Domain.Entities
{
    public class User
    {  
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }  
    }
}
