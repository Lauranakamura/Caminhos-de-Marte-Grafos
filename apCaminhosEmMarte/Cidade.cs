using System;
using System.Collections.Generic;
using System.IO;

public class Cidade : IRegistro<Cidade>,
                      IComparable<Cidade>
{
    // atributos que formam uma linha do arquivo de cidades
    public string nome;
    public double x, y;

    public Cidade() { }  // construtor default

    public string Chave => this.nome;
    public string Nome { get => nome; set => nome = value; }
    public double X { get => x; set => x = value; }
    public double Y { get => y; set => y = value; }

    public override bool Equals(object obj) {
        return obj is Cidade cidade && Chave == cidade.Chave;
    }

    public Cidade LerRegistro(StreamReader arquivo)
    {
        if (arquivo != null)  // está aberto
        {
            string linha = arquivo.ReadLine(); // lê uma linha
            nome = linha.Substring(0, 15);  // (inicio, quantos)
            nome = nome.Trim();
            x = double.Parse(linha.Substring(15, 7));
            y = double.Parse(linha.Substring(22, 7));
            return this;
        }
        return default(Cidade);  // para arquivo não aberto
    }
    public void EscreverRegistro(StreamWriter arquivo)
    {
        if (arquivo != null)
        {
            arquivo.WriteLine($"{nome.PadRight(15)}{x:0.00000}{y:0.00000}");
        }
    }
    public int CompareTo(Cidade outra)  // <0, ==0, >0
    {
        return this.nome.CompareTo(outra.nome);
    }
    
}

