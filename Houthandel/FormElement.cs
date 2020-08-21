using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glashandel
{
    public abstract class FormElement
    {
        protected Form form;

        public FormElement(Form form)
        {
            this.form = form;
        }
        public void ChangePosition(int widthOfset, int heightOfset)
        {
            AlterPosition(widthOfset, heightOfset);
        }
        abstract protected void AlterPosition(int widthOfset, int heightOfset);
    }
}
