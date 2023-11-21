using GridSystem;
using System;
using UnityEngine;
using Zenject;

namespace DungeonGame
{
    public class DungeonLevelInstaller : MonoInstaller
    {
        [SerializeField]
        private GameSettingsSO gameSettings;

        public override void InstallBindings()
        {
            Container.Bind<GameSettingsSO>().To<GameSettingsSO>().FromScriptableObject(gameSettings).AsSingle().NonLazy();
        }
    }
}