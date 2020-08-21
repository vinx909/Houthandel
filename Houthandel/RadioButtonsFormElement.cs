using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glashandel
{
    public class RadioButtonsFormElement : FormElement
    {
        private const string exceptionNoOptionSelected = "no radio option selected";

        private int rowHeight;

        private GroupBox group;
        private RadioButton[] radioButtons;

        private string[] options;

        public RadioButtonsFormElement(Form form, string[] options, int rowHeight) : base(form)
        {
            this.options = CopyStringArray(options);
            group = new GroupBox();
            radioButtons = new RadioButton[options.Length];
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i] = new RadioButton();
                group.Controls.Add(radioButtons[i]);
                radioButtons[i].Text = this.options[i];
                form.Controls.Add(radioButtons[i]);
            }

            this.rowHeight = rowHeight;
        }
        protected override void AlterPosition(int widthOfset, int heightOfset)
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].Location = new System.Drawing.Point(widthOfset, heightOfset + rowHeight * i);
            }
        }
        public string GetValue()
        {
            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked == true)
                {
                    return radioButton.Text;
                }
            }
            throw new Exception(exceptionNoOptionSelected);
        }
        public void SetRowHeight(int height)
        {
            rowHeight = height;
        }
        public int GetAmountOfRows()
        {
            return radioButtons.Length;
        }
        private string[] CopyStringArray(string[] toCopy)
        {
            string[] toReturn = new string[toCopy.Length];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = toCopy[i];
            }
            return toReturn;
        }
    }
}
