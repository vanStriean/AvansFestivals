using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Concrete
{
    public class CommentRepo : ICommentRepo
    {
       AvansFestivalsEntities db = new AvansFestivalsEntities();
       
        public void AddComment(Comment comment)
       {
           db.Comments.Add(comment);
           db.SaveChanges();
       }
    }
}
