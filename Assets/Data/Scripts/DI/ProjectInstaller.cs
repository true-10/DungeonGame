using System;
using UnityEngine;
using Zenject;

namespace DungeonGame
{ 
public class ProjectInstaller : MonoInstaller
{
        public override void InstallBindings()
        {
            Container.Bind<CharacterStaticData>().FromMethod(GetCharacter ).AsSingle();
            Container.Bind<DungeonManager>().AsSingle();
            Container.Bind<RewardStaticManager>().AsSingle();
        }

        private CharacterStaticData GetCharacter()
        {
            return StaticDataLoader.LoadCharacterStaticData().GetCharacter();         
        }
    }
}