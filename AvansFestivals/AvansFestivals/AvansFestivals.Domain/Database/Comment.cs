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
    
    public partial class Comment
    {
        public Comment()
        {
            this.Comments1 = new HashSet<Comment>();
        }
    
        public int Id { get; set; }
        public string Message { get; set; }
        public System.DateTime Created { get; set; }
        public int CommentId { get; set; }
        public int FestivalId { get; set; }
        public int UserId { get; set; }
    
        public virtual ICollection<Comment> Comments1 { get; set; }
        public virtual Comment Comment1 { get; set; }
        public virtual Festival Festival { get; set; }
        public virtual User User { get; set; }
    }
}