using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DungeonGame
{
    public class EnemyManager
    {
        public Action<Enemy> OnDeath { get; set; }
        public List<Enemy> Enemies => enemies;

        private List<Enemy> enemies = new();


        public void Init(List<Enemy> enemies)
        {
            this.enemies = enemies;

            enemies.ForEach(x => x.OnDeath += OnDeathHandler);
        }
        public void Dispose()
        {
            enemies.ForEach(x => x.OnDeath -= OnDeathHandler);
            enemies.Clear();
        }

        private void OnDeathHandler(Enemy enemy)
        {
            OnDeath?.Invoke(enemy);
        }

        public bool IsAllDead()
        {
            return Enemies.All(x => x.IsDead());
        }

    }
}
