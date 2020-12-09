using System;
using System.Collections;
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
    public partial class FormularioReservas : Form
    {
        ArrayList DNISocios = new ArrayList();
        public FormularioReservas()
        {
            InitializeComponent();
        }

        private void FormularioReservas_Load(object sender, EventArgs e)
        {
            cargarComboSocios();
        }

        private void cargarComboSocios()
        {
            clubraquetaDataSet db = new clubraquetaDataSet();
            clubraquetaDataSetTableAdapters.sociosTableAdapter sociosTableAdapter = new clubraquetaDataSetTableAdapters.sociosTableAdapter();
            sociosTableAdapter.Fill(db.socios);

            for (int i = 0; i < db.socios.Count; i++)
            {
                cbSocios.Items.Add(db.socios[i].nombre+"-"+db.socios[i].apellidos);
                DNISocios.Add(db.socios[i].DNI);

            }
        }

        private void cbSocios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPanelSocios();
            cargarReservas();

        }

        private void cargarPanelSocios()
        {
            int pos = cbSocios.SelectedIndex;
            String DNI =(String) DNISocios[pos];

            clubraquetaDataSet db = new clubraquetaDataSet();
            clubraquetaDataSetTableAdapters.sociosTableAdapter sociosTableAdapter = new clubraquetaDataSetTableAdapters.sociosTableAdapter();
            sociosTableAdapter.FillByDNI(db.socios, DNI);

            lblDNI.Text = db.socios[0].DNI.ToString();
            txtNombre.Text = db.socios[0].nombre.ToString();
            txtApellido.Text = db.socios[0].apellidos.ToString();
            txtDireccion.Text = db.socios[0].domicilio.ToString();
            txtEmail.Text = db.socios[0].email.ToString();
            txtCCC.Text = db.socios[0].cuentaCorriente.ToString();
            txtTelefono.Text = db.socios[0].telefono.ToString();
        }

        private void cargarReservas()
        {

            

            clubraquetaDataSet db = new clubraquetaDataSet();
            clubraquetaDataSetTableAdapters.reservasTableAdapter reservasTableAdapter = new clubraquetaDataSetTableAdapters.reservasTableAdapter();
            

            try
            {
                String DNI = (String)DNISocios[cbSocios.SelectedIndex];
                reservasTableAdapter.FillBySocio(db.reservas, DNI);
                dgvReservas.DataSource = db.reservas;

                dgvReservas.Columns[0].ReadOnly = true;
                dgvReservas.Columns[1].ReadOnly = true;
                dgvReservas.Columns[2].ReadOnly = true;
                dgvReservas.Columns[3].ReadOnly = true;
                dgvReservas.Columns[4].ReadOnly = true;
                dgvReservas.Columns[5].ReadOnly = true;
                dgvReservas.Columns[6].ReadOnly = true;

            }
            catch
            {
                MessageBox.Show("Ese socio no tiene pistas reservadas");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvReservas.SelectedRows.Count>0)
            {
                clubraquetaDataSet db = new clubraquetaDataSet();
                clubraquetaDataSetTableAdapters.reservasTableAdapter reservasTableAdapter = new clubraquetaDataSetTableAdapters.reservasTableAdapter();

                String DNI = (String)DNISocios[cbSocios.SelectedIndex];
                reservasTableAdapter.FillBySocio(db.reservas, DNI);

                if (db.reservas[0].pagado.Equals("Si"))
                {
                    MessageBox.Show("Ya esta pagada esa reserva");
                }
                else
                {
                    db.reservas[0].pagado = "Si";
                    MessageBox.Show("Has pagado con exito la reserva");
                    dgvReservas.DataSource = db.reservas;
                    dgvReservas.Refresh();
                }

                

            }
            else
            {
                MessageBox.Show("Tienes que seleccionar una reserva que pagar");
            }
        }
    }
}
