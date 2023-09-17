// CRUD = Criar, ler, atualizar e deletar 

class Program
{
    static List<Moto> motos = new List<Moto>();
    static string nomeDoArquivo = "tasks.txt";

    static void Main(string[] args)
    {
        if (File.Exists(nomeDoArquivo))                                                  // File faz parte da familia IO System 
        {
            string[] linhas = File.ReadAllLines(nomeDoArquivo);

            foreach (string linha in linhas)
            {
                string[] partes = linha.Split('|');

                if (partes.Length == 4)
                {
                    string modelo = partes[0];
                    string cor = partes[1];

                    if (double.TryParse(partes[2], out double valor))
                    {
                        if (int.TryParse(partes[3], out int cilindrada))
                        {
                            motos.Add(new Moto { Modelo = modelo, Cor = cor, Valor = valor, Cilindrada = cilindrada });
                        }
                        else
                        {
                            Console.WriteLine("Erro na conversão de cilindrada para int.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro na conversão de valor para double.");
                    }
                }
            }
        }

        while (true)
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1. Adicionar nova moto");
            Console.WriteLine("2. Listar motos");
            Console.WriteLine("3. Editar uma moto");
            Console.WriteLine("4. Deletar uma moto");
            Console.WriteLine("5. Salvar e Sair");
            Console.WriteLine("");                                       // Para pular uma linha na hora de digitar 

            string escolha = Console.ReadLine();                     // Imprimindo o que é para ser lido 

            switch (escolha)                                        // Qual vai escholer primeiro 1,2.3....)
            {
                case "1":
                    AdicionaMoto();
                    break;
                case "2":
                    ListarMotos();
                    break;
                case "3":
                    EditarMoto();
                    break;
                case "4":
                    DeletarMoto();
                    break;
                case "5":
                    SalvarAMotoNoArquivo();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");    // Se caso tentar aperta o número 8 para cima vai dar inválido 
                    break;
            }
        }
    }

    static void AdicionaMoto()
    {
        Console.WriteLine("Digite o modelo da moto:");
        string modelo = Console.ReadLine();

        Console.WriteLine("Digite a cor da moto:");
        string cor = Console.ReadLine();

        Console.WriteLine("Digite a cilindrada da moto");
        int cilindrada = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Digite o valor da moto");
        double valor = Convert.ToDouble(Console.ReadLine());

        Moto novaMoto = new Moto { Modelo = modelo, Cor = cor, Cilindrada = cilindrada, Valor = valor };
        motos.Add(novaMoto);

        Console.WriteLine("Moto criada com sucesso:");
    }

    static void ListarMotos()
    {
        Console.WriteLine("Lista de moto:");
        int index = 1;                                // Index vai ser um valor que sempre recebe 1

        foreach (Moto moto in motos)
        {
            Console.WriteLine($"{index}. Modelo: {moto.Modelo}, Cor: {moto.Cor}, Cilindrada: {moto.Cilindrada}, Valor: {moto.Valor}");
            index++;
        }
    }

    static void EditarMoto()
    {
        Console.WriteLine("Digite o número da moto que deseja editar:");

        if (int.TryParse(Console.ReadLine(), out int motoSelecionada) && motoSelecionada >= 1 && motoSelecionada <= motos.Count)
        {
            Console.WriteLine("Digite o modelo da moto:");
            string modelo = Console.ReadLine();

            Console.WriteLine("Digite a cor da moto:");
            string cor = Console.ReadLine();

            Console.WriteLine("Digite a cilindrada da moto");
            int cilindrada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o valor da moto");
            double valor = Convert.ToDouble(Console.ReadLine());

            Moto motoAtualizada = motos[motoSelecionada - 1];
            motoAtualizada.Modelo = modelo;
            motoAtualizada.Cor = cor;
            motoAtualizada.Cilindrada = cilindrada;
            motoAtualizada.Valor = valor;

            Console.WriteLine("Modelo atualizada com sucesso!");
        }
        else
        {
            Console.WriteLine("Número da moto inválido.");
        }
    }

    static void DeletarMoto()
    {
        Console.WriteLine("Digite o número da moto que desejas excluir:");

        if (int.TryParse(Console.ReadLine(), out int numeroDaPeça) && numeroDaPeça >= 1 && numeroDaPeça <= motos.Count)
        {
            Moto EditarMoto = motos[numeroDaPeça - 1];
            motos.Remove(EditarMoto);

            Console.WriteLine("Moto excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Número da  inválido.");
        }
    }

    static void SalvarAMotoNoArquivo()
    {
        using (StreamWriter writer = new StreamWriter(nomeDoArquivo))
        {
            foreach (Moto moto in motos)
            {
                writer.WriteLine($"{moto.Modelo}|{moto.Cor}|{moto.Cilindrada}|{moto.Valor}");
            }
        }
    }
}

class Moto
{
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public double Valor { get; set; }
    public int Cilindrada { get; set; }
}

