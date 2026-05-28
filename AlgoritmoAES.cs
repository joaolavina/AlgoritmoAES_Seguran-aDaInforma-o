public class AlgoritmoAES
{
    static void Main(string[] args)
    {
        int opcaoCriptografia = LerOpcao("Selecione a opção desejada:\n1 - Criptografar\n2 - Descriptografar", 1, 2);
        int opcaoModo = LerOpcao("Selecione o modo de operação:\n1 - ECB\n2 - CBC", 1, 2);

        byte[] chave = LerChave();
        byte[]? iv = opcaoModo == 2 ? LerVetorInicializacao() : null;
        string arquivoEntrada = LerArquivo();
        string nomeArquivoSaida = LerNomeArquivo();

        if (opcaoCriptografia == 1)
            Criptografar(arquivoEntrada, chave, opcaoModo, iv, nomeArquivoSaida);
        else
            Descriptografar(arquivoEntrada, chave, opcaoModo, iv, nomeArquivoSaida);
    }

    static int LerOpcao(string mensagem, int min, int max)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            if (int.TryParse(Console.ReadLine(), out int opcao) && opcao >= min && opcao <= max)
                return opcao;
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }

    static byte[] LerChave()
    {
        while (true)
        {
            Console.Write("Digite a chave (16 valores decimais separados por vírgula): ");
            string? input = Console.ReadLine();
            string[] partes = input?.Split(',') ?? [];

            if (partes.Length != 16)
            {
                Console.WriteLine("Tamanho inválido. A chave deve ter exatamente 16 valores.");
                continue;
            }

            byte[] chave = new byte[16];
            bool valido = true;

            for (int i = 0; i < partes.Length; i++)
            {
                if (!byte.TryParse(partes[i].Trim(), out chave[i]))
                {
                    Console.WriteLine($"Valor inválido: '{partes[i].Trim()}'. Use números entre 0 e 255.");
                    valido = false;
                    break;
                }
            }

            if (valido) return chave;
        }
    }

    static string LerArquivo()
    {
        while (true)
        {
            Console.Write("Digite o caminho do arquivo de entrada: ");

            string? input = Console.ReadLine();
            string caminho = input ?? string.Empty;

            if (File.Exists(caminho)) return caminho;

            Console.WriteLine("Arquivo não encontrado. Tente novamente.");
        }
    }

    static string LerNomeArquivo()
    {
        while (true)
        {
            Console.Write("Digite o nome do arquivo de saída: ");
            string? input = Console.ReadLine();
            string nome = input?.Trim() ?? string.Empty;

            string[] reservados = { "CON","PRN","AUX","NUL",
                "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
                "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9" };

            var erro = string.IsNullOrWhiteSpace(nome) ? "Nome não pode ser vazio" :
                        nome.Any(c => Path.GetInvalidFileNameChars().Contains(c)) ? "Nome contém caracteres inválidos." :
                        reservados.Contains(Path.GetFileNameWithoutExtension(nome).ToUpper()) ? "Nome reservado pelo sistema." :
                        nome.Length > 255 ? "Nome muito longo (deve ser menor que 255 caracteres)." :
                        nome.EndsWith(".") ? "Não pode terminar com '.'" :
                        null;

            if (erro == null) return nome;
            Console.WriteLine(erro);
        }
    }

    static byte[] LerVetorInicializacao()
    {
        while (true)
        {
            Console.Write("Digite o vetor de inicialização (16 valores decimais separados por vírgula): ");
            string? input = Console.ReadLine();
            string[] partes = input?.Split(',') ?? [];

            if (partes.Length != 16)
            {
                Console.WriteLine("Tamanho inválido. O IV deve ter exatamente 16 valores.");
                continue;
            }

            byte[] iv = new byte[16];
            bool valido = true;

            for (int i = 0; i < partes.Length; i++)
            {
                if (!byte.TryParse(partes[i].Trim(), out iv[i]))
                {
                    Console.WriteLine($"Valor inválido: '{partes[i].Trim()}'. Use números entre 0 e 255.");
                    valido = false;
                    break;
                }
            }

            if (valido) return iv;
        }
    }

    public static void Criptografar(string arquivoEntrada, byte[] chave, int modo, byte[]? iv, string nomeArquivoSaida)
    {
        byte[] dados = File.ReadAllBytes(arquivoEntrada);
        byte[][] keySchedule = ExpansaoChave.ExpandirChave(chave);
        byte[] dadosCifrados = modo == 1
            ? ECB.Cifrar(dados, keySchedule)
            : CBC.Cifrar(dados, keySchedule, iv);

        string diretorioSaida = Path.GetDirectoryName(arquivoEntrada) ?? Directory.GetCurrentDirectory();
        string arquivoSaida = Path.Combine(diretorioSaida, Path.GetFileNameWithoutExtension(nomeArquivoSaida) + ".bin");
        File.WriteAllBytes(arquivoSaida, dadosCifrados);
        Console.WriteLine($"Arquivo cifrado salvo em: {arquivoSaida}");
    }

    public static void Descriptografar(string arquivoEntrada, byte[] chave, int modo, byte[]? iv, string nomeArquivoSaida)
    {
        byte[] dados = File.ReadAllBytes(arquivoEntrada);
        byte[][] keySchedule = ExpansaoChave.ExpandirChave(chave);
        byte[] dadosDecifrados = modo == 1
            ? ECB.Decifrar(dados, keySchedule)
            : CBC.Decifrar(dados, keySchedule, iv);

        string diretorioSaida = Path.GetDirectoryName(arquivoEntrada) ?? Directory.GetCurrentDirectory();
        string arquivoSaida = Path.Combine(diretorioSaida, Path.GetFileNameWithoutExtension(nomeArquivoSaida) + ".txt");
        File.WriteAllBytes(arquivoSaida, dadosDecifrados);
        Console.WriteLine($"Arquivo decifrado salvo em: {arquivoSaida}");
    }
}