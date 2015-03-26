﻿using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.StatePattern
{
    public class SoldOutState : BaseState
    {
        public override FestivalState ChangeState()
        {
            return FestivalState.SoldOut;
        }
    }
}
