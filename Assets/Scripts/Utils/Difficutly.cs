
namespace Utils
{
    public static class Difficulty
    {
        public const string EASY = "Easy";
        public const string MEDIUM = "Medium";
        public const string HARD = "Hard";
        public const string SUPERHARD = "Superhard";
        public const string MEGAHARD = "Megahard";
        public const string GIGAHARD = "Gigahard";
        public const string NINETOUSANDANDONE = "9001";

        public static string GetNextHigherDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case EASY:
                    return MEDIUM;
                case MEDIUM:
                    return HARD;
                case HARD:
                    return SUPERHARD;
                case SUPERHARD:
                    return MEGAHARD;
                case MEGAHARD:
                    return GIGAHARD;
                default:
                    return NINETOUSANDANDONE;
            }
        }
    };
}

