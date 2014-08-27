using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketTest.Models
{
    public class GroceryItem
    {
        public int GroceryId { get; set;}
        public string description  { get; set;}
        public string unit { get; set;}
        public decimal price { get; set;}

    }
}