using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingBasketTest.Models;

namespace ShoppingBasketTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpPost]
        public ActionResult Shop(string SelectedGroceries)
        {
            BasketViewModel myViewModel = new BasketViewModel();
            try
            {
                // Construct Stock and Offer info
                Stock myStock = new Stock();
                myStock = myStock.CreateStock();
                myViewModel.getStock(myStock);
                Offers mySpecials = new Offers();
                mySpecials = mySpecials.CreateOffers();
                string[] groceries = SelectedGroceries.Trim().Split(' ');
                decimal subTotal = 0;
                
                // Calculate basket cost from Stock info (without discounts)
                foreach (string item in groceries)
                {
                    // Find Stock info for this Grocery Item
                    GroceryItem myGroceryItem = myStock.list.Where(i => i.description == item).FirstOrDefault();
                    if (myGroceryItem == null)
                    {
                        myViewModel.outputLines.Add("Error! " + item == " " ? "<space>" : item  + "wasn't found!");
                    }
                    // Add to Running total
                    subTotal = subTotal + myGroceryItem.price;
                }
                // Display Sub Total
                string myLine = String.Format("Sub Total: {0:C}", subTotal);
                myViewModel.outputLines.Add(myLine);
                // Subtract any discounts
                decimal discountTotal = 0;
                int offerCnt = 0;
                decimal discountAmt1 = 0;
                // Buy two item and get half off something else discount
                foreach (Offer anOffer in mySpecials.Specials)
                {
                    if (anOffer.type == 2)
                    {
                        // Find GroceryId for stock
                        GroceryItem anItem = myStock.list.Where(i => i.GroceryId == anOffer.GroceryId).FirstOrDefault();
                        // find the number of occurrences for this product
                        int offerItemCnt = 0;
                        foreach (string str in groceries)
                        {
                            if (str == anItem.description)
                            {
                                offerItemCnt++;
                            }
                        }
                        // two or more items?
                        if (offerItemCnt > 1)
                        {
                            // is a associated product included?
                            GroceryItem associatedItem = myStock.list.Where(i => i.GroceryId == anOffer.AssociatedGroceryId).FirstOrDefault();
                            foreach (string str in groceries)
                            {
                                if (str == associatedItem.description)
                                {
                                    // Calculate the buy two get half off something else discount
                                    decimal half = Convert.ToDecimal("0.5");
                                    discountTotal = discountTotal + decimal.Multiply(associatedItem.price, half);
                                    discountAmt1 = decimal.Multiply(associatedItem.price, half);
                                    myLine = associatedItem.description + " " + anOffer.description + ": -" + String.Format("{0:C}", discountAmt1);
                                    myViewModel.outputLines.Add(myLine);
                                    offerCnt++;
                                    break;
                                }
                            }
                        }
                    }

                }

                // Calculate any percentage off discounts
                foreach (string item in groceries)
                {
                    // Find the Stock record for the current check-out item
                    GroceryItem myGroceryItem = myStock.list.Where(i => i.description == item).FirstOrDefault();
                    Offer myOffer = mySpecials.Specials.Where(i => i.GroceryId == myGroceryItem.GroceryId).FirstOrDefault();
                    if (myOffer != null)
                    {
                       // Is this a discount offer?
                        if (myOffer.type == 1)
                        {
                            // Calculate the percentage discount
                            offerCnt++;
                            discountTotal = discountTotal + myGroceryItem.price * myOffer.discount;
                            decimal discountAmt2 = myGroceryItem.price * myOffer.discount / 100;
                            myLine = myGroceryItem.description + " " + myOffer.description + ": -" + String.Format("{0:C}", discountAmt2);
                            myViewModel.outputLines.Add(myLine);
                            discountAmt1 = discountAmt1 + discountAmt2;
                        }
                    }
                }
                if (offerCnt == 0)
                {
                    // If no offers in basket, display this info
                    myLine = "(No offers available)";
                    myViewModel.outputLines.Add(myLine);
                    myLine = String.Format(" Total Price {0:C}", subTotal - discountAmt1);
                    myViewModel.outputLines.Add(myLine);
                }
                else
                {
                    // Display Total info
                    myLine = String.Format(" Total Price {0:C}", subTotal - discountAmt1);
                    myViewModel.outputLines.Add(myLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ");
                Console.WriteLine(ex.Message);
                string stack = ex.StackTrace;
                myViewModel.outputLines.Add("Error! " + ex.Message + stack);
                return View(myViewModel);
            }

                return View(myViewModel);
        }

        public ActionResult Shop()
        {
            Stock myStock = new Stock();
            myStock = myStock.CreateStock();
            BasketViewModel myViewModel = new BasketViewModel();
            myViewModel.getStock(myStock);
            myViewModel.myStock.list = myStock.list;
            myViewModel.myBasket = new Basket();
            return View(myViewModel);

        }
        public ActionResult Create()
        {
            // Create Stock container
            Stock myStock = new Stock();
            myStock = myStock.CreateStock();

            // Create Offers
            Offers mySpecials = new Offers();
            mySpecials = mySpecials.CreateOffers();
            List<string> stocklist = myStock.displayStock();
            List<string> offlist = mySpecials.displayOffers();
            ViewBag.stringlist = stocklist.Concat(offlist).ToList();
            return View();
        }
    }
}
