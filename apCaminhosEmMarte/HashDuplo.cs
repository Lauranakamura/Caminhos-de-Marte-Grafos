using System;
using System.Collections;
using System.Collections.Generic;

public class HashDuplo<Tipo> : ITabelaDeHash<Tipo>
  where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{
    private readonly int tamanho = 131;
    private readonly Tipo[] tabela;

    public HashDuplo()
    {
        this.tabela = new Tipo[tamanho];
    }

    public List<string> Conteudo()
    {
        List<string> conteudo = new List<string>();

        for (int i = 0; i < tamanho; i++)
        {
            if (tabela[i] != null)
            {
                conteudo.Add(tabela[i].Chave);  // Adiciona a chave do elemento à lista
            }
        }

        return conteudo;
    }

    public bool Existe(Tipo item, out int onde)
    {
        int hash = Hash(item.Chave);             // Obtém o hash do item
        int incremento = Incremento(item.Chave); // Obtém o incremento
        
        onde = hash;                             // A posição inicial é o hash do item

        while (tabela[onde] != null)
        {
            if (tabela[onde].Equals(item))
            {
                return true;  // Se o item for encontrado, retorna true
            }

            onde = (onde + incremento) % tamanho; // Calcula a próxima posição usando o incremento
        }

        return false;// Caso o item nao seja encontrado, retorna false
    }

     
    public int Hash(string chave)
    {
        long tot = 0;

        for (int i = 0; i < chave.Length; i++)
            tot += 37 * tot + (char)chave[i];

        tot = tot % tabela.Length;
        if (tot < 0)
            tot += tabela.Length;

        return (int)tot;
    }

    public void Inserir(Tipo item)
    {
        int onde;
        if (!Existe(item, out onde)) // Verifica se o item já existe na tabela
        {
            tabela[onde] = item; // Insere o item na posição calculada
        }
    }

    public bool Remover(Tipo item)
    {
        int onde;
        if (Existe(item, out onde)) // Verifica se o item existe na tabela
        {
            tabela[onde] = default(Tipo); //remove o item da posicao
            return true;                  //retorna true se o item foi encontrado, caso contrario, retorna false
        }
        return false;                     
    }

    

    private int Incremento(string chave)
    {
        int hashSecundario = 7 - (Hash(chave) % 7); // calculo do hash secundario
        return hashSecundario;
    }

    public Tipo Dado(string chave)
    {
        int pos = Hash(chave);    // Calcula a posição na tabela usando o hash da chave
        Tipo dado = tabela[pos];  // Obtém o item na posição calculada
        if (dado == null)
            throw new Exception("Não foi possivel resgatar dado!");

        return dado;              // Retorna o item encontrado
    }

    public List<Tipo> ConteudoTipo()
    {
        List<Tipo> conteudo = new List<Tipo>();

        for (int i = 0; i < tamanho; i++)// Percorre a lista
        {
            if (tabela[i] != null)
                conteudo.Add(tabela[i]); // Adiciona o item à lista
        }
        return conteudo;                 // Retorna a lista de itens
    }
}

