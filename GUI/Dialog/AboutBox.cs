using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("O {0}", AssemblyData.Title);
            this.labelProductName.Text = AssemblyData.Product;
            this.labelVersion.Text = String.Format("Verze: {0}", AssemblyData.Version);
            this.labelCopyright.Text = AssemblyData.Copyright;
            this.labelCompanyName.Text = AssemblyData.Company;
            this.textBoxDescription.Text = AssemblyData.Description;
        }
    }
}
