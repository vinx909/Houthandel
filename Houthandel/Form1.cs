using Glashandel;
using OuderbijdrageSchool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Houthandel
{
    public partial class Form1 : Form
    {
        private const string woodOrderVolumeString = "the volume of the order in cubic meters";
        private const string woodOrderScrapeSurfaseString = "the surfase that must be scraped in quare meters";
        private const string woodOrderOrderDateString = "date the order was placed";
        private const string woodOrderDeliveryDateString = "delivery date";
        private const string woodOrderShowCostButtonString = "calculate the order cost";
        private const string messageBoxShowCostString = "the cost of the wood order is ";

        private const int widthMargin = 10;
        private const int heightMargin = 10;
        private const int rowHeight = 30;
        private const int textLengthTextBox = 240;
        private const int textLengthDate = 132;

        private RadioButtonsFormElement woodOrderClass;
        private TextBoxWithLabelFormElement woodOrderVolume;
        private TextBoxWithLabelFormElement woodOrderScrapeSurfase;
        private DateFormElement woodOrderOrderDate;
        private DateFormElement woodOrderDeliveryDate;
        private ButtonFormElement woodOrderShowCostButton;

        public Form1()
        {
            InitializeComponent();
            InitializeElements();
            ResetPosition();
        }
        private void InitializeElements()
        {
            woodOrderClass = new RadioButtonsFormElement(this, TimberTradeCost.GetWoodOrderClasses(), rowHeight);
            woodOrderVolume = new TextBoxWithLabelFormElement(this, woodOrderVolumeString);
            woodOrderVolume.SetTextBoxOfset(textLengthTextBox);
            woodOrderScrapeSurfase = new TextBoxWithLabelFormElement(this, woodOrderScrapeSurfaseString);
            woodOrderScrapeSurfase.SetTextBoxOfset(textLengthTextBox);
            woodOrderOrderDate = new DateFormElement(this, woodOrderOrderDateString);
            woodOrderOrderDate.SetTextBoxOfset(textLengthDate);
            woodOrderDeliveryDate = new DateFormElement(this, woodOrderDeliveryDateString);
            woodOrderDeliveryDate.SetTextBoxOfset(textLengthDate);
            woodOrderShowCostButton = new ButtonFormElement(this, woodOrderShowCostButtonString, WoodOrderShowCostButtonFunction);
        }
        private void ResetPosition()
        {
            int row = 0;
            woodOrderClass.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row += woodOrderClass.GetAmountOfRows();
            woodOrderVolume.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            woodOrderScrapeSurfase.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            woodOrderOrderDate.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            woodOrderDeliveryDate.ChangePosition(widthMargin, heightMargin + row * rowHeight);
            row++;
            woodOrderShowCostButton.ChangePosition(widthMargin, heightMargin + row * rowHeight);
        }

        private void WoodOrderShowCost()
        {
            string className = woodOrderClass.GetValue();
            double volume = woodOrderVolume.GetValueAsDouble();
            double scrapeSurfase = woodOrderScrapeSurfase.GetValueAsDouble();
            int orderDay = woodOrderOrderDate.GetDateDay();
            int orderMonth = woodOrderOrderDate.GetDayMonth();
            int orderYear = woodOrderOrderDate.GetDateYear();
            int deliveryDay = woodOrderDeliveryDate.GetDateDay();
            int deliveryMonth = woodOrderDeliveryDate.GetDayMonth();
            int deliveryYear = woodOrderDeliveryDate.GetDateYear();
            double cost = TimberTradeCost.GetCost(className, volume, scrapeSurfase, orderDay, orderMonth, orderYear, deliveryDay, deliveryMonth, deliveryYear);
            MessageBox.Show(messageBoxShowCostString + cost);
        }

        private void WoodOrderShowCostButtonFunction(object sender, EventArgs e)
        {
            WoodOrderShowCost();
        }
    }
}
