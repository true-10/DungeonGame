using System.Collections.Generic;
using UnityEngine;

namespace DungeonGame
{

    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = GlobalConstants.UIPath.ProjectFolder + "EnemyStaticData")]
    public class EnemyStaticStorage : ScriptableObject
    {
        [SerializeField]
        private List<EnemyStaticData> enemyStaticDatas;
        public List<EnemyStaticData> EnemyStaticDatas => enemyStaticDatas;
    }


    [System.Serializable]
    public class EnemyStaticData
    {
        public int Id;
        public int Health;
        public string prefabName;
    }
}
