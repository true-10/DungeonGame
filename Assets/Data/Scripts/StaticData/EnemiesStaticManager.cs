using System.Collections.Generic;
using System.Linq;

namespace DungeonGame
{
    public sealed class EnemiesStaticManager
    {
        public readonly List<EnemyStaticData> Enemies;

        public EnemiesStaticManager()
        {
            Enemies = StaticDataLoader.LoadEnemyStaticData().EnemyStaticDatas;
        }

        public EnemyStaticData GetEnemyStaticDataById(int enemyId)
        {
            return Enemies.FirstOrDefault(enemy => enemy.Id == enemyId);
        }
    }
}
