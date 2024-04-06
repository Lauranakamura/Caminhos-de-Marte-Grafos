using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Policy;

public class HashQuadratico<Tipo> : ITabelaDeHash<Tipo>
  where Tipo : IComparable<Tipo>, IRegistro<Tipo>
{
    private const int tamanho = 131;
    private Tipo[] info;

    public HashQuadratico() { info = new Tipo[tamanho]; }

    public int Hash(string chave) {
        long tam = 0;

        for (int i = 0; i < chave.Length; i++)
            tam += 37 * tam + (char)chave[i];

        tam = tam % info.Length;

        if (tam < 0)
            tam += info.Length;

        return (int)tam;
    }

    public List<string> Conteudo() {
        List<string> conteudo = new List<string>();

        for (int i = 0; i < tamanho; i++) {
            if (info[i] != null)
                conteudo.Add(info[i].Chave);
        }
        return conteudo;
    }

    public bool Existe(Tipo item, out int onde) {
        int valorDeHash = Hash(item.Chave);
        int i = valorDeHash;
        int aux = 0;

        while (info[i] != null && !info[i].Equals(item)) {
            i = (valorDeHash + (aux * aux)) % tamanho;
            aux++;
            if (aux >= tamanho) {
                onde = -1;
                return false;
            }
        }
        if (info[i] != null && info[i].Equals(item)) {
            onde = i;
            return true;
        }
        else {
            onde = -1;
            return false;
        }
    }

    public void Inserir(Tipo item) {
        int valorDeHash = Hash(item.Chave);
        int i = valorDeHash;

        int aux = 0;

        while(info[i] != null) {
            i = (valorDeHash + (aux * aux)) % tamanho;
            aux++;
        }
        info[i] = item;
    }

    public bool Remover(Tipo item) {
        int indice = -1;
        for (int i = 0; i < tamanho; i++) {
            if (info[i] != null && info[i].Equals(item)) {
                indice = i;
                break;
            }
        }
        if (indice != -1) {
            info[indice] = default;
            return true;
        }
        else {
            return false;
        }
    }

    public Tipo Dado(string chave) {
        int pos = Hash(chave);
        Tipo dado = info[pos];
        if (dado == null)
            throw new Exception("Não foi possivel resgatar dado!");
        
        return dado;
    }
}

