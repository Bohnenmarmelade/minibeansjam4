using UnityEngine;

namespace Punishments
{
    public static class PunishmentType
    {
        public const string POISON = "Poison";
        public const string SHAKE = "Shake";
        public const string SLEEP = "Sleep";

        public static string GetRandomPunishment()
        {
            switch (Random.Range(0, 3))
            {
                case 1:
                    return SHAKE;
                case 2:
                    return SLEEP;
                default:
                    return POISON;
            }
        }
    }
}