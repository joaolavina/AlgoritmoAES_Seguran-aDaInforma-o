public static class Padding
{
    public static byte[] Adicionar(byte[] dados)
    {
        int faltam = 16 - (dados.Length % 16);
        byte[] resultado = new byte[dados.Length + faltam];

        Array.Copy(dados, resultado, dados.Length);

        for (int i = dados.Length; i < resultado.Length; i++)
            resultado[i] = (byte)faltam;

        return resultado;
    }

    public static byte[] Remover(byte[] dados)
    {
        try
        {
            int padding = dados[dados.Length - 1];
            return dados[0..(dados.Length - padding)];
        }
        catch
        {
            throw new Exception("Não foi possível descriptografar o arquivo. O arquivo pode estar corrompido, a chave/IV podem estar incorretos ou você está utilizando o tipo de criptografia errado.");
        }
    }
}