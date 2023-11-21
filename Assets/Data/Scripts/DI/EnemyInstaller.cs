using UnityEngine;
using Zenject;

namespace DungeonGame
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            Container.Bind<EnemiesStaticManager>().AsSingle();
            Container.Bind<EnemyManager>().AsSingle();
        }
    }

}