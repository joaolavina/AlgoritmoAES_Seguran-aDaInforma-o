public static class ExpansaoChave
{
    public static byte[][] ExpandirChave(byte[] chave)
    {
        byte[,] matrizEstado = Util.CriarMatriz(chave);
        byte[][] keySchedule = new byte[44][];

        for (int i = 0; i < 4; i++)
            keySchedule[i] = Util.GetPalavraColuna(matrizEstado, i);


        for (int i = 4; i < 44; i++)
        {
            if (i % 4 == 0) // Primeira palavra de cada rodada
            {

                int numeroRoundKey = i / 4;
                byte[] palavraAnterior = keySchedule[i - 1]; // (1) Copiar última palavra da round key anteriror
                byte[] rotacionada = RotWord(palavraAnterior); // (2) Rotacionar bytes (RotWord)
                byte[] substituida  = SubWord(rotacionada); // (3) Substituir bytes usando S-Box (SubWord)
                byte[] roundConstant = RoundConstant.GetPalavra(numeroRoundKey); // (4) Gerar RoundConstant para a rodada atual
                byte[] temp = XorPalavras(substituida, roundConstant); // (5) XOR com RoundConstant
                keySchedule[i] = XorPalavras(keySchedule[i - 4], temp); // (6) XOR com a palavra da rodada anterior
            }
            else // Outras palavras
            {
                keySchedule[i] = XorPalavras(keySchedule[i - 4], keySchedule[i - 1]); // XOR com a palavra da rodada anterior
            }
        }

        return keySchedule;
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

     private static byte[] XorPalavras(byte[] a, byte[] b)
    {
        return
        [
            (byte)(a[0] ^ b[0]),
            (byte)(a[1] ^ b[1]),
            (byte)(a[2] ^ b[2]),
            (byte)(a[3] ^ b[3])
        ];
    }
}