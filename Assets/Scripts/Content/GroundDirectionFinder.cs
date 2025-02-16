using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public sealed class GroundDirectionFinder : IGizmosDrawer
    {
        private readonly GroundDirectionPointsHandler _pointsHandler;

        private List<Vector2> _groundPoints;

        public GroundDirectionFinder(GroundDirectionPointsHandler pointsHandler)
        {
            _pointsHandler = pointsHandler;
        }

        public void Initialize()
        {
            _groundPoints = new();

            foreach (var point in _pointsHandler.GroundPoints)
            {
                _groundPoints.Add(point.position);
            }

            _groundPoints = _groundPoints.OrderBy(a => a.x).ToList();
        }

        public Vector2 GetVectorByPosition(Vector2 checkPosition)
        {
            for (int i = 0; i < _groundPoints.Count - 1; i++)
            {
                if (_groundPoints[i].x <= checkPosition.x && _groundPoints[i + 1].x >= checkPosition.x)
                {
                    var leftPoint = _groundPoints[i];
                    var rightPoint = _groundPoints[i + 1];

                    return (rightPoint - leftPoint).normalized;

                }
            }

            Debug.LogError("Filed to GetVectorByPosition, Character is out of ground");

            return Vector2.zero;
        }

        public void OnDrawGizmos()
        {
            if (_groundPoints == null)
                return;

            Gizmos.color = Color.red;

            for (int i = 0; i < _groundPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(_groundPoints[i], _groundPoints[i + 1]);
            }
        }
    }
}
