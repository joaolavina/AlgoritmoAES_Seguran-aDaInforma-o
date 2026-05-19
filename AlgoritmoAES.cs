public class AlgoritmoAES
{
    static void Main(string[] args)
    {
        int opcaoCriptografia = LerOpcao("Selecione a opção desejada:\n1 - Criptografar\n2 - Descriptografar", 1, 2);
        int opcaoModo        = LerOpcao("Selecione o modo de operação:\n1 - ECB\n2 - CBC", 1, 2);
        byte[] chave         = LerChave();
        string arquivoEntrada = LerArquivo();

        if (opcaoCriptografia == 1)
            Criptografar(arquivoEntrada, chave, opcaoModo);
        else
            Descriptografar(arquivoEntrada, chave, opcaoModo);
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
            string[] partes = Console.ReadLine().Split(',');

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
            string caminho = Console.ReadLine();

            if (File.Exists(caminho)) return caminho;

            Console.WriteLine("Arquivo não encontrado. Tente novamente.");
        }
    }

    public static void Criptografar(string arquivoEntrada, byte[] chave, int modo)
    {
        byte[,] matrizEstado = Util.CriarMatriz(chave);

        
    }



    public static void Descriptografar(string arquivoEntrada, byte[] chave, int modo) { }
}