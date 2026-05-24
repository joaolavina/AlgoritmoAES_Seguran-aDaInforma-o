public static class CBC
{
    public static byte[] Cifrar(byte[] dados, byte[][] keySchedule, byte[] iv)
    {
        byte[] dadosComPadding = Padding.Adicionar(dados);
        List<byte> resultado   = [];
        byte[] blocoAnterior   = iv;

        for (int i = 0; i < dadosComPadding.Length; i += 16)
        {
            byte[] bloco   = dadosComPadding[i..(i + 16)];
            byte[] blocoXor = new byte[16];

            for (int j = 0; j < 16; j++)
                blocoXor[j] = (byte)(bloco[j] ^ blocoAnterior[j]);

            byte[] blocoCifrado = Cifragem.CifrarBloco(blocoXor, keySchedule);
            resultado.AddRange(blocoCifrado);
            blocoAnterior = blocoCifrado;
        }

        return resultado.ToArray();
    }

    public static byte[] Decifrar(byte[] dados, byte[][] keySchedule, byte[] iv)
    {
        List<byte> resultado = [];
        byte[] blocoAnterior = iv; 

        for (int i = 0; i < dados.Length; i += 16)
        {
            byte[] bloco         = dados[i..(i + 16)];
            byte[] blocoDecifrado = Decifragem.DecifrarBloco(bloco, keySchedule);
            byte[] blocoFinal     = new byte[16];

            for (int j = 0; j < 16; j++)
                blocoFinal[j] = (byte)(blocoDecifrado[j] ^ blocoAnterior[j]);

            resultado.AddRange(blocoFinal);
            blocoAnterior = bloco;
        }

        return Padding.Remover(resultado.ToArray());
    }
}