//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AvansFestivals.Domain.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public TicketType TicketType { get; set; }
        public int FestivalId { get; set; }
        public int OrderItemId { get; set; }
        public int UserId { get; set; }
    
        public virtual Festival Festival { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual User User { get; set; }
    }
}