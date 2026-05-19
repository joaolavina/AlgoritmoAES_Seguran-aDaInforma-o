public static class ExpansaoChave
{
    public static byte[][] ExpandirChave(byte[] chave, byte[,] matrizEstado)
    {
        byte[][] keySchedule = new byte[44][];

        for (int i = 0; i < 4; i++)
            keySchedule[i] = Util.GetPalavraColuna(matrizEstado, i);


        for (int i = 4; i < 44; i++)
        {
            if (i % 4 == 0) // Primeira palavra de cada rodada
            {
                byte[] temp = new byte[4];

                // int numeroRoundKey = i / 4;
                byte[] palavraAnterior = keySchedule[i - 1]; // (1) Copiar última palavra da round key anteriror
                temp = RotWord(palavraAnterior); // (2) Rotacionar bytes (RotWord)
            }
            else // Outras palavras
            {

            }
        }
    }

    private static byte[] RotWord(byte[] palavra)
    {
        return [palavra[1], palavra[2], palavra[3], palavra[0]];
    }

    private static byte[] SubWord(byte[] palavra)
    {
        return new byte[]
        {
            SBox.Substituir(palavra[0]),
            SBox.Substituir(palavra[1]),
            SBox.Substituir(palavra[2]),
            SBox.Substituir(palavra[3])
        };
    }

}