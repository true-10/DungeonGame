using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace True10.Prototyping
{
    public class ObjectSpawner : MonoBehaviour
    {
        public List<GameObject> ObjectsList => objectsList;
        public GameObject First => objectsList.First();
        public GameObject Last => objectsList.Last();

        [SerializeField]
        private Transform root;

        protected List<GameObject> objectsList = new();


        public void Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Action<GameObject> onSpawn = null)
        {
            var newObject = Instantiate(prefab, position, rotation, root);
            objectsList.Add(newObject);
        }

        public void LoadAndSpawn(string pathToPrefab, Vector3 position, Quaternion rotation, Action<GameObject> onSpawn = null)
        {
            GameObject prefab = Resources.Load<GameObject>(pathToPrefab);
            if (prefab == null)
            {
                return;
            }
            Spawn(prefab, position, rotation, onSpawn);

        }

        public void Remove(GameObject item)
        {
            if (objectsList.Contains(item))
            {
                objectsList.Remove(item);
                Destroy(item);
            }
        }

        public void Clear()
        {
            objectsList.ForEach(x => Destroy(x.gameObject));
            objectsList.Clear();
        }

    }
}
