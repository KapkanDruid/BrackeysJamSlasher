using Assets.Scripts.Architecture;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PLayerController : MonoBehaviour
    {
        private InputSystemActions _inputSystemActions;

        [Inject]
        private void Construct(InputSystemActions inputActions)
        {
            _inputSystemActions = inputActions;
            Debug.Log("sdsdsd");
        }
    }
}