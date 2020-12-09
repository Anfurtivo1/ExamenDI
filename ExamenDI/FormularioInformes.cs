using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenDI
{
    public partial class FormularioInformes : Form
    {
        public FormularioInformes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (clubraquetaDataSet objDB = new clubraquetaDataSet())
            {

            }
        }
    }
}
