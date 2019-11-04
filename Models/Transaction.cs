using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_And_Sell_House_Core_Webapp.Business
{
    //Represensts the house sales  transaction 
    public class Transaction
    {
        //Holds the tranaction id
        public string TransactionId { get; set; }
        //Holds buyer Id
        public string BuyerId { get; set; }

        //Holds  house id
        public string HouseId { get; set; }

        //Holds a link to buyer.
        public Buyer Buyer { get; set; }

        //Holds a link to house.
        public  House House{ get; set; }

    }
}
