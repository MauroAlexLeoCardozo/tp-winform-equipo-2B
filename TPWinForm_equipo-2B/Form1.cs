using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using dominio;
using System.Threading;

namespace TPWinForm_equipo_2B
{
    public partial class frmInicio : Form
    {
        private List<dominio.Articulo> listaArticulos;
        private List<dominio.Imagen> listaImagenes;
        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            listaArticulos = articulo.listar();
            ImagenNegocio img = new ImagenNegocio();
            listaImagenes = img.listar();

            dgvArticulo.DataSource = listaArticulos;
            dgvArticulo.Columns["id"].Visible = false;
            dgvArticulo.Columns["img"].Visible = false;
            
            
        }

        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarGaleria();
        }

        private void ActualizarGaleria()
        {
            flpImagenes.Controls.Clear();

            if (dgvArticulo.CurrentRow == null) return;

            Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;

            foreach (Imagen img in listaImagenes)
            {
                if (img.idArticulo == seleccionado.id)
                {

                    PictureBox pbx = new PictureBox();
                    pbx.Size = new Size(150, 150);
                    pbx.SizeMode = PictureBoxSizeMode.StretchImage;

                    try
                    {
                        pbx.Load(img.imgUrl);
                    }
                    catch (Exception)
                    {

                        pbx.Load("https://r1.community.samsung.com/t5/image/serverpage/image-id/4337293iF6DBBABC2515A2AF/image-size/large?v=v2&px=999");
                    }


                    flpImagenes.Controls.Add(pbx);
                }
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
        }
    }
}