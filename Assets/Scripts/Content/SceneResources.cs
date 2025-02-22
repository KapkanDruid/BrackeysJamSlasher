using Assets.Scripts.Content.CoreProgression;
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
        [SerializeField] private ProgressCardsPopup _progressCardsPopup;

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
        public ProgressCardsPopup ProgressCardsPopup => _progressCardsPopup;
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
