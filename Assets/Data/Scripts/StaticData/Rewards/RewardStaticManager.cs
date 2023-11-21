using System.Collections.Generic;
using System.Linq;

namespace DungeonGame
{
    public sealed class RewardStaticManager
    {
        public readonly List<RewardStaticData> Rewards;

        public RewardStaticManager()
        {
            Rewards = StaticDataLoader.LoadRewardStaticData().RewardDatas;
        }

        public RewardStaticData GetRewardStaticDataById(int rewardId)
        {
            return Rewards.FirstOrDefault(reward => reward.RewardId == rewardId);
        }
    }
}
