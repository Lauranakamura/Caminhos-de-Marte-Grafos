# 🔍 Caminho em Mapas com Grafos

Este projeto implementa um sistema de busca de caminhos em um mapa predefinido utilizando **grafos**. Os usuários podem definir pontos no mapa e conectar esses pontos através de arestas, permitindo encontrar o melhor caminho entre dois locais.

## 📌 Funcionalidades

- Criação de pontos personalizados no mapa.
- Conexão entre pontos através de arestas ponderadas.
- Busca do caminho mais curto utilizando algoritmos de grafos.
- Interface interativa via linha de comando.

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Paradigma:** Programação orientada a objetos
- **Estruturas de dados:** Listas, dicionários, grafos
- **Algoritmos:** Dijkstra e/ou A*

## 📦 Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. Compile e execute o projeto (usando .NET CLI):
   ```bash
   dotnet build
   dotnet run
   ```

## 📜 Como Usar

### Adicionar Pontos no Mapa
O usuário pode adicionar locais no mapa fornecendo um nome único para cada ponto.
```bash
AdicionarPonto "Casa"
AdicionarPonto "Supermercado"
```

### Conectar Pontos
Os pontos podem ser conectados através de uma aresta ponderada (distância ou custo).
```bash
ConectarPontos "Casa" "Supermercado" 5
```

### Encontrar o Melhor Caminho
Para calcular o caminho mais curto entre dois pontos:
```bash
BuscarCaminho "Casa" "Supermercado"
```
Saída esperada:
```
Caminho encontrado: Casa → Rua A → Supermercado (Distância: 5)
```

## 📖 Explicação do Algoritmo

O projeto utiliza um **grafo ponderado** para representar os pontos e suas conexões. Para encontrar o caminho mais eficiente, são usados algoritmos clássicos de grafos, como:

- **Dijkstra:** Calcula o caminho mais curto de um único ponto de origem para todos os outros.
- **A\*** (opcional): Um algoritmo heurístico que pode ser utilizado para otimizar a busca em mapas maiores.

## 📌 Exemplo Visual do Grafo

```
[Casa] ----5----> [Supermercado]
   |                     |
   2                     4
   |                     |
[Escola] ----3----> [Parque]
```

## 📜 Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

### 🔗 Contato
Caso tenha dúvidas ou sugestões, sinta-se à vontade para entrar em contato:
- 📧 Email: lauranakamuraml@gmail.com
- 🐙 GitHub: [Laura Nakamura](https://github.com/Lauranakamura)
