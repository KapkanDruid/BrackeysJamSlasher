using Assets.Scripts.Architecture;
using Assets.Scripts.Content;
using Assets.Scripts.Content.CoreProgression;
using System;
using UnityEngine;
using Zenject;

public class MainSceneBootstrap : MonoBehaviour
{
    [Inject] private InputSystemActions _inputActions;
    [Inject] private GameEndController _gameEndController;
    [Inject] private PopupTextController _popupTextController;
    [Inject] private GroundDirectionFinder _groundDirectionFinder;
    [Inject] private GroundDirectionPointsHandler _groundDirectionPointsHandler;
    [Inject] private GameDataLoader _gameDataLoader;

    public event Action OnServicesInitialized;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _gameDataLoader.Initialize();
        _gameEndController.Initialize();
        _inputActions.Enable();
        _groundDirectionPointsHandler.Initialize();
        _groundDirectionFinder.Initialize();
        _popupTextController.Initialize();

        OnServicesInitialized?.Invoke();
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
