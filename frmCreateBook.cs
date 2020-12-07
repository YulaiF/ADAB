using System;
using System.Windows.Forms;

namespace ADAB
{
    public partial class frmCreateBook : Form
    {
        public string ReturnValue { get; private set; }
        public string suggestedBookName { get; set; }

        public frmCreateBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.ReturnValue = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void frmCreateBook_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(suggestedBookName))
                textBox1.Text = suggestedBookName;
        }
    }
}
