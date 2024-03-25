using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace apCaminhosEmMarte
{
  public partial class FrmCaminhos : Form
  {

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
      if (dlgAbrir.ShowDialog() == DialogResult.OK)
      {
        if (rbBucketHash.Checked) 
          tabelaDeHash = new BucketHash<Cidade>();
        else
          if (rbSondagemLinear.Checked)
             tabelaDeHash = new HashLinear<Cidade>();
          else
            if (rbSondagemQuadratica.Checked)
               tabelaDeHash = new HashQuadratico<Cidade>();
            else
              tabelaDeHash = new HashDuplo<Cidade>();

        // abrimos o arquivo escolhido
        var asCidades = new StreamReader(dlgAbrir.FileName);
        // ler registros do arquivo aberto
        while (!asCidades.EndOfStream)
        {
                   
           // instanciar um objeto cidade
            // lê-lo do arquivo para preencher seus atributos
        
                    // armazenar esse objeto na tabela de Hash
                    // de acordo com a técnica de hash escolhida
                    // pelo usuário
                }
                // Desenhar os nomes das cidades no mapa de Marte
                asCidades.Close();  // deixar arquivo fechado
      }
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
