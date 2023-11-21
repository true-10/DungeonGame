using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;
using System.Linq;
using True10.Prototyping;

namespace DungeonGame
{

    public class RewardViewer : MonoBehaviour
    {
        [Inject]
        private DungeonManager dungeonManager;
        [Inject]
        private RewardStaticManager rewardStaticManager;

        [SerializeField]
        private GameObject emptyObject;
        [SerializeField]
        private Transform prefabRoot;
        [SerializeField]
        private ObjectSpawner objectSpawner;
        [SerializeField]
        private TextMeshProUGUI hintText;

        private GameObject reward;

        private RewardStaticData rewardStatic = null;
        public bool IsEmpty => reward == null;


        void Start()
        {
            SetEmpty();
            SpawnCurrentReward();
        }
        
        private void ShowText()
        {
            if (rewardStatic == null)
            {
                HideText();
                return;
            }
            ShowText($"{rewardStatic.RewardName}");
        }

        private void ShowText(string newText)
        {
            if (hintText == null)
            {
                return;
            }
            hintText.text = newText;
        }

        private void HideText()
        {
            if (hintText == null)
            {
                return;
            }
            hintText.text = string.Empty;
        }

        private void SpawnCurrentReward()
        {
            var levelDynamic = dungeonManager.CurrentDungeon.Levels.LastOrDefault(x => x.IsComplete == true);
            if (levelDynamic == null)
            {
                return;
            }
            
            var rewardId = levelDynamic.Reward;
            SpawnReward(rewardId);            
        }

        private void SpawnReward(int rewardId)
        {
            objectSpawner.Clear();
            rewardStatic = null;
            rewardStatic = rewardStaticManager.GetRewardStaticDataById(rewardId);
            if (rewardStatic == null)
            {
                Debug.Log($"void SpawnReward(int rewardId = {rewardId}) rewardStatic is null");
                return;
            }
            string pathToReward = $"{GlobalConstants.Path.RewardPrefabPath}{rewardStatic.RewardPrefab}";
            objectSpawner.LoadAndSpawn(pathToReward, prefabRoot.position, Quaternion.identity, null);

            reward = objectSpawner.ObjectsList[0];
            if (reward == null)
            {
                Debug.Log($"void SpawnReward(int rewardId = {rewardId}) reward spawn fail");
                return;
            }

            reward.transform.DOPunchScale(Vector3.one * 1.1f, 3f).SetLoops(-1);
            emptyObject.SetActive(false);

        }


        [ContextMenu("Set Empty")]
        private void SetEmpty()
        {
            emptyObject.SetActive(true);
            Destroy(reward?.gameObject);
        }

        private void OnMouseEnter()
        {
            Debug.Log($"RewardViewer: OnMouseEnter");
            ShowText();
        }

        private void OnMouseExit()
        {
            Debug.Log($"RewardViewer: OnMouseExit");
            HideText();
        }

        private void OnMouseDown()
        {
            if (IsEmpty )
            {
                Debug.Log($"RewardViewer: no rewards");
                return;
            }
            //takeReward
            //resetAll
            Debug.Log($"Congrats!");
            dungeonManager.ResetData();
        }
    }
}
