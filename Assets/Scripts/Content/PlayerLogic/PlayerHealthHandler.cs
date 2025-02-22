using Assets.Scripts.Architecture;
using Assets.Scripts.Content.CoreProgression;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerHealthHandler : IDamageable, IDisposable
    {
        private readonly PlayerData _data;
        private readonly Animator _animator;
        private readonly PopupTextController _popupTextController;
        private readonly CharacterJumpHandler _characterJumpHandler;
        private readonly GameEndController _gameEndController;
        private readonly AudioController _audioController;
        private readonly HeadUpDisplay _headUpDisplay;

        private float _currentHealth;
        private bool _canBeDamaged;
        private bool _isDead;

        public bool IsDead => _isDead;

        public PlayerHealthHandler(
            PlayerData playerData,
            PopupTextController popupTextController,
            CharacterJumpHandler characterJumpHandler,
            Animator animator,
            GameEndController gameEndController,
            AudioController audioController,
            HeadUpDisplay headUpDisplay)
        {
            _data = playerData;
            _animator = animator;
            _popupTextController = popupTextController;
            _characterJumpHandler = characterJumpHandler;

            _canBeDamaged = true;
            _isDead = false;
            _gameEndController = gameEndController;
            _audioController = audioController;
            _headUpDisplay = headUpDisplay;
        }

        public void Initialize()
        {
            if (StaticData.CurrentPlayerHP > 0)
                _currentHealth = StaticData.CurrentPlayerHP;
            else
            {
                _currentHealth = _data.MaxHealth;
                StaticData.CurrentPlayerHP = _currentHealth;
            }

            StaticData.OnMaxHealthChanged += OnMaxHealthChanged;
            _headUpDisplay.ChangeHealth(_currentHealth / _data.MaxHealth);
        }

        public void OnMaxHealthChanged()
        {
            _headUpDisplay.ChangeHealth(_currentHealth / _data.MaxHealth);
        }

        public void TakeDamage(float damage, Action callBack = null)
        {
            if (_isDead)
                return;

            if (!_characterJumpHandler.IsGrounded)
                return;

            if (!_canBeDamaged)
                return;

            _canBeDamaged = false;

            _animator.SetTrigger(AnimatorHashes.TakeDamageTrigger);

            _audioController.PlayOneShot(AudioController.SoundEffects.PlayerHit);

            if (UnityEngine.Random.Range(0, 100) < _data.DodgeChancePercent)
            {
                _popupTextController.ShowDodge(_data.DamageTextPoint.position);
            }
            else
            {
                _currentHealth -= damage;
                _popupTextController.ShowDamage(_data.DamageTextPoint.position, damage);

                _headUpDisplay.ChangeHealth(_currentHealth / _data.MaxHealth);
            }

            callBack?.Invoke();

            ActivateInvincibleFrames().Forget();

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                _isDead = true;
                _animator.SetTrigger(AnimatorHashes.DeathTrigger);

                _headUpDisplay.ChangeHealth(_currentHealth / _data.MaxHealth);

                _gameEndController.OnPlayerDeath();
            }
        }

        private async UniTaskVoid ActivateInvincibleFrames()
        {
            try
            {
                await UniTask.WaitForSeconds(_data.InvincibleFramesDuration, cancellationToken: _data.CancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            _canBeDamaged = true;
        }


        public void Heal()
        {
            _popupTextController.ShowHeal(_data.DamageTextPoint.position, _currentHealth * _data.HealPercent / 100);

            var healAmount = (1 + _data.HealPercent / 100);
            _currentHealth *= healAmount;

            if (_currentHealth > _data.MaxHealth)
            {
                _currentHealth = _data.MaxHealth;
            }

            _headUpDisplay.ChangeHealth(_currentHealth / _data.MaxHealth);
        }

        public void Dispose()
        {
            StaticData.CurrentPlayerHP = _currentHealth;
            StaticData.OnMaxHealthChanged -= OnMaxHealthChanged;
        }
    }
}
