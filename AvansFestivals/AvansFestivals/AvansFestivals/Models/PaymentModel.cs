using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AvansFestivals.Models
{
    public class PaymentModel
    {
        public TicketOrderModel TicketModel { get; set; }
        public IDealModel IdealModel { get; set; }
        public PayPalModel PaypalModel { get; set; }
        public string Strategy { get; set; }

    }

    public class IDealModel
    {
        [Required]
        public int CardNumber { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}")]
        public string Iban { get; set; }

    }

    public class PayPalModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}