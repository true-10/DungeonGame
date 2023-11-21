using UnityEngine;

namespace DungeonGame
{

    public static class StaticDataLoader
    {
        private const string StaticPath = "StaticData/";
        private const string RewardsPath = "DungeonRewardData";
        private const string EnemyPath =  "EnemyStaticData";
        private const string CharacterPath = "CharacterStaticData";

        public static RewardDataStaticStorage LoadRewardStaticData()
        {
            return Resources.Load<RewardDataStaticStorage>(StaticPath + RewardsPath);
        }

        public static EnemyStaticStorage LoadEnemyStaticData()
        {
            return Resources.Load<EnemyStaticStorage>(StaticPath + EnemyPath);
        }

        public static CharacterStaticDataScriptableObject LoadCharacterStaticData()
        {
            return Resources.Load<CharacterStaticDataScriptableObject>(StaticPath + CharacterPath);
        }
    }

}
