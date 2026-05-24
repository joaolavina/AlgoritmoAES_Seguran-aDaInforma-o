public static class RoundConstant
{
    private static readonly byte[] Tabela =
    {
        0x00,
        0x01, 0x02, 0x04, 0x08, 0x10,
        0x20, 0x40, 0x80, 0x1B, 0x36
    };

    public static byte[] GetPalavra(int i)
    {
        return [Tabela[i], 0x00, 0x00, 0x00];
    }
}