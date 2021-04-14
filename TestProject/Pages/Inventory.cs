using CSharpUISelenium.DriverManager;
using CSharpUISelenium.ServiceImplimentation;
using CSharpUISelenium.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Pages
{
    public class Inventory : Base
    {

        private By pgTitle = By.CssSelector(".title");
        private By inventoryItem = By.CssSelector("#inventory_container .inventory_item");



        private IElementServices _elementServices = new ElementHelpers();
        public Inventory()
        {
            _elementServices = _elementServices ?? new ElementHelpers();
        }

        

    }
}
