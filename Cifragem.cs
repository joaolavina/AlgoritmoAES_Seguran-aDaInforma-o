public static class Cifragem
{
    public static byte[] CifrarBloco(byte[] bloco, byte[][] keySchedule)
    {
        // monta a matriz de estado a partir do bloco de 16 bytes
        byte[,] matrizEstado = Util.CriarMatriz(bloco);

        // etapa 1 — rodada 0
        matrizEstado = Util.AddRoundKey(matrizEstado, keySchedule, 0);

        // rodadas 1 a 9
        for (int rodada = 1; rodada <= 9; rodada++)
        {
            matrizEstado = SubBytes(matrizEstado);
            matrizEstado = ShiftRows(matrizEstado);
            matrizEstado = MixColumns(matrizEstado);
            matrizEstado = Util.AddRoundKey(matrizEstado, keySchedule, rodada);
        }

        // rodada final — sem MixColumns
        matrizEstado = SubBytes(matrizEstado);
        matrizEstado = ShiftRows(matrizEstado);
        matrizEstado = Util.AddRoundKey(matrizEstado, keySchedule, 10);

        return Util.MatrizParaBytes(matrizEstado);
    }



    public static byte[,] SubBytes(byte[,] matrizEstado)
    {
        byte[,] resultado = new byte[4, 4];

        for (int lin = 0; lin < 4; lin++)
            for (int col = 0; col < 4; col++)
                resultado[lin, col] = SBox.Substituir(matrizEstado[lin, col]);

        return resultado;
    }

    public static byte[,] ShiftRows(byte[,] matrizEstado)
    {
        byte[,] resultado = new byte[4, 4];

        for (int lin = 0; lin < 4; lin++)
            for (int col = 0; col < 4; col++)
                resultado[lin, col] = matrizEstado[lin, (col + lin) % 4];

        return resultado;
    }

    public static byte[,] MixColumns(byte[,] estado)
    {
        byte[,] resultado = new byte[4, 4];

        byte[,] matrizMult = new byte[,]
        {
        { 2, 3, 1, 1 },
        { 1, 2, 3, 1 },
        { 1, 1, 2, 3 },
        { 3, 1, 1, 2 }
        };

        for (int col = 0; col < 4; col++)
            for (int lin = 0; lin < 4; lin++)
            {
                byte valor = 0;
                for (int k = 0; k < 4; k++)
                    valor ^= Util.MultiplicarGalois(matrizMult[lin, k], estado[k, col]);
                resultado[lin, col] = valor;
            }

        return resultado;
    }
}