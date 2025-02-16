using Assets.Scripts.Architecture;
using Assets.Scripts.Content;
using UnityEngine;
using Zenject;

public class MainSceneBootstrap : MonoBehaviour
{
    [Inject] private InputSystemActions _inputActions;
    [Inject] private GroundDirectionFinder _groundDirectionFinder;
    [Inject] private GroundDirectionPointsHandler _groundDirectionPointsHandler;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _inputActions.Enable();
        _groundDirectionPointsHandler.Initialize();
        _groundDirectionFinder.Initialize();
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
