public static class ECB
{
    public static byte[] Cifrar(byte[] dados, byte[][] keySchedule)
    {
        byte[] dadosComPadding = Padding.Adicionar(dados);
        List<byte> resultado = [];

        for (int i = 0; i < dadosComPadding.Length; i += 16)
        {
            byte[] bloco = dadosComPadding[i..(i + 16)];
            byte[] blocoCifrado = Cifragem.CifrarBloco(bloco, keySchedule);
            resultado.AddRange(blocoCifrado);
        }

        return [.. resultado];
    }

    public static byte[] Decifrar(byte[] dados, byte[][] keySchedule)
    {
        List<byte> resultado = [];

        for (int i = 0; i < dados.Length; i += 16)
        {
            byte[] bloco = dados[i..(i + 16)];
            byte[] blocoDecifrado = Decifragem.DecifrarBloco(bloco, keySchedule);
            resultado.AddRange(blocoDecifrado);
        }

        return Padding.Remover([.. resultado]);
    }
}