using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.StrategyPatternPayment
{
    public class PaypalStrategy : PaymentStategy
    {
        string email;
        string password;

        public PaypalStrategy(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public override void Pay(int amount)
        {
            // todo
        }
    }
}
