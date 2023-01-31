namespace ExamVeshkin.Utilities
{
    public static class RandomUtils
    {
        private static Random s_random = new Random();

        private const string AlphaCaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string AlphaLow = "abcdefghijklmnopqrstuvwxyz";
        private const string Numerics = "0123456789";
        private const string Special = "@#$-=/";
        private const string AllSymbols = AlphaCaps + AlphaLow + Numerics + Special;
        private const string NotSpetialSymbols = AlphaCaps + AlphaLow + Numerics;

        private const int DefaultStringLength = 10;


        public static string GenerateString(string sourceOfSymbols = NotSpetialSymbols, int length = DefaultStringLength)
        {
            return new string(Enumerable.Repeat(sourceOfSymbols, length)
                .Select(charSeq => charSeq[s_random.Next(charSeq.Length)]).ToArray());
        }
    }
}