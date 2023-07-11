using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAtmSP.Models
{
    public class Card
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
