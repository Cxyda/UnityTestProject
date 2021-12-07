using UnityEngine;
using Zenject;

namespace Utility
{
    public class PrefabFactory
    {
        private readonly DiContainer _container;

        public PrefabFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject CreateGameObject(GameObject prefab, Vector3 position = default, Quaternion rotation = default,  Transform parent = null)
        {
            var gameObject = _container.InstantiatePrefab(prefab, position, rotation, parent);

            return gameObject;
        }

        public void ReleaseGameObject<T>(T instance) where T : Object
        {
            Object.Destroy(instance);
        }
    }
}
