using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.StrategyPatternPayment
{
    public class IdealStrategy : PaymentStategy
    {
        string cardNumber;
        string ibanNumber;

        public IdealStrategy(string cardnumber, string iban)
        {
            this.cardNumber = cardnumber;
            this.ibanNumber = iban;
        }

        public override void Pay(int amount)
        {
            // todo
        }
    }
}
