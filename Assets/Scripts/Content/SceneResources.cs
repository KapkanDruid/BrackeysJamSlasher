using Assets.Scripts.Content.BasicAI;
using Assets.Scripts.Content.CoreProgression;
using Assets.Scripts.Content.PlayerLogic;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Content.AudioController;

namespace Assets.Scripts.Content
{
    [CreateAssetMenu(fileName = "SceneResources", menuName = "Slasher/SceneResources")]
    public class SceneResources : ScriptableObject
    {
        [SerializeField] private PopupText _damageTextPrefab;
        [SerializeField] private PopupText _healTextPrefab;
        [SerializeField] private PopupText _dodgeTextPrefab;
        [SerializeField] private int _damageTextPoolSize;
        [SerializeField] private GameLoosePopup _gameLoosePopup;
        [SerializeField] private HeadUpDisplay _HUDPrefab;
        [SerializeField] private StopwatchTimer _stopwatchTimer;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private CharacterConfig _cupcakeConfig;
        [SerializeField] private CharacterConfig _sausageConfig;
        [SerializeField] private CharacterConfig _breadConfig;
        [SerializeField] private CharacterConfig _meatConfig;

        [Header("Sound Settings")]
        [SerializeField] private List<SoundEffects> _soundEffect;
        [SerializeField] private AudioClip[] _stepSounds;
        [SerializeField] private float _stepSoundDelay = 0.4f;

        [Header("SceneNames")]
        [SerializeField] private string _mainMenu;
        [SerializeField] private string _firstLevel;

        [Header("Time settings")]
        [SerializeField] private float _maxATime;
        [SerializeField] private float _maxBTime;

        public PopupText DamageTextPrefab => _damageTextPrefab;
        public int DamageTextPoolSize => _damageTextPoolSize;
        public GameLoosePopup GameLoosePopup => _gameLoosePopup;
        public string FirstLevel => _firstLevel;
        public string MainMenu => _mainMenu;
        public List<SoundEffects> SoundEffect => _soundEffect;
        public AudioClip[] StepSounds => _stepSounds;
        public float StepSoundDelay => _stepSoundDelay;
        public HeadUpDisplay HUDPrefab => _HUDPrefab;
        public StopwatchTimer StopwatchTimer => _stopwatchTimer;
        public PopupText HealTextPrefab => _healTextPrefab;
        public PopupText DodgeTextPrefab => _dodgeTextPrefab;
        public float MaxATime => _maxATime;
        public float MaxBTime => _maxBTime;
        public PlayerConfig PlayerConfig => _playerConfig;
        public CharacterConfig CupcakeConfig => _cupcakeConfig;
        public CharacterConfig SausageConfig => _sausageConfig;
        public CharacterConfig BreadConfig => _breadConfig;
        public CharacterConfig MeatConfig => _meatConfig;
    }

    [Serializable]
    public class SoundEffects
    {
        [SerializeField] private AudioController.SoundEffects _effectType;
        [SerializeField] private AudioClip _effectClip;

        public AudioController.SoundEffects EffectType => _effectType;
        public AudioClip EffectClip => _effectClip;
    }
}
