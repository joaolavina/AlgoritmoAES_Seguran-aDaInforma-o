public static class Util
{
    public static byte[,] CriarMatriz(byte[] bytes)
    {
        byte[,] matriz = new byte[4, 4];
        for (int col = 0; col < 4; col++)
            for (int lin = 0; lin < 4; lin++)
                matriz[lin, col] = bytes[col * 4 + lin];
        return matriz;
    }

    public static byte[] MatrizParaBytes(byte[,] matriz)
    {
        byte[] result = new byte[16];
        for (int col = 0; col < 4; col++)
            for (int lin = 0; lin < 4; lin++)
                result[col * 4 + lin] = matriz[lin, col];
        return result;
    }

    public static byte[] GetPalavraColuna(byte[,] matriz, int col)
    {
        return [matriz[0, col], matriz[1, col], matriz[2, col], matriz[3, col]];
    }

    public static byte[] GetPalavraLinha(byte[,] matriz, int lin)
    {
        return [matriz[lin, 0], matriz[lin, 1], matriz[lin, 2], matriz[lin, 3]];
    }

    public static void ImprimirMatriz(byte[,] matriz)
    {
        for (int lin = 0; lin < 4; lin++)
        {
            for (int col = 0; col < 4; col++)
                Console.Write($"0x{matriz[lin, col]:X2} ");
            Console.WriteLine();
        }
    }
}