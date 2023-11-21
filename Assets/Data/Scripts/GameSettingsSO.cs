using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGame
{
    [CreateAssetMenu(menuName = GlobalConstants.UIPath.ProjectFolder + "Game Settings")]
    public class GameSettingsSO : ScriptableObject
    {

    }

    public static class GlobalConstants
    {
        public static class Scenes
        {
            public const string LevelScene = "DungeonLevel";
            public const string LobbyScene = "DungeonEnter";

        }

        public static class Path
        {
            public const string RewardPrefabPath = "RewardPrefabs/";
        }
        public static class UIPath
        {
            public const string ProjectFolder = "ApehaTest/";
        }

        
    }
}