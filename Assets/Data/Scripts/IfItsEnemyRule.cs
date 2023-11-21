using GridSystem;
using UnityEngine;

namespace DungeonGame
{
    public class IfItsEnemyRule : IRule
    {
        public bool IsFollowed(CellInfo<GameObject> targetCellInfo)
        {
            if (targetCellInfo.Object != null && targetCellInfo.Object.TryGetComponent<DungeonGame.EnemyDummy>(out var enemyDummy))
            {
                return true;
            }
                
            return false;
        }
    }
}
