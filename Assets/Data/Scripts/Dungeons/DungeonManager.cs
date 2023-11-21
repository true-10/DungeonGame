
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace DungeonGame
{

    public class LevelDynamic
    {
        public int Id;
        public bool IsStarted;
        public bool IsComplete;
        public int Reward;
    }

    public class DungeonDynamicData
    {
        public LevelDynamic GetСurrentLevel() => Levels.FirstOrDefault(x => x.IsComplete == false);

        public List<LevelDynamic> Levels { get; private set; }

        public DungeonDynamicData()
        {
            InitLevels();
        }

        private void InitLevels()
        {
            Levels = new();
            for (int i = 1; i <= 3; i++)
            {
                var newLevelDynamic = new LevelDynamic()
                {
                    Id = i,
                    Reward = i,
                    IsComplete = false,
                };
                Levels.Add(newLevelDynamic);
            }

        }
        public void SetLevelComleted(int level, bool isCompleted)
        {
            var levelDynamic = Levels.FirstOrDefault(x => x.Id == level);
            levelDynamic.IsComplete = isCompleted;

        }

        public void SetLevelStarted(int level, bool isStarted)
        {
            var levelDynamic = Levels.FirstOrDefault(x => x.Id == level);
            levelDynamic.IsStarted = isStarted;

        }

    }

    public class DungeonManager
    {
        public DungeonDynamicData CurrentDungeon { get; set; }


        public DungeonManager()
        {
            ResetData();
        }

        public void ResetData()
        {
            CurrentDungeon = new();
        }
    }
}

