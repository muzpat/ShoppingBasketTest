using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingBasketTest.Models
{
    public class Stock
    {
        public List<GroceryItem> list { get; set; }
        public int nextGroceryID
        {
            set;
            get;


        }
        public Stock()
        {
            nextGroceryID = 0;
        }
        public int GetNextGroceryID()
        {
            nextGroceryID = nextGroceryID + 1;
            return nextGroceryID;
        }
        public Stock CreateStock()
        {
            Stock myStock = new Stock();
            // Add stock item Soup
            GroceryItem myItem = new GroceryItem();
            myStock.list = new List<GroceryItem>();
            myItem.GroceryId = myStock.GetNextGroceryID();
            myItem.description = "Soup";
            myItem.unit = "per tin";
            myItem.price = 0.65M;
            myStock.list.Add(myItem);
            // Add Bread
            myItem = new GroceryItem();
            myItem.GroceryId = myStock.GetNextGroceryID();
            myItem.description = "Bread";
            myItem.unit = "per loaf";
            myItem.price = 0.80M;
            myStock.list.Add(myItem);
            // Add Milk
            myItem = new GroceryItem();
            myItem.GroceryId = myStock.GetNextGroceryID();
            myItem.description = "Milk";
            myItem.unit = "per bottle";
            myItem.price = 1.30M;
            myStock.list.Add(myItem);
            // Add Apples
            myItem = new GroceryItem();
            myItem.GroceryId = myStock.GetNextGroceryID();
            myItem.description = "Apples";
            myItem.unit = "per bag";
            myItem.price = 1.00M;
            myStock.list.Add(myItem);
            return myStock;
        }

        public List<string> displayStock()
        {
             List<string> outputLines = new List<string>();
             outputLines.Add("--Stock--");
            // outputLines.Add("");
            // outputLines.Add("");
            foreach (GroceryItem myItem in list)
            {
                string myLine = myItem.description + " " + String.Format("{0:0.00}", myItem.price) + " " + myItem.unit;
                outputLines.Add(myLine);
            }
            return outputLines;
        }

    }
}