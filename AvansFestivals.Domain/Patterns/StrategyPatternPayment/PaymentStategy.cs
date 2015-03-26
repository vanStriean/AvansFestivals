using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.StrategyPatternPayment
{
    public abstract class PaymentStategy
    {
        public abstract void Pay(int amount);
    }
}
