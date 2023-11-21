using System;
using UnityEngine;

namespace DungeonGame
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }

    public class Enemy : IDamageable
    {
        public Action<Enemy> OnDeath { get; set; }

        public GameObject Body { get; private set; }

        public EnemyStaticData StaticData => staticData;
        public bool IsDead() => currentHP <= 0;

        private EnemyStaticData staticData;
        private int currentHP;

        public Enemy(EnemyStaticData staticData)
        {
            this.staticData = staticData;
            currentHP = staticData.Health;
        }

        public void SetBody(GameObject body)
        {
            Body = body;
        }

        public void TakeDamage(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            OnDeath?.Invoke(this);
        }
    }
}
