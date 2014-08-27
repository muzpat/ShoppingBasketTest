using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketTest.Models
{
    public class Offer
    {
        public int GroceryId { get; set; }
        public string description { get; set; }
        public int type { get; set; }   /// 1= discount, 2= buy one get something else half price, 3 bogof
        public int discount { get; set; }
        public int AssociatedGroceryId { get; set; }

    }
}