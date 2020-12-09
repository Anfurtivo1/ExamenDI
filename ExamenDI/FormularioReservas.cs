using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenDI
{
    public partial class FormularioReservas : Form
    {
        ArrayList DNISocios = new ArrayList();
        ArrayList idPistas = new ArrayList();
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
            cargarComboPistas();
            habilitarElementosPanelLista();

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

        private void cbPista_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarImagenPista();
            cargarPanelPista();
        }

        private void cargarImagenPista()
        {

            clubraquetaDataSet db = new clubraquetaDataSet();
            clubraquetaDataSetTableAdapters.pistasTableAdapter pistasTableAdapter = new clubraquetaDataSetTableAdapters.pistasTableAdapter();
            pistasTableAdapter.Fill(db.pistas);

            // Una vez ejecutada la consulta sobre la tabla pistas, y cargado el DataSet correspondiente
            //1º Se comprueba si en esa tabla devuelta con la consulta sobre las pistas hay algún registro
            if (db.pistas.Rows.Count > 0)
            {

                //    //Inicializamos una fila de datos en la cual se almacenaran todos los datos de la fila seleccionada
                //    //En este ejemplo nos quedamos con la primera

                try
                {
                    DataRow myRow = db.pistas.Rows[0];
                    //Se almacena el campo foto de la tabla en el array de bytes
                    byte[] MyData = (byte[])myRow["foto"];
                    //Se inicializa un flujo en memoria del array de bytes
                    MemoryStream stream = new MemoryStream(MyData);
                    //En el picture box se muestra la imagen que esta almacenada en el flujo en memoria 
                    //el cual contiene el array de bytes
                    pbFotoPista.Image = Image.FromStream(stream);
                }
                catch
                {
                    MessageBox.Show("Error al cargar la imagen, no se ha encontrado una imagen valida");
                }
            }
        }

        private void cargarComboPistas()
        {
            idPistas.Clear();
            cbPista.Items.Clear();
            clubraquetaDataSet db = new clubraquetaDataSet();
            clubraquetaDataSetTableAdapters.pistasTableAdapter sociosTableAdapter = new clubraquetaDataSetTableAdapters.pistasTableAdapter();
            sociosTableAdapter.Fill(db.pistas);

            for (int i = 0; i < db.pistas.Count; i++)
            {
                cbPista.Items.Add(db.pistas[i].nombre);
                idPistas.Add(db.pistas[i].idPista);

            }
        }

        private void habilitarElementosPanelLista()
        {
            cbPista.Enabled = true;
            dtpFecha.Enabled = true;
            nudHora.Enabled = true;
            nupMinuto.Enabled = true;
            button2.Enabled = true;
        }

        private void cargarPanelPista()
        {

            clubraquetaDataSet db = new clubraquetaDataSet();

            clubraquetaDataSetTableAdapters.reservasTableAdapter reservasTableAdapter = new clubraquetaDataSetTableAdapters.reservasTableAdapter();
            reservasTableAdapter.Fill(db.reservas);

            //dtpFecha = db.reservas[0].fecha;
            //nudHora.Value = db.reservas[0].hora;

            }
        
        }

    }
