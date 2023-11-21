using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace DungeonGame
{
    public enum DoorType
    {
        Enter,
        Exit
    }

    public class DoorOpener : MonoBehaviour
    {
        [Inject]
        private DungeonManager dungeonManager;
        public Action<Vector3> OnClick { get; set; }
        [SerializeField]
        private Transform doorTransform;
        [SerializeField]
        private float yMaxAngle = 164f;
        [SerializeField]
        private float duration = 1f;
        [SerializeField]
        private Light behindTheDoorLight;
        [SerializeField]
        private float lightIntensity = 164f;        
        [SerializeField]
        private bool enableMouseOver = true;
        [SerializeField]
        private DoorType doorType = DoorType.Enter;
        [SerializeField]
        private TextMeshProUGUI hintText;


        private Sequence openDoorSequence = null;

        private bool dontCloseTheDoor = false;

        public void OpenDoor()
        {
            SequenceInit(Vector3.up * yMaxAngle, lightIntensity);
        }

        public void CloseDoor()
        {
            SequenceInit(Vector3.zero, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<CharacterDummy>(out var dummy) )
            {
                switch (doorType)
                {
                    case DoorType.Enter:
                        LoadLevel();
                        break;
                    case DoorType.Exit:
                        LoadLobyy();
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            openDoorSequence?.Complete();
            openDoorSequence?.Kill();
        }

        private void SequenceInit(Vector3 axisEndValue, float intensity)
        {
            SequenceCheck();
            if (openDoorSequence != null)
            {
                
                openDoorSequence
                .Append(doorTransform?.DOLocalRotate(axisEndValue, duration))
                .Join(DOTween.To(() => behindTheDoorLight.intensity, x => behindTheDoorLight.intensity = x, intensity, duration));
            }
        }

        private void SequenceCheck()
        {
            if (openDoorSequence != null)
            {
                openDoorSequence.Complete();
            }
            else
            {
                openDoorSequence = DOTween.Sequence();
            }
        }

        private void OnMouseEnter()
        {
            if (enableMouseOver == false)
            {
                return;
            }
            OpenDoor();
            var txt = GetText();
            ShowText(txt);
        }

        private string GetText()
        {
            switch (doorType)
            {
                case DoorType.Enter:
                    {
                        int lvlId = dungeonManager.CurrentDungeon.Get—urrentLevel().Id;
                        return $"Level {lvlId}";
                    }
                case DoorType.Exit:
                    return "Exit Level";
            }
            return string.Empty;
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

        private void OnMouseExit()
        {
            if (enableMouseOver == false)
            {
                return;
            }
            if (dontCloseTheDoor)
            {
                return;
            }
            CloseDoor();
            HideText();
        }


        private void OnMouseDown()
        {
            dontCloseTheDoor = true;
            OnClick?.Invoke(transform.position);
        }

        private void LoadLobyy()
        {
            SceneManager.LoadScene(GlobalConstants.Scenes.LobbyScene);
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(GlobalConstants.Scenes.LevelScene);
        }
    }
}
