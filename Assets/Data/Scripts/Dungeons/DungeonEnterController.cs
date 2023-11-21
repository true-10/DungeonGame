using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Zenject;
using UniRx;
using System.Linq;

namespace DungeonGame
{
    public class DungeonEnterController : MonoBehaviour
    {
        [SerializeField]
        private DoorOpener doorOpener;
        [SerializeField]
        private CharacterDummy character;

        [Inject]
        private DungeonManager dungeonManager;

        private IDisposable fiveMinTimer = null;

        private void OnEnable()
        {
            doorOpener.OnClick += LoadLevelScene;
            SetTimer();
        }

        private void OnDisable ()
        {
            doorOpener.OnClick -= LoadLevelScene;
            fiveMinTimer?.Dispose();
            fiveMinTimer = null;
        }

        private void SetTimer()
        {
            fiveMinTimer?.Dispose();
            fiveMinTimer = null;
            if (dungeonManager.CurrentDungeon.Levels.Any(x => x.IsStarted))
            {
                fiveMinTimer = Observable
                    .Timer(TimeSpan.FromMinutes(5))
                    .Subscribe((l) =>
                    {
                        Debug.Log($"Time's up! ");
                        dungeonManager.ResetData();
                    })
                    .AddTo(this);
            }
        }

        private void LoadLevelScene(Vector3 position)
        {
            character.MoveTo(position);
        }
    }

}
