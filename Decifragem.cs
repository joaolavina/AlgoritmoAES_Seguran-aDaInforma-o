public static class Decifragem
{
    public static byte[] DecifrarBloco(byte[] bloco, byte[][] keySchedule)
    {
        byte[,] estado = Util.CriarMatriz(bloco);

        // etapa 1 — rodada 10
        estado = Utils.AddRoundKey(estado, keySchedule, 10);
        estado = InvShiftRows(estado);
        estado = InvSubBytes(estado);

        // rodadas 9 a 1
        for (int rodada = 9; rodada >= 1; rodada--)
        {
            estado = Utils.AddRoundKey(estado, keySchedule, rodada);
            estado = InvMixColumns(estado);
            estado = InvShiftRows(estado);
            estado = InvSubBytes(estado);
        }

        // rodada final
        estado = Utils.AddRoundKey(estado, keySchedule, 0);

        return Util.MatrizParaBytes(estado);
    }

    public static byte[,] InvShiftRows(byte[,] estado)
    {
        byte[,] resultado = new byte[4, 4];

        for (int lin = 0; lin < 4; lin++)
            for (int col = 0; col < 4; col++)
                resultado[lin, col] = estado[lin, (col - lin + 4) % 4];

        return resultado;
    }

    public static byte[,] InvMixColumns(byte[,] estado)
    {
        byte[,] resultado = new byte[4, 4];

        byte[,] matrizMultInv = new byte[,]
        {
        { 0x0E, 0x0B, 0x0D, 0x09 },
        { 0x09, 0x0E, 0x0B, 0x0D },
        { 0x0D, 0x09, 0x0E, 0x0B },
        { 0x0B, 0x0D, 0x09, 0x0E }
        };

        for (int col = 0; col < 4; col++)
            for (int lin = 0; lin < 4; lin++)
            {
                byte valor = 0;
                for (int k = 0; k < 4; k++)
                    valor ^= Util.MultiplicarGalois(matrizMultInv[lin, k], estado[k, col]);
                resultado[lin, col] = valor;
            }

        return resultado;
    }

    public static byte[,] InvSubBytes(byte[,] estado)
    {
        byte[,] resultado = new byte[4, 4];

        for (int lin = 0; lin < 4; lin++)
            for (int col = 0; col < 4; col++)
                resultado[lin, col] = SBox.SubstituirInverso(estado[lin, col]);

        return resultado;
    }
}