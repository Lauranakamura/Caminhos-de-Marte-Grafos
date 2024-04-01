using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Security.Cryptography;

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
                    arrayCidade = new ArrayList();

                    var arqCidades = new StreamReader(dlgAbrir.FileName);

                    while (!arqCidades.EndOfStream)
                    {
                        Cidade cidade = new Cidade().LerRegistro(arqCidades);
                        tabelaDeHash.Inserir(cidade);
                        lsbListagem.Items.Add(cidade.nome);
                    }
                    arqCidades.Close();
                    pbMapa.Invalidate();
                }
                else
                {
                    MessageBox.Show("Nenhum arquivo foi seleciondo, portanto o programa será finalizado!",
                                    "Saindo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }

            }
            else if (rbSondagemLinear.Checked)
            {
                tabelaDeHash = new HashLinear<Cidade>();
            }
            else if (rbSondagemQuadratica.Checked)
            {
                //tabelaDeHash = new HashQuadratico<Cidade>();
            }
            //else
            //tabelaDeHash = new HashDuplo<Cidade>();
        }

        private void FrmCaminhos_FormClosing(object sender, FormClosingEventArgs e)
        {
            // aqui, a tabela de hash deve ser percorrida e os 
            // registros armazenados devem ser gravados no arquivo
            // agora, aberto para saída (StreamWriter).
        }

        private void lsbListagem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

