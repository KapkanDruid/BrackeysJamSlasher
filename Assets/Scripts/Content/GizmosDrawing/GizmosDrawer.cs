using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public sealed class GizmosDrawer : MonoBehaviour
    {
        private IGizmosDrawer[] _gizmosDrawers;
        private IGizmosDrawerOnSelected[] _gizmosDrawerOnSelected;

        [Inject]
        private void Construct(List<IGizmosDrawer> gizmosDrawers, List<IGizmosDrawerOnSelected> gizmosDrawerOnSelected)
        {
            _gizmosDrawers = gizmosDrawers.ToArray();
            _gizmosDrawerOnSelected = gizmosDrawerOnSelected.ToArray();
        }

        private void OnDrawGizmos()
        {
            if (_gizmosDrawers == null)
                return;

            foreach (var drawer in _gizmosDrawers)
                drawer.OnDrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (_gizmosDrawerOnSelected == null)
                return;

            foreach (var drawer in _gizmosDrawerOnSelected)
                drawer.OnDrawGizmosSelected();
        }
    }
}
