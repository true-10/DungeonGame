using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DungeonGame
{
    public sealed class EnemyDummy : MonoBehaviour
    {
        private Enemy enemy;
        private Transform target;

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
            enemy.SetBody(gameObject);
        }
        
        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void TakeDamage(int damage)
        {
            enemy?.TakeDamage(damage);
        }

        private void Start()
        {
            transform.DOLocalJump(transform.position + Vector3.up * .2f, 1f, 1, 1.5f).SetLoops(-1);
        }

        private void Update()
        {
            if (target != null)
            {
                transform.LookAt(target, Vector3.up);
            }
        }

    }
}
