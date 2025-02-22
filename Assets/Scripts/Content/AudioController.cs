using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _SFXSource;
        [SerializeField] private AudioSource _stepSource;

        private SceneResources _sceneResources;
        private bool _isStepsPlaying;
        private float _nextStepTime = 0f;

        [Inject]
        private void Construct(SceneResources sceneResources)
        {
            _sceneResources = sceneResources;
        }

        public enum SoundEffects
        {
            PlayerHit,
            CakeHit,
            SausageHit,
            BreadHit,
            GroundHit,
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
