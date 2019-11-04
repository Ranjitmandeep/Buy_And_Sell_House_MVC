using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buy_And_Sell_House_Core_Webapp.Business
{
    //Represensts a Seller 
    public class Seller
    {
        // Holds seller id 
        public string Id { get; set; }

        //Holds Link to a house.
        public House House { get; set; }

        //Holds house id foreign key.
        public string HouseId { get; set; }

        //Holds seller name
        public string SellerName { get; set; }

        //Holds seller phone number.
        public string SellerPhoneNumber { get; set; }


    }
}
