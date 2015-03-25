using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Patterns.StatePattern
{
    public abstract class BaseState
    {
        public abstract FestivalState ChangeState();
    }
}
