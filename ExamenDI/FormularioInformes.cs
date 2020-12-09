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
                

                var consultaReservas = from reservas in objDB.reservas
                                       group reservas by reservas.idReserva into grupo
                                       select
                                       new {
                                           IdReservas = grupo.Key,
                                           NumVeces = grupo.Count()
                                       };


                var consultaSocios = from socios in objDB.socios
                                     from cantidadSocios in consultaReservas.ToList()

                                     select
                                     new { socios.nombre, socios.apellidos };


                //socio.DNI = txtDNI.Text.Trim();
                //socio.nombre = txtNombre.Text.Trim();
                //socio.apellidos = txtApellidos.Text.Trim();
                //socio.domicilio = txtDomicilio.Text.Trim();
                //socio.email = txtEmail.Text.Trim();
                //socio.cuentaCorriente = txtCuenta.Text.Trim();
                //socio.telefono = txtTelefono.Text.Trim();
            }
        }
    }
}
