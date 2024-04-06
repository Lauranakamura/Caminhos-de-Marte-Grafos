using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace apCaminhosEmMarte
{
    public partial class FrmCaminhos : Form
    {
        ITabelaDeHash<Cidade> tabelaDeHash;

        string nomeArq = null;
        public FrmCaminhos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            dlgAbrir.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                if (rbBucketHash.Checked)              tabelaDeHash = new BucketHash<Cidade>();
                else if (rbSondagemLinear.Checked)     tabelaDeHash = new HashLinear<Cidade>();
                else if (rbSondagemQuadratica.Checked) tabelaDeHash = new HashQuadratico<Cidade>();
                else if (rbHashDuplo.Checked)          tabelaDeHash = new HashDuplo<Cidade>();

                var arqCidades = new StreamReader(dlgAbrir.FileName);

                while (!arqCidades.EndOfStream) {
                    Cidade cidade = new Cidade();
                    cidade.LerRegistro(arqCidades);
                    tabelaDeHash.Inserir(cidade);
                }
                arqCidades.Close();
                pbMapa.Invalidate();
                nomeArq = dlgAbrir.FileName;

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
          
            if (nomeArq != null)
            {
                StreamWriter sw = new StreamWriter(nomeArq);

                foreach (Cidade item in tabelaDeHash.ConteudoTipo())
                {
                    item.EscreverRegistro(sw);
                }
                sw.Close();
            }
        }

        private void lsbListagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            float escalaElipse = 8f; 
            if (tabelaDeHash != null)
            {
                Graphics g = e.Graphics;
                foreach (Cidade cidade in tabelaDeHash.ConteudoTipo())
                {
                    double x = (cidade.X * pbMapa.Width); // Largura original do mapa
                    double y = (cidade.Y * pbMapa.Height); // Altura original do mapa

                    Color corInicial = Color.Red;
                    Color corFinal   = Color.Yellow;

                    LinearGradientBrush brush = new LinearGradientBrush(
                        //new Point((int)(pbMapa.Width * cidade.X - escalaElipse / 2), (int)(pbMapa.Height * cidade.Y - escalaElipse / 2)),
                        //new Point((int)(pbMapa.Width * cidade.X + escalaElipse / 2), (int)(pbMapa.Height * cidade.Y + escalaElipse / 2)),

                        new Point((int)x - 3, (int)y - 3), 
                        new Point((int)x + 3, (int)y + 3),
                        corInicial,
                        corFinal
                    );

                    var pen = new Pen(Color.Black, 1);
                    var rec = new Rectangle((int)x - 3, (int)y - 3, 6, 6);
                    var font = new Font("Arial", 8f, FontStyle.Bold);

                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    g.FillEllipse(brush, rec);
                    g.DrawEllipse(pen, rec);
                    g.DrawString(cidade.nome, font, new SolidBrush(Color.Black), (int)x, (int)y - 10, sf);

                    //g.FillEllipse(brush, (float)(pbMapa.Width * cidade.X) - escalaElipse / 2,
                    //(float)(pbMapa.Height * cidade.Y) - escalaElipse / 2, escalaElipse, escalaElipse);

                    //g.DrawString(cidade.Nome, new Font("Arial", 8f, FontStyle.Bold), 
                    //    new SolidBrush(Color.Black),
                    //(float)(pbMapa.Width * cidade.X), (float)(pbMapa.Height * cidade.Y));
                }
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();

            cidade.nome = txtNome.Text;
            cidade.X = (double)udX.Value;
            cidade.Y = (double)udY.Value;

            try {
                tabelaDeHash.Inserir(cidade);
                pbMapa.Invalidate();
                Dados();
            }
            catch(Exception er) {
                MessageBox.Show($"Erro: {er.Message}");
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();

            cidade.nome = txtNome.Text;
            cidade.X = (double)udX.Value;
            cidade.Y = (double)udY.Value;

            if(tabelaDeHash.Remover(cidade)) {
                MessageBox.Show("Sucesso ao remover cidade do registro!");
                
                pbMapa.Invalidate();
                Dados();
            }
            else { MessageBox.Show("Falha ao remover cidade do registro;"); }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();

            try {
                cidade = tabelaDeHash.Dado(txtNome.Text);
                MessageBox.Show("Cidade localizada com sucesso!");

                udX.Value = (decimal)cidade.X;
                udY.Value = (decimal)cidade.Y;
            }
            catch(Exception er) {
                MessageBox.Show("Falha ao buscar cidade!");
            }
        }

        private void Dados()
        {
            lsbListagem.Items.Clear();
            var cidades = tabelaDeHash.Conteudo();

            foreach (string cidade in cidades)
            {
                lsbListagem.Items.Add(cidade);
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            Dados();
            pbMapa.Invalidate();
        }

        private void pbMapa_MouseUp(object sender, MouseEventArgs e)
        {
            udX.Value = (decimal)e.X / pbMapa.Width;
            udY.Value = (decimal)e.Y / pbMapa.Height;
        }
    }
}


