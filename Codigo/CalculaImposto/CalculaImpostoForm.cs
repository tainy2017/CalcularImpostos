using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalculaImposto
{
    public partial class FrmCalculaImposto : Form
    {
        String caminho;
        public FrmCalculaImposto()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (openFileDialogNfe.ShowDialog() == DialogResult.OK)
            {
                textBoxFile.Text = openFileDialogNfe.FileName;
            }
        }

        private void BtnBuscarNfe_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivo zip | *.zip";
            openFileDialog.DefaultExt = "zip";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
                caminho = textBox1.Text;

                if (string.IsNullOrEmpty(openFileDialog.FileName) == false)
                {
                    try
                    {

                        LerZipEExibirGrid(caminho);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Não foi possível abrir o arquivo. Erro: {0}", ex.Message), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LerZipEExibirGrid(string caminho)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(caminho))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        GetDados();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(String.Format("Não foi possível abrir o arquivo. Erro: {0}", ex.Message), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDados()
        {
            try
            {
                DataSet dsResultado = new DataSet();
                dsResultado.ReadXml(caminho);

                if (dsResultado.Tables.Count != 0)
                {
                    if (dsResultado.Tables[0].Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dsResultado.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
