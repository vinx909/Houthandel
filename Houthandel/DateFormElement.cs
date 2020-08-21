using Glashandel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OuderbijdrageSchool
{
    class DateFormElement:FormElement
    {
        private const int textBoxOfsetDefault = 100;
        private const int textBoxBetweenOfsetDefault = 30;
        private const int textBoxWidthDefault = 25;
        private const int noIntDayDefault = 1;
        private const int noIntMonthDefault = 1;
        private const int noIntYearDefault = 0;

        private int textBoxOfset;
        private int textBoxBetweenOfset;
        private int textBoxWidth;
        private int noIntDay;
        private int noIntMonth;
        private int noIntYear;

        private const int dateArrayLength = 3;
        private const int dateDayIndex = 0;
        private const int dateMonthIndex = 1;
        private const int dateYearIndex = 2;

        private Label label;
        private TextBox textBoxDay;
        private TextBox textBoxMonth;
        private TextBox textBoxYear;

        public DateFormElement(Form form, string labelText):base(form)
        {
            textBoxOfset = textBoxOfsetDefault;
            textBoxBetweenOfset = textBoxBetweenOfsetDefault;
            textBoxWidth = textBoxWidthDefault;
            noIntDay = noIntDayDefault;
            noIntMonth = noIntMonthDefault;
            noIntYear = noIntYearDefault;

            label = new Label();
            label.Text = labelText;
            form.Controls.Add(label);

            textBoxDay = new TextBox();
            textBoxDay.Width = textBoxWidth;
            form.Controls.Add(textBoxDay);

            textBoxMonth = new TextBox();
            textBoxMonth.Width = textBoxWidth;
            form.Controls.Add(textBoxMonth);

            textBoxYear = new TextBox();
            textBoxYear.Width = textBoxWidth;
            form.Controls.Add(textBoxYear);
        }

        protected override void AlterPosition(int widthOfset, int heightOfset)
        {
            label.Location = new Point(widthOfset, heightOfset);
            textBoxDay.Location = new Point(widthOfset + textBoxOfset, heightOfset);
            textBoxMonth.Location = new Point(widthOfset + textBoxOfset + textBoxBetweenOfset, heightOfset);
            textBoxYear.Location = new Point(widthOfset + textBoxOfset + textBoxBetweenOfset * 2, heightOfset);
        }
        public int[] GetDate()
        {
            int[] date = new int[dateArrayLength];
            date[dateDayIndex] = GetDateDay();
            date[dateMonthIndex] = GetDayMonth();
            date[dateYearIndex] = GetDateYear();
            return date;
        }
        public int GetDateDay()
        {
            try
            {
                return int.Parse(textBoxDay.Text);
            }
            catch (FormatException exception)
            {
                return noIntDay;
            }
        }
        public int GetDayMonth()
        {
            try
            {
                return int.Parse(textBoxMonth.Text);
            }
            catch (FormatException exception)
            {
                return noIntMonth;
            }
        }
        public int GetDateYear()
        {
            try
            {
                return int.Parse(textBoxYear.Text);
            }
            catch (FormatException exception)
            {
                return noIntYear;
            }
        }
        public static int GetYearIndex()
        {
            return dateYearIndex;
        }
        public static int GetMonthIndex()
        {
            return dateMonthIndex;
        }
        public static int GetDayIndex()
        {
            return dateDayIndex;
        }

        public void SetTextBoxOfset(int ofset)
        {
            textBoxOfset = ofset;
            label.Width = ofset;
        }
        public void SetTextBoxBetweenOfset(int ofset)
        {
            textBoxBetweenOfset = ofset;
        }
        public void SetTextBoxWidthDefault(int width)
        {
            textBoxWidth = width;
            textBoxDay.Width = textBoxWidth;
            textBoxMonth.Width = textBoxWidth;
            textBoxYear.Width = textBoxWidth;
        }
        public void SetIfNoDayInput (int ifNoDayInput)
        {
            noIntDay = ifNoDayInput;
        }
        public void SetIfNoMonthInput(int ifNoMonthInput)
        {
            noIntMonth = ifNoMonthInput;
        }
        public void SetIfNoYearInput(int ifNoYearInput)
        {
            noIntYear = ifNoYearInput;
        }
    }
}
