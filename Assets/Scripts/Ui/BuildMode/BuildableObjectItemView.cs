using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Ui.BuildMode
{
    public class BuildableObjectItemView : MonoBehaviour, IPointerDownHandler
    {
        public event Action<BuildableObjectItemView> PointerDownEvent;

        public EntityType EntityType => entityType;
        public GameObject Prefab => prefab;

        [SerializeField] protected EntityType entityType;
        [SerializeField] protected GameObject prefab;

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDownEvent?.Invoke(this);
        }
    }
}
