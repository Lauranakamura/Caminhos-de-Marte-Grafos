using System;
using System.Collections.Generic;

public class HashQuadratico<Tipo> : ITabelaDeHash<Tipo>
  where Tipo : IComparable<Tipo>, IRegistro<Tipo>
{
  public List<string> Conteudo()
  {
    throw new NotImplementedException();
  }

  public bool Existe(Tipo item, out int onde)
  {
    throw new NotImplementedException();
  }

  public int Hash(string chave)
  {
    throw new NotImplementedException();
  }

  public void Inserir(Tipo item)
  {
    throw new NotImplementedException();
  }

  public bool Remover(Tipo item)
  {
    throw new NotImplementedException();
  }
}

