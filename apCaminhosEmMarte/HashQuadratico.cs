using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Policy;

public class HashQuadratico<Tipo> : ITabelaDeHash<Tipo>
  where Tipo : IComparable<Tipo>, IRegistro<Tipo>
{
    private readonly int tamanho;
    private readonly Tipo[] tabela;
    private readonly bool[] indicesOcupados;

    public HashQuadratico(int tamanho)
    {
        this.tamanho = tamanho;
        tabela = new Tipo[tamanho];
        indicesOcupados = new bool[tamanho];
    }
    public int Hash(string chave)
    {
        int somaValores = 0;

        foreach (char c in chave)
        {
            somaValores += (int)c;
        }

        return somaValores % tamanho;
    }

    public List<string> Conteudo()
    {
        List<string> conteudo = new List<string>();

        for (int i = 0; i < tamanho; i++)
        {
            if (indicesOcupados[i])
            {
                conteudo.Add(tabela[i].ToString());
            }
        }

        return conteudo;
    }

    public bool Existe(Tipo item, out int onde)
    {
        onde = Hash(item.Chave);
        int tentativas = 0;

        while (indicesOcupados[onde])
        {
            if (tabela[onde].Equals(item))
            {
                return true;
            }

            // Faz a sondagem quadrática
            onde = (onde + tentativas * tentativas) % tamanho;
            tentativas++;

            // Se tentou demais, retorna false
            if (tentativas >= tamanho)
            {
                return false;
            }
        }

        return false;
    }

    public void Inserir(Tipo item)
    {
        int onde;
        if (!Existe(item, out onde))
        {
            tabela[onde] = item;
            indicesOcupados[onde] = true;
        }
    }

    public bool Remover(Tipo item)
    {
        int onde;
        if (Existe(item, out onde))
        {
            tabela[onde] = default(Tipo);
            indicesOcupados[onde] = false;
            return true;
        }
        return false;
    }

    int ITabelaDeHash<Tipo>.Hash(string chave)
    {
        int hash = 0;
        foreach (char c in chave)
        {
            hash = (hash + c) % tamanho;
        }
        return hash;

    }
}

