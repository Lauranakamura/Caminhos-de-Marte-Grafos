using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace apCaminhosEmMarte
{
    public partial class FrmCaminhos : Form
    {

        ArrayList arrayCidade;
        ITabelaDeHash<Cidade> cidade;
        public FrmCaminhos()
        {
            InitializeComponent();
        }

        ITabelaDeHash<Cidade> tabelaDeHash;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            dlgAbrir.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                if (rbBucketHash.Checked)
                {
                    tabelaDeHash = new BucketHash<Cidade>();

                    var arqCidades = new StreamReader(dlgAbrir.FileName);

                    while (!arqCidades.EndOfStream)
                    {
                        Cidade cidade = new Cidade().LerRegistro(arqCidades);
                        tabelaDeHash.Inserir(cidade);
                        arrayCidade.Add(cidade); // Adiciona a cidade ao array para posterior uso
                        lsbListagem.Items.Add(cidade.nome);
                    }
                    arqCidades.Close();
                    pbMapa.Invalidate(); // Redesenha o mapa
                }
                else if (rbSondagemLinear.Checked)
                {
                    tabelaDeHash = new HashLinear<Cidade>();
                    foreach (Cidade cidade in arrayCidade)
                    {
                        tabelaDeHash.Inserir(cidade);
                    }
                }
                else if (rbSondagemQuadratica.Checked)
                {
                    //tabelaDeHash = new HashQuadratico<Cidade>();
                    //foreach (Cidade cidade in arrayCidade)
                    //{
                    //    tabelaDeHash.Inserir(cidade);
                    //}
                }
                else if (rbHashDuplo.Checked)
                {
                    //tabelaDeHash = new HashDuplo<Cidade>();
                    //foreach (Cidade cidade in arrayCidade)
                    //{
                    //    tabelaDeHash.Inserir(cidade);
                    //}
                }
            }
            else
            {
                MessageBox.Show("Nenhum arquivo foi selecionado, portanto o programa será finalizado!",
                                "Saindo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void FrmCaminhos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void lsbListagem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmCaminhos_Paint(object sender, PaintEventArgs e)
        {
            if (tabelaDeHash != null)
            {
                Graphics g = e.Graphics;
                foreach (Cidade cidade in arrayCidade)
                {
                    double x = (cidade.x * pbMapa.Width) / 4096; // Largura original do mapa
                    double y = (cidade.y * pbMapa.Height) / 2048; // Altura original do mapa

                    var brush = new SolidBrush(Color.Gray);
                    var pen = new Pen(Color.Black, 1);
                    var rec = new RectangleF((float)x - 3, (float)y - 3, 6, 6);
                    var font = new Font("Arial", 10);

                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    g.FillEllipse(brush, rec);
                    g.DrawEllipse(pen, rec);
                    g.DrawString(cidade.nome, font, new SolidBrush(Color.Black), (float)x, (float)y - 10, sf);
                }
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            double x = Convert.ToDouble(udX.Value);
            double y = Convert.ToDouble(udY.Value);

            Cidade novaCidade = new Cidade();
            tabelaDeHash.Inserir(novaCidade);
            arrayCidade.Add(novaCidade);
            lsbListagem.Items.Add(novaCidade.nome);
            pbMapa.Invalidate();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lsbListagem.SelectedIndex != -1)
            {
                string nomeCidade = lsbListagem.SelectedItems.ToString();
                Cidade cidadeARemover = null;

                foreach (Cidade cidade in arrayCidade)
                {
                    if (cidade.nome == nomeCidade)
                    {
                        cidadeARemover = cidade;
                        break;
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (lsbListagem.SelectedIndex != -1)
            {
                string nomeCidade = lsbListagem.SelectedItems.ToString();
                Cidade cidadeABuscar = null;

                foreach (Cidade cidade in arrayCidade)
                {
                    if (cidade.nome == nomeCidade)
                    {
                        cidadeABuscar = cidade;
                        break;
                    }
                }
                if (cidadeABuscar != null)
                {
                    MessageBox.Show($"Cidade encontrada:\nNome: {cidadeABuscar.nome}\nX: {cidadeABuscar.x}\nY: {cidadeABuscar.y}",
                                    "Cidade Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cidade não encontrada.", "Cidade Não Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}


