using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class DynamicDrawLayer : MonoBehaviour
    {
        [SerializeField] private Transform _drawPoint;
        [SerializeField] private bool _includeThisAsMain;
        [SerializeField] private Transform _mainObject;
        [SerializeField] private List<Transform> _childObjects = new() { null };
        [SerializeField] private Transform _shadow;

        private Transform _lastObject;
        private Vector3[] _childPositions;

        private void Start()
        {
            SortObjectsLayer();
        }

        [ContextMenu("FindObjects")]
        private void FindChildObjects()
        {
            _childObjects.Clear();

            var foundedObjects = transform.GetComponentsInChildren<Transform>();

            _childObjects.AddRange(foundedObjects);

            if (_includeThisAsMain)
                _mainObject = transform;
        }

        [ContextMenu("SetObjectsLayer")]
        private void SortObjectsLayer()
        {
            var mainObjectIndex = _childObjects.IndexOf(_mainObject);

            mainObjectIndex *= -1;

            for (int i = 0; i < _childObjects.Count; i++)
            {
                if (_childObjects[i] == _mainObject)
                {
                    _lastObject = _childObjects[i];
                    continue;
                }

                _childObjects[i].localPosition = new Vector3(_childObjects[i].localPosition.x, _childObjects[i].localPosition.y, mainObjectIndex + i);
                _lastObject = _childObjects[i];
            }

            SetShadowPosition();
        }

        private void SetShadowPosition()
        {
            if (_shadow != null)
                _shadow.position = new Vector3(_shadow.position.x, _shadow.position.y, _lastObject.position.z + 1);
        }

        private void SetMainObjectPosition()
        {
            _mainObject.position = new Vector3(_mainObject.position.x, _mainObject.position.y, _drawPoint.position.y * 100);
        }

        private void Update()
        {
            SetMainObjectPosition();
            SetShadowPosition();
        }
    }
}
