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
    
    public partial class Role
    {
        public Role()
        {
            this.UserAndRoles = new HashSet<UserAndRole>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<UserAndRole> UserAndRoles { get; set; }
    }
}
