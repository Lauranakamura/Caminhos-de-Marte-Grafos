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
                conteudo.Add(tabela[i].Chave);
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

    

    private int Incremento(string chave)
    {
        int hashSecundario = 7 - (Hash(chave) % 7);
        return hashSecundario;
    }

    public Tipo Dado(string chave)
    {
        int pos = Hash(chave);
        Tipo dado = tabela[pos];
        if (dado == null)
            throw new Exception("Não foi possivel resgatar dado!");

        return dado;
    }

    public List<Tipo> ConteudoTipo()
    {
        List<Tipo> conteudo = new List<Tipo>();

        for (int i = 0; i < tamanho; i++)
        {
            if (tabela[i] != null)
                conteudo.Add(tabela[i]);
        }
        return conteudo;
    }
}

