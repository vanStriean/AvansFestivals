using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Patterns.IteratorPatternComments
{
    public class Item
    {
        private Comment _comment;
        // Constructor
        public Item(Comment comment)
        {
            _comment = comment;
        }

        public Comment Comment
        {
            get { return _comment; }
        }
    }
}
