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
        ITabelaDeHash<Cidade> tabelaDeHash;
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
                //else if (rbHashDuplo.Checked)        tabelaDeHash = new HashDublo<Cidade>();

                var arqCidades = new StreamReader(dlgAbrir.FileName);

                while (!arqCidades.EndOfStream) {
                    Cidade cidade = new Cidade();
                    cidade.LerRegistro(arqCidades);
                    tabelaDeHash.Inserir(cidade);
                }
                arqCidades.Close();
                pbMapa.Invalidate(); 
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
            //string arq = null;
            //StreamWriter sw = new StreamWriter(arq);

            //if (arq != null) {
            //    foreach(Cidade item in tabelaDeHash.Conteudo()) {
            //        item.EscreverRegistro(sw);
            //    }
            //    sw.Close();
            //}
        }

        private void lsbListagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FrmCaminhos_Paint(object sender, PaintEventArgs e)
        {
            if (tabelaDeHash != null)
            {
                Graphics g = e.Graphics;
                foreach (Cidade cidade in tabelaDeHash.Conteudo())
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
            Cidade cidade = new Cidade();

            cidade.nome = txtNome.Text;
            cidade.x = (double)udX.Value;
            cidade.y = (double)udY.Value;

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
            cidade.x = (double)udX.Value;
            cidade.y = (double)udY.Value;

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

                udX.Value = (decimal)cidade.x;
                udY.Value = (decimal)cidade.y;
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
    }
}


