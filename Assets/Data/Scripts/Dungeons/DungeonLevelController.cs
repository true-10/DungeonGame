using GridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.SceneManagement;

namespace DungeonGame
{

    public sealed class DungeonLevelController : MonoBehaviour
    {
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<GameObject> gridInfoManager;
        [Inject]
        private EnemyManager enemyManager;
        [Inject]
        private DungeonManager dungeonManager;
        [Inject]
        private CharacterStaticData character;

        [SerializeField]
        private CharacterDummy characterDummy;
        [SerializeField]
        private MonoGridInput monoGridInput;
        [SerializeField]
        private List<GameObject> propsOnGrid;
        [SerializeField]
        private EnemySpawner enemySpawner;
        [SerializeField]
        private GameObject UIObject;
        [SerializeField]
        private DoorOpener doorOpener;

        private GridCell activeCell = null;
        private AvailableCellsProcessor availableCellsProcessor;
        private List<IRule> rules = new List<IRule>()
        {
            new IfItsEnemyRule()
        };

        private IGridInput gridInput => monoGridInput?.GridInput;


        private void Start()
        {
            activeCell = null;
            availableCellsProcessor = new AvailableCellsProcessor(gridController, gridInfoManager, rules);
            gridInput.OnInput += OnInput; 
            enemyManager.OnDeath += OnEnemyDeath;
            enemySpawner.Init();
            enemyManager.Enemies
                .ForEach(enemy => 
                {
                    if (enemy.Body.TryGetComponent<EnemyDummy>(out var enemyDummy))
                    {
                        enemyDummy.SetTarget(characterDummy.transform);
                    }
                });

            SetPpopsOnGrid();
            SetEnemiesOnGrid();
            SetPlayerOnGrid();
            UIObject.SetActive(false);
            var levelDyn = dungeonManager.CurrentDungeon.GetÑurrentLevel();
            dungeonManager.CurrentDungeon.SetLevelStarted(levelDyn.Id, true);
        }

        private void OnDestroy()
        {
            gridInput.OnInput -= OnInput;
            enemyManager.OnDeath -= OnEnemyDeath;
            //enemySpawner.di
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            
            enemySpawner.Remove(enemy.Body);
            if (enemyManager.IsAllDead())
            {
                doorOpener.OpenDoor();
                var levelDyn = dungeonManager.CurrentDungeon.GetÑurrentLevel();
                dungeonManager.CurrentDungeon.SetLevelComleted(levelDyn.Id, true);
                Debug.Log($"Level [{levelDyn.Id}] is complete");
            }
        }

        private void OnInput(GridCell cell)
        {
            if (cell == null)
            {
                activeCell = null;
                return;
            }
            var targetCellInfo = gridInfoManager.GetCellInfoByIndex(cell.Index);
            var CurrentCellInfo = gridInfoManager
                .GridInfo
                .GetAllCellInfos()
                .FirstOrDefault(x => x.IsEmpty == false && x.Object.GetHashCode() == characterDummy.gameObject.GetHashCode());
            if (Input.GetMouseButton(0) && gridInfoManager.GridInfo.AvailableCells.Contains(cell) )
            {
                activeCell = cell;
                UIObject.SetActive(true);
                monoGridInput.Disable = true;
            }
        }

        public void AttackActiveCell()
        {
            if (activeCell == null)
            {
                return;
            }
            var targetCellInfo = gridInfoManager.GetCellInfoByIndex(activeCell.Index);
            if (targetCellInfo != null && targetCellInfo.Object.TryGetComponent<EnemyDummy>(out var enemyDummy))
            {
                enemyDummy.TakeDamage(character.Damage);
            }
            UIObject.SetActive(false);
            monoGridInput.Disable = false;
        }


        public void WalkToActiveCell()
        {
            if (activeCell == null)
            {
                return;
            }
            var targetCellInfo = gridInfoManager.GetCellInfoByIndex(activeCell.Index);
            if (targetCellInfo.IsEmpty == false)
            {

                UIObject.SetActive(false);
                monoGridInput.Disable = false;
                return;
            }
            var CurrentCellInfo = gridInfoManager
                .GridInfo
                .GetAllCellInfos()
                .FirstOrDefault(x => x.IsEmpty == false && x.Object.GetHashCode() == characterDummy.gameObject.GetHashCode());
            if (CurrentCellInfo != null)
            {
                CurrentCellInfo.Object = null;
            }
            characterDummy.MoveTo(activeCell.Position);
            targetCellInfo.Object = characterDummy.gameObject;

            gridInfoManager.UpdateCellInfo(activeCell, targetCellInfo);
            availableCellsProcessor.CalculateAvailableCellsFrom(activeCell, character.MoveLength);
            UIObject.SetActive(false);
            monoGridInput.Disable = false;
        }

        private void SetPlayerOnGrid()
        {
           var cell = SetOnGrid(characterDummy.gameObject);
           availableCellsProcessor.CalculateAvailableCellsFrom(cell,character.MoveLength);
        }

        private void SetEnemiesOnGrid()
        {
            enemySpawner.enemiesObjects.ForEach(x => SetOnGrid(x));
        }

        private void SetPpopsOnGrid()
        {
            propsOnGrid.ForEach(x => SetOnGrid(x));
        }

        private GridCell SetOnGrid(GameObject obj)
        {
            if (obj.activeInHierarchy == false)
            {
                return null;
            }
            Vector3 position = obj.transform.position;
            position.y = 0f;
            var cell = gridController.Grid.GetCellByPosition(position);
            if (cell == null)
            {
                return null;
            }
            obj.transform.position = cell.Position;
            var cellInfo = gridInfoManager.GetCellInfoByIndex(cell.Index);
            cellInfo.Object = obj;
            gridInfoManager.UpdateCellInfo(cell, cellInfo);
            return cell;

        }

        private void OnEnable()
        {
            doorOpener.OnClick += LoadLevelScene;
        }

        private void OnDisable()
        {
            doorOpener.OnClick -= LoadLevelScene;
        }

        private void LoadLevelScene(Vector3 position)
        {
            characterDummy.MoveTo(position);
        }


    }
}
