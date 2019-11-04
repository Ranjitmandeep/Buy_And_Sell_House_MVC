using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_And_Sell_House_Core_Webapp.Business
{
    //Represents a House.
    public class House
    {
        //Holds house Id.
        public string Id { get; set; }

        //Holds house address.
        public string HouseAddress { get; set; }

        //Holds house price.
        public int HousePrice { get; set; }

        //Holds link to the seller.
        public Seller Seller { get; set; }

    }
}
