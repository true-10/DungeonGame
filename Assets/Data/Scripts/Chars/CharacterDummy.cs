using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace DungeonGame
{
    public class CharacterDummy : MonoBehaviour
    {

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private CharacterController characterController;
        [SerializeField]
        private float moveDuration = 1f;


        private Transform cachedTransform;
        private Sequence moveSequence = null;

        private void OnValidate()
        {
            cachedTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            if (cachedTransform == null)
            {
                cachedTransform = GetComponent<Transform>();
            }
        }

        private void OnDestroy()
        {
            moveSequence?.Complete();
            moveSequence?.Kill();
        }

        public void MoveTo(Vector3 targetPosition)
        {
            SequenceCheck();
            if (moveSequence == null)
            {
                return;
            }
            
            var lookAtTween = cachedTransform.DOLookAt(targetPosition, moveDuration / 2f);
            var moveTween = cachedTransform.DOMove(targetPosition, moveDuration);
            moveSequence.Append(lookAtTween)
                .Append(moveTween);
        }

        private void SequenceCheck()
        {
            if (moveSequence != null)
            {
                moveSequence.Complete();
            }
            else
            {
                moveSequence = DOTween.Sequence();
            }
        }
    }
}
