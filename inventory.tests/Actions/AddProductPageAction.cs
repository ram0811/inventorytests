using inventory.tests.DataModel;
using inventory.tests.Pages;
using inventory.tests.Utils;
using OpenQA.Selenium;

namespace inventory.tests.Actions
{
    public class AddProductPageAction : BaseAction
    {
        private AddProductPage addproduct;

        public AddProductPageAction(IWebDriver wd) : base(wd)
        {
            addproduct = new AddProductPage(wd);
        }

        public void FillRequiredFields(string prodcode, string proddesc)
        {
            var prodcodetb = addproduct.ProductCodeTextbox;
            prodcodetb.Clear();
            prodcodetb.SendKeys(prodcode);

            var proddescta = addproduct.ProductDescTextarea;
            proddescta.Clear();
            proddescta.SendKeys(proddesc);
        }

        public void SelectUnitMeasure(string unit)
        {
            addproduct.UnitMeasureSelectbox.SelectByText(unit);
        }

        public void SelectProductGrp(string group)
        {
            addproduct.ProductGrpSelectbox.SelectByText(group);
        }

        public void ClickSave()
        {
            addproduct.SaveButton.Click();
        }

        public MessageBoxContent GetMessagebox()
        {
            var msgboxcontent = new MessageBoxContent();
            var msgbox = addproduct.Messagebox;
            msgboxcontent.Title = addproduct.GetMessageboxTitle(msgbox).Trim();
            msgboxcontent.Text = addproduct.GetMessageboxText(msgbox).Trim();
            addproduct.GetMessageboxCloseBtn(msgbox).Click();
            return msgboxcontent;
        }

        public bool IsMessageboxInvisible
        {
            get
            {
                return addproduct.IsMessageboxInvisible;
            }
        }
    }
}
