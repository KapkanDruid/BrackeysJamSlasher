using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class DynamicDrawLayer : MonoBehaviour
    {
        [SerializeField] private Transform _drawPoint;
        [SerializeField] private bool _includeThisAsMain;
        [SerializeField] private SpriteRenderer _mainObject;
        [SerializeField] private List<SpriteRenderer> _childObjects = new() { null };

        private Vector3[] _childPositions;
        private int _mainObjectIndex;

        private void Start()
        {
            SortObjectsLayer();
        }

        [ContextMenu("SetObjectsLayer")]
        private void SortObjectsLayer()
        {
            _mainObjectIndex = _childObjects.IndexOf(_mainObject);

            SortObjects();
        }

        private void SetMainObjectPosition()
        {
            _mainObject.sortingOrder = -(int)(_drawPoint.position.y * 1000);
        }

        private void Update()
        {
            SetMainObjectPosition();
        }
        private void LateUpdate()
        {
            SetMainObjectPosition();
            SortObjects();
        }

        private void SortObjects()
        {
            for (int i = 0; i < _childObjects.Count; i++)
            {
                if (_childObjects[i] == _mainObject)
                {
                    continue;
                }

                _childObjects[i].sortingOrder = _mainObject.sortingOrder + (_mainObjectIndex - i);
            }
        }
    }
}
