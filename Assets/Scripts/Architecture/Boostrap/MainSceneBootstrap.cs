using Assets.Scripts.Architecture;
using UnityEngine;
using Zenject;

public class MainSceneBootstrap : MonoBehaviour
{
    [Inject] private InputSystemActions _inputActions;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _inputActions.Enable();
    }

    private void Dispose()
    {
        _inputActions.Disable();
    }

    private void OnDisable()
    {
        Dispose();
    }
}
