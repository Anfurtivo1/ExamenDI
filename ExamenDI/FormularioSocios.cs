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
    public partial class FormularioSocios : Form
    {
        public FormularioSocios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (clubraquetaEntities objDB = new clubraquetaEntities())
            {
                //Se busca la materia prima que sea igual que la del textox
                var consulta1 = from socios in objDB.socios
                                where socios.DNI.ToUpper().Trim() == txtDNI.Text.ToUpper().Trim()
                                select
                                new { socios.DNI };


                //Si se ha encontrado, es que existe
                if (consulta1.ToList().Count > 0)
                {
                    MessageBox.Show("Ya existe un socio con ese DNI");
                }
                else
                {
                    socios socio = new socios();
                    socio.DNI = txtDNI.Text.Trim();
                    socio.nombre = txtNombre.Text.Trim();
                    socio.apellidos = txtApellidos.Text.Trim();
                    socio.domicilio = txtDomicilio.Text.Trim();
                    socio.email = txtEmail.Text.Trim();
                    socio.cuentaCorriente = txtCuenta.Text.Trim();
                    socio.telefono = txtTelefono.Text.Trim();

                    objDB.socios.Add(socio);

                    objDB.SaveChanges();
                    MessageBox.Show("Usuario insertado correctamente");

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using(clubraquetaEntities objDB = new clubraquetaEntities())
            {
                string DNI = txtDNI.Text.Trim();

                socios socio = objDB.socios.First(x => x.DNI.Equals(DNI));
                socio.DNI = txtDNI.Text.Trim();
                socio.nombre = txtNombre.Text.Trim();
                socio.apellidos = txtApellidos.Text.Trim();
                socio.domicilio = txtDomicilio.Text.Trim();
                socio.email = txtEmail.Text.Trim();
                socio.cuentaCorriente = txtCuenta.Text.Trim();
                socio.telefono = txtTelefono.Text.Trim();

                objDB.SaveChanges();
                MessageBox.Show("Usuario modificado correctamente");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (clubraquetaEntities objDB = new clubraquetaEntities())
                {
                    string DNI = txtDNI.Text.Trim();

                    socios socio = objDB.socios.First(x => x.DNI.Equals(DNI));
                    objDB.socios.Remove(socio);

                    objDB.SaveChanges();
                    MessageBox.Show("Usuario eliminado correctamente");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar ese registro porque esta asociado a un producto o no existe", "Error al eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (clubraquetaEntities objDB = new clubraquetaEntities())
            {
                var consulta1 = from socios in objDB.socios
                                select
                                new { socios.DNI,socios.nombre,socios.apellidos,socios.domicilio,socios.telefono,socios.email,socios.cuentaCorriente };

                dgvSocios.DataSource = consulta1.ToList();
            }

            

        }

        private void FormularioSocios_Load(object sender, EventArgs e)
        {

        }
    }
}
