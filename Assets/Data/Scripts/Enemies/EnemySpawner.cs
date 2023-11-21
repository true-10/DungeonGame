using GridSystem;
using System.Collections.Generic;
using True10.Prototyping;
using UnityEngine;
using Zenject;

namespace DungeonGame
{

    public class EnemySpawner : MonoBehaviour
    {
        [Inject]
        private EnemyManager enemyManager;
        [Inject]
        private EnemiesStaticManager enemiesStaticManager;
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<GameObject> gridInfoManager;


        [SerializeField]
        private int count = 3;

        [SerializeField]
        private ObjectSpawner objectSpawner;

        public List<GameObject> enemiesObjects => objectSpawner.ObjectsList;

        public void Init()
        {

            List<Enemy> enemies = new();
            for (int i = 0; i < count; i++)
            {
                var enemyStaticData = enemiesStaticManager.GetEnemyStaticDataById(0);
                var newEnemy = CreateEnemy(enemyStaticData);
                enemies.Add(newEnemy);
                AddEnemyOnRandomCell(newEnemy);
            }
            enemyManager.Init(enemies);
        }

        private void AddEnemyOnRandomCell(Enemy enemy)
        {

            var cell = gridController.Grid.GetRandomCell();
            var cellInfo = gridInfoManager.GetCellInfoByIndex(cell.Index);
            var enemyStatic = enemy.StaticData;
            if (cellInfo.IsEmpty)
            {
                objectSpawner.LoadAndSpawn(enemyStatic.prefabName, cell.Position, Quaternion.identity, null);
                var lastGo = objectSpawner.Last;
                if (lastGo.TryGetComponent<EnemyDummy>(out var enemyDummy))
                {
                    enemyDummy.SetEnemy(enemy);
                }
            }
        }

        private Enemy CreateEnemy(EnemyStaticData enemyStaticData)
        {
            Enemy newEnemy = new(enemyStaticData);

            return newEnemy;

        }

        public void Remove(GameObject item)
        {
            objectSpawner.Remove(item);
        }

        


    }
}
