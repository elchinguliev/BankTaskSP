using BankAtmSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAtmSP.Repo
{
    public class CardRepo
    {
        public List<Card> GetAll()
        {
            return new List<Card>
            {
                new Card
                {
                  CardHolderName="Elchin",
                  CardNumber="5103071509738049",
                  Balance=1000
                },
                new Card
                {
                  CardHolderName="Rasul",
                  CardNumber="4098584450544398",
                  Balance=740
                }
            };
        }
    }
}
