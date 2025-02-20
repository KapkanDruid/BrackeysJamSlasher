﻿using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private CharacterHandler _characterHandler;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;

        public override void InstallBindings()
        {
            Container.Bind<CharacterHealthHandler>().AsSingle().NonLazy();
            Container.Bind<CharacterHandler>().FromComponentOnRoot().AsSingle().NonLazy();
            Container.Bind<CharacterData>().FromInstance(_characterHandler.CharacterData).AsSingle();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<Rigidbody2D>().FromInstance(_rigidbody).AsSingle();
            Container.Bind<CharacterStateMachine>().FromComponentOnRoot().AsSingle();
            Container.Bind<GizmosDrawer>().FromComponentInChildren().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterSensor>().AsSingle().NonLazy();
        }
    }
}