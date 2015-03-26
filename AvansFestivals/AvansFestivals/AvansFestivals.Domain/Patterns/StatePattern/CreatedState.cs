using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Patterns.StatePattern
{
    public class CreatedState : BaseState
    {
        public override FestivalState ChangeState()
        {
            return FestivalState.Created;
        }
    }
}
