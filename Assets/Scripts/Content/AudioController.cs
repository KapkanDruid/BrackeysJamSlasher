using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Content
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _SFXSource;
        [SerializeField] private AudioSource _stepSource;

        private AudioClip[] _musicClips;
        private AudioClip _currentClip;

        private SceneResources _sceneResources;
        private bool _isStepsPlaying;
        private float _nextStepTime = 0f;

        private int _currentClipIndex;

        public enum SoundEffects
        {
            PlayerHit,
            CakeHit,
            SausageHit,
            BreadHit,
            GroundHit,
        }

        [Inject]
        private void Construct(SceneResources sceneResources)
        {
            _sceneResources = sceneResources;
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == _sceneResources.MainMenu)
                _musicClips = _sceneResources.MainMenuMusic;
            else
                _musicClips = _sceneResources.LevelMusic;

            _currentClip = _musicClips[Random.Range(0, _musicClips.Length - 1)];
        }


        public void PlayOneShot(SoundEffects effectType)
        {
            foreach (var clip in _sceneResources.SoundEffect)
            {
                if (clip.EffectType == effectType)
                    _SFXSource.PlayOneShot(clip.EffectClip);
            }
        }

        private void Update()
        {
            if (_isStepsPlaying && Time.time >= _nextStepTime)
            {
                PlayRandomFootstep();
                _nextStepTime = Time.time + _sceneResources.StepSoundDelay;
            }

            if (!_musicSource.isPlaying)
            {
                PlayNextClip();
            }
        }

        private void PlayNextClip()
        {
            _musicSource.clip = _currentClip;

            _musicSource.Play();
        }
    
        public void StartFootsteps()
        {
            if (_isStepsPlaying)
                return;

            if (_sceneResources.StepSounds.Length == 0 || _stepSource == null) 
                return;

            _isStepsPlaying = true;
            _nextStepTime = Time.time;
        }

        public void StopFootsteps()
        {
            _isStepsPlaying = false;
        }

        private void PlayRandomFootstep()
        {
            if (_sceneResources.StepSounds.Length == 0) 
                return;

            int randomIndex = Random.Range(0, _sceneResources.StepSounds.Length);
            _stepSource.PlayOneShot(_sceneResources.StepSounds[randomIndex]);
        }

    }
}
