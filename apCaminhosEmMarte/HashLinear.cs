using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HashLinear<Tipo> : ITabelaDeHash<Tipo>
      where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{

    private const int TamanhoTabela = 100; // Tamanho da tabela hash

    private Tipo[] tabela; // Armazena os itens da tabela hash
    private bool[] indicesOcupados; // Indica se um índice da tabela está ocupado ou não


    public HashLinear()
    {
        tabela = new Tipo[TamanhoTabela];
        indicesOcupados = new bool[TamanhoTabela];
    }


    public List<string> Conteudo()
    {
         throw new NotImplementedException();
    }

  public int Hash(string chave)
  {
        int somaValores = 0;

        foreach (char c in chave)
        {
            somaValores += (int)c;
        }

        return somaValores % TamanhoTabela;
    }

  bool ITabelaDeHash<Tipo>.Existe(Tipo item, out int onde)
  {
        onde = Hash(item.Chave);

        while (indicesOcupados[onde])
        {
            if (item.Chave == tabela[onde].Chave)
            {
                return true;
            }

            onde = (onde + 1) % TamanhoTabela; // Avança para o próximo índice usando uma estratégia de Hash Linear
        }

        return false;
    }

  void ITabelaDeHash<Tipo>.Inserir(Tipo item)
  {
        int onde;

        if (!((ITabelaDeHash<Tipo>)this).Existe(item, out onde))
        {
            tabela[onde] = item;
            indicesOcupados[onde] = true;
        }
    }

  bool ITabelaDeHash<Tipo>.Remover(Tipo item)
  {
        int onde;

        if (((ITabelaDeHash<Tipo>)this).Existe(item, out onde))
        {
            tabela[onde] = default(Tipo);
            indicesOcupados[onde] = false;
            return true;
        }

        return false;
    }
}

