using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blocknote
{
    public partial class MasterKey : Form
    {
        public string Result;
        public MasterKey()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Result = textBox1.Text;
            Close();
        }
    }
}
