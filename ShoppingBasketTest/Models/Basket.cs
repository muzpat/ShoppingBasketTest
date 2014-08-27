using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketTest.Models
{
    public class Basket
    {   
        public List<GroceryItem> ShoppingBasket {get; set;}
        public Basket()
        {
            ShoppingBasket = new List<GroceryItem>();
        }

    }

}