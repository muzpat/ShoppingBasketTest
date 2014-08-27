using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingBasketTest.Models
{
    public class BasketViewModel
    {
        public string selecteditemDdlGroceryItem { get; set; }
        public List<SelectListItem> ddlGroceryItem { get; set; }
        public Basket myBasket { get; set; }
        public Stock myStock { get; set; }
        public List<string> outputLines { get; set; }
        public BasketViewModel()
        {
            ddlGroceryItem = new List<SelectListItem>();
            selecteditemDdlGroceryItem = "1";
            myStock = new Stock();
            outputLines = new List<string>();
        }
        public List<SelectListItem> getStock(Stock myStock)
        {
            SelectListItem myItem = new SelectListItem();
            foreach (GroceryItem Item in myStock.list)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Text = Item.description;
                listItem.Value = Item.GroceryId.ToString();
                ddlGroceryItem.Add(listItem);
            }
            return ddlGroceryItem;
        }
    }
}