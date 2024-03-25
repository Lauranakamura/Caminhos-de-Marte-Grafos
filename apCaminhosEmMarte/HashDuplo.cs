using System;
using System.Collections;
using System.Collections.Generic;

public class HashDuplo<Tipo> : ITabelaDeHash<Tipo>
  where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{
    private readonly int tamanho;
    private readonly Tipo[] tabela;

    public HashDuplo(int tamanho)
    {
        this.tamanho = tamanho;
        this.tabela = new Tipo[tamanho];
    }

    public List<string> Conteudo() // O for está errado
    {
        List<string> conteudo = new List<string>();

        for (int i = 0; i == tamanho; i++)
        {
            if (tabela[i] != null)
            {
                conteudo.Add(tabela[i].ToString());
            }
        }

        return conteudo;
    }

    public bool Existe(Tipo item, out int onde)
    {
        int hash = Hash(item.Chave);
        int incremento = Incremento(item.Chave);
        onde = hash;

        while (tabela[onde] != null)
        {
            if (tabela[onde].Equals(item))
            {
                return true;
            }

            onde = (onde + incremento) % tamanho;
        }

        return false;
    }

    public int Hash(string chave)
    {
        int hash = 0;
        foreach (char c in chave)
        {
            hash = (hash + c) % tamanho;
        }
        return hash;
    }

    public void Inserir(Tipo item)
    {
        int onde;
        if (!Existe(item, out onde))
        {
            tabela[onde] = item;
        }
    }

    public bool Remover(Tipo item)
    {
        int onde;
        if (Existe(item, out onde))
        {
            tabela[onde] = default(Tipo);
            return true;
        }
        return false;
    }

    List<string> ITabelaDeHash<Tipo>.Conteudo()
    {
        throw new NotImplementedException();
    }

    private int Incremento(string chave)
    {
        int hashSecundario = 7 - (Hash(chave) % 7);
        return hashSecundario;
    }
}

