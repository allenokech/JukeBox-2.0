using System;
using System.Windows.Forms;

namespace JukeBox
{
    public partial class AboutForm : Form
    {
    
        public AboutForm()
        {
            InitializeComponent();
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
