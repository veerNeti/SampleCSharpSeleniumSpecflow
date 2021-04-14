using CSharpUISelenium.DriverManager;
using CSharpUISelenium.ServiceImplimentation;
using CSharpUISelenium.Services;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Pages
{
    public class InventoryPage : Base
    {

        private By pgTitle = By.CssSelector(".title");
        private By inventoryItem = By.CssSelector("#inventory_container .inventory_item");



        private IElementServices _elementServices = new ElementHelpers();
        public InventoryPage()
        {
            _elementServices = _elementServices ?? new ElementHelpers();
        }

        

    }
}
