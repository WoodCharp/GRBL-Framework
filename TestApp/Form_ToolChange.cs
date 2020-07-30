using System;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form_ToolChange : Form
    {
        public string ToolInfo { get; set; }

        public Form_ToolChange()
        {
            InitializeComponent();
        }

        private void Form_ToolChange_Load(object sender, EventArgs e)
        {
            label_id.Text = ToolInfo;
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
