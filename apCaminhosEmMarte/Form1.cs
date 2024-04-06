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

        string nomeArq = null; //arquivo que iremos utilizar
        public FrmCaminhos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            dlgAbrir.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; // filtro para facilitar o encontro do arquivo .txt
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                //seleção do tipo de Hash com base no RadioButton selecionado
                if (rbBucketHash.Checked)              tabelaDeHash = new BucketHash<Cidade>();
                else if (rbSondagemLinear.Checked)     tabelaDeHash = new HashLinear<Cidade>();
                else if (rbSondagemQuadratica.Checked) tabelaDeHash = new HashQuadratico<Cidade>();
                else if (rbHashDuplo.Checked)          tabelaDeHash = new HashDuplo<Cidade>();

                var arqCidades = new StreamReader(dlgAbrir.FileName); //abertura do arquivo

                while (!arqCidades.EndOfStream) { //é lida linha por linha do arquivo .txt selecionado  
                    Cidade cidade = new Cidade();
                    cidade.LerRegistro(arqCidades);
                    tabelaDeHash.Inserir(cidade);
                }
                arqCidades.Close(); //É fechado o arquivo apos a leitura 
                pbMapa.Invalidate(); //redesenha o mapa para exibir as atualizações(caso tenha)
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
          
            //Salva o arquivo modificado para que as alterações nao sejam perdidaas
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

                    //cores definidas para serem usadas no gradiente
                    Color corInicial = Color.Red;
                    Color corFinal   = Color.Yellow;

                    //É criado um gradiente de cor para a elipse
                    LinearGradientBrush brush = new LinearGradientBrush(
                        new Point((int)x - 3, (int)y - 3), 
                        new Point((int)x + 3, (int)y + 3),
                        corInicial,
                        corFinal
                    );

                    var pen = new Pen(Color.Black, 1);
                    var rec = new Rectangle((int)x - 3, (int)y - 3, 6, 6);
                    var font = new Font("Arial", 8f, FontStyle.Bold);

                    //É centralizado o texto 
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    g.FillEllipse(brush, rec);
                    g.DrawEllipse(pen, rec);
                    g.DrawString(cidade.nome, font, new SolidBrush(Color.Black), (int)x, (int)y - 10, sf);
                }
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            // Criamos uma nova cidade com os dados inseridos pelo usuário
            Cidade cidade = new Cidade();

            cidade.nome = txtNome.Text;
            cidade.X = (double)udX.Value;
            cidade.Y = (double)udY.Value;

            try {
                tabelaDeHash.Inserir(cidade); // Tentamos inserir a cidade na tabela de hash
                pbMapa.Invalidate();          // Redesenha o mapa para mostrar a nova cidade
                Dados();                      // Atualizamos a listagem das cidade
            }
            catch(Exception er) {
                MessageBox.Show($"Erro: {er.Message}"); // Se houver erro, mostramos uma mensagem
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();

            cidade.nome = txtNome.Text;
            cidade.X = (double)udX.Value;
            cidade.Y = (double)udY.Value;

            // Tentamos remover a cidade da tabela de hash
            if (tabelaDeHash.Remover(cidade)) {
                MessageBox.Show("Sucesso ao remover cidade do registro!");
                
                pbMapa.Invalidate(); //Atualiza o mapa para mostrar as atualizações
                Dados();             // Atualiza a listagem das cidades
            }
            else { MessageBox.Show("Falha ao remover cidade do registro;"); } // Se falhar, mostramos mensagem
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cidade cidade = new Cidade();

            try {
                cidade = tabelaDeHash.Dado(txtNome.Text); // Tentamos buscar a cidade na tabela de hash
                MessageBox.Show("Cidade localizada com sucesso!");

                // Exibimos as coordenadas da cidade encontrada
                udX.Value = (decimal)cidade.X;
                udY.Value = (decimal)cidade.Y;
            }
            catch(Exception er) {
                MessageBox.Show("Falha ao buscar cidade!"); // Se falhar, mostramos mensagem
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
            Dados();             // Atualizamos a listagem das cidades   
            pbMapa.Invalidate(); // Redesenha o mapa
        }

        private void pbMapa_MouseUp(object sender, MouseEventArgs e)
        {
            // Atualizamos as coordenadas com a posição onde o mouse foi solto
            udX.Value = (decimal)e.X / pbMapa.Width;
            udY.Value = (decimal)e.Y / pbMapa.Height;
        }
    }
}


