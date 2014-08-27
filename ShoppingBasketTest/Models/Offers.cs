using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketTest.Models
{
    public class Offers
    {
        public List<Offer> Specials { get; set; }

        public Offers()
        {
        Specials = new List<Offer>();
        }
        public List<string> displayOffers()
        {
            List<string> outputLines = new List<string>();
            outputLines.Add("--This week's offers--");
            foreach (Offer mySpecial in Specials)
            {
                string myLine = mySpecial.description;
                outputLines.Add(myLine);
            }
            return outputLines;
        }
        public Offers CreateOffers()
        {
            Stock myStock = new Stock();
            myStock = myStock.CreateStock();
            Offers mySpecials = new Offers();
            Offer myOffer = new Offer();
            myOffer.GroceryId = myStock.list.Where(i => i.description == "Apples").FirstOrDefault().GroceryId;
            myOffer.type = 1; // discount
            myOffer.description = "have a 10% discount off their normal price this week";
            myOffer.discount = 10;
            mySpecials.Specials.Add(myOffer);
            // Add soup off
            myOffer = new Offer();
            myOffer.GroceryId = myStock.list.Where(i => i.description == "Soup").FirstOrDefault().GroceryId;
            myOffer.type = 2; // buy two get something else half price
            myOffer.description = "buy two tins of soup and get a loaf of bread half price";
            myOffer.AssociatedGroceryId = myStock.list.Where(i => i.description == "Bread").FirstOrDefault().GroceryId;
            mySpecials.Specials.Add(myOffer);
            return mySpecials;
        }
    }
}