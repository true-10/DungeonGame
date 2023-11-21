using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonGame
{
    [CreateAssetMenu(fileName = "RewardData", menuName = GlobalConstants.UIPath.ProjectFolder + "Rewards/RewardData")]
    public class RewardDataStaticStorage : ScriptableObject
    {
        [SerializeField]
        private List<RewardStaticData> rewardDatas;

        public List<RewardStaticData> RewardDatas => rewardDatas;


        public RewardStaticData GetRewardStaticDataById(int rewardId)
        {
            return RewardDatas.FirstOrDefault(x => x.RewardId == rewardId);
        }
    }
}

