class Program
{
    static void Main()
    {
        List<(int codigo, string nome, int quantidade, double preco)> estoque = new List<(int, string, int, double)>();

        while (true)
        {
            Console.WriteLine("1. Cadastrar Produto");
            Console.WriteLine("2. Consultar Produto");
            Console.WriteLine("3. Atualizar Estoque");
            Console.WriteLine("4. Relatórios");
            Console.WriteLine("5. Sair");

            int opcao = int.Parse(Console.ReadLine());
            

            switch (opcao)
            {
                case 1:
                    CadastrarProduto(estoque);
                    break;
                case 2:
                    ConsultarProduto(estoque);
                    break;
                case 3:
                    AtualizarEstoque(estoque);
                    break;
                case 4:
                    GerarRelatorios(estoque);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }

    static void CadastrarProduto(List<(int, string, int, double)> estoque)
    {
        try
        {
            Console.WriteLine("Informe o código do produto:");
            int codigo = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o nome do produto:");
            string nome = Console.ReadLine();

            Console.WriteLine("Informe a quantidade em estoque:");
            int quantidade = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o preço unitário:");
            double preco = double.Parse(Console.ReadLine());

            estoque.Add((codigo, nome, quantidade, preco));

            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar produto: {ex.Message}");
        }
    }

    static void ConsultarProduto(List<(int, string, int, double)> estoque)
    {
        try
        {
            Console.WriteLine("Informe o código do produto:");
            int codigo = int.Parse(Console.ReadLine());

            var produto = estoque.FirstOrDefault(p => p.Item1 == codigo);

            if (produto.Equals(default((int, string, int, double))))
            {
                throw new ProdutoNaoEncontradoException("Produto não encontrado.");
            }

            Console.WriteLine($"Código: {produto.Item1}, Nome: {produto.Item2}, Quantidade: {produto.Item3}, Preço: {produto.Item4}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar produto: {ex.Message}");
        }
    }

    static void AtualizarEstoque(List<(int, string, int, double)> estoque)
    {
        try
        {
            Console.WriteLine("Informe o código do produto:");
            int codigo = int.Parse(Console.ReadLine());

            var produto = estoque.FirstOrDefault(p => p.Item1 == codigo);

            if (produto.Equals(default((int, string, int, double))))
            {
                throw new ProdutoNaoEncontradoException("Produto não encontrado.");
            }

            Console.WriteLine("Informe a quantidade de entrada (positiva) ou saída (negativa):");
            int quantidadeMovimentacao = int.Parse(Console.ReadLine());

            if (produto.Item3 + quantidadeMovimentacao < 0)
            {
                throw new EstoqueInsuficienteException("Quantidade insuficiente em estoque para a saída desejada.");
            }

            produto.Item3 += quantidadeMovimentacao;

            Console.WriteLine("Estoque atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar estoque: {ex.Message}");
        }
    }

    static void GerarRelatorios(List<(int, string, int, double)> estoque)
    {
        Console.WriteLine("Informe o limite de quantidade para o relatório 1:");
        int limiteQuantidade = int.Parse(Console.ReadLine());

        var relatorio1 = estoque.Where(p => p.Item3 < limiteQuantidade);
        Console.WriteLine("Relatório 1: Produtos com quantidade em estoque abaixo do limite:");
        ImprimirRelatorio(relatorio1);

        Console.WriteLine("Informe o valor mínimo para o relatório 2:");
        double minValor = double.Parse(Console.ReadLine());
        Console.WriteLine("Informe o valor máximo para o relatório 2:");
        double maxValor = double.Parse(Console.ReadLine());

        var relatorio2 = estoque.Where(p => p.Item4 >= minValor && p.Item4 <= maxValor);
        Console.WriteLine("Relatório 2: Produtos com valor entre o mínimo e o máximo:");
        ImprimirRelatorio(relatorio2);

        double valorTotalEstoque = estoque.Sum(p => p.Item3 * p.Item4);
        Console.WriteLine($"Relatório 3: Valor total do estoque: {valorTotalEstoque:C}");
    }

    static void ImprimirRelatorio(IEnumerable<(int, string, int, double)> relatorio)
    {
        foreach (var produto in relatorio)
        {
            Console.WriteLine($"Código: {produto.Item1}, Nome: {produto.Item2}, Quantidade: {produto.Item3}, Preço: {produto.Item4}");
        }
    }
}

class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}

class EstoqueInsuficienteException : Exception
{
    public EstoqueInsuficienteException(string mensagem) : base(mensagem)
    {
    }
}