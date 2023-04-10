using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Wonderland.Types
{
    public class ObjectPool<T> : IPool<T> where T : Objects, IPoolable<T>
    {
        public ObjectPool(GameObject pooledObject, int numToSpawn = 0)
        {
            _prefab = pooledObject;
            Spawn(numToSpawn);
        }
        
        public ObjectPool(GameObject pooledObject, Transform parent, int numToSpawn = 0)
        {
            _prefab = pooledObject;
            Spawn(numToSpawn, parent);
        }
        
        public ObjectPool(GameObject pooledObject, Action<T> pullObject, Action<T> pushObject, int spawnAmount = 0)
        {
            _prefab = pooledObject;
            _pullObject = pullObject;
            _pushObject = pushObject;
            Spawn(spawnAmount);
        }

        public ObjectPool(GameObject pooledObject, Transform parent, Action<T> pullObject, Action<T> pushObject, int spawnAmount = 0)
        {
            _prefab = pooledObject;
            _pullObject = pullObject;
            _pushObject = pushObject;
            Spawn(spawnAmount, parent);
        }

        private readonly Action<T> _pullObject;
        private readonly Action<T> _pushObject;
        private readonly GameObject _prefab;
        private readonly Stack<T> _pooledObjects = new();
        public int PooledCount => _pooledObjects.Count;

        public T Pull()
        {
            T t;
            
            if (PooledCount > 0)
                t = _pooledObjects.Pop();
            else
                return null;
            
            t.gameObject.SetActive(true); //ensure the object is on

            return t;
        }

        public T Pull(Vector3 position)
        {
            var t = Pull();
            t.transform.position = position;
            return t;
        }

        public T Pull(Vector3 position, Quaternion rotation)
        {
            var t = Pull();
            var transform = t.transform;
            transform.position = position;
            transform.rotation = rotation;
            return t;
        }
        
        public T Pull(Vector3 position, Quaternion rotation, Transform parent)
        {
            var t = Pull();
            var transform = t.transform;
            transform.position = position;
            transform.rotation = rotation;
            transform.parent = parent;
            return t;
        }

        public GameObject PullGameObject()
        {
            return PooledCount == 0 ? null : Pull().gameObject;
        }

        public GameObject PullGameObject(Vector3 position)
        {
            if (PooledCount == 0) return null;
            var go = Pull().gameObject;
            go.transform.position = position;
            return go;
        }

        public GameObject PullGameObject(Vector3 position, Quaternion rotation)
        {
            if (PooledCount == 0) return null;
            var go = Pull().gameObject;
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }
        
        public GameObject PullGameObject(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (PooledCount == 0) return null;
            var go = Pull().gameObject;

            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.parent = parent;
            
            //allow default behavior and turning object back on
            var t = go.GetComponent<T>();
            _pullObject?.Invoke(t);
            
            return go;
        }

        public void Push(T t)
        {
           _pooledObjects.Push(t);

            //create default behavior to turn off objects
            _pushObject?.Invoke(t);

            t.gameObject.SetActive(false);
        }

        private void Spawn(int number)
        {
            for (var i = 0; i < number; i++)
            {
                var t = Object.Instantiate(_prefab).GetComponent<T>();
                _pooledObjects.Push(t);
                t.gameObject.SetActive(false);
            }
        }
        
        private void Spawn(int number, Transform parent)
        {
            for (var i = 0; i < number; i++)
            {
                var t = Object.Instantiate(_prefab, parent).GetComponent<T>();
                _pooledObjects.Push(t);
                t.gameObject.SetActive(false);
            }
        }
    }
}
