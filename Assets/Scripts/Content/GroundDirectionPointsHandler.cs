using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public sealed class GroundDirectionPointsHandler : MonoBehaviour
    {
        private List<Transform> _groundPoints;

        public Transform[] GroundPoints => _groundPoints.ToArray();

        public void Initialize()
        {
            _groundPoints = new();

            _groundPoints.AddRange(GetComponentsInChildren<Transform>());

            if (_groundPoints.Contains(transform))
                _groundPoints.Remove(transform);
        }
    }
}
