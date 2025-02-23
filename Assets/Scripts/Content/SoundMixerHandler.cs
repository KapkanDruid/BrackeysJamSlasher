using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Content
{
    public class SoundMixerHandler : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        public void SetEffectsVolume(float value)
        {
            _audioMixer.SetFloat("SFXValue", Mathf.Log10(value) * 20);
        }
        
        public void SetMusicVolume(float value)
        {
            _audioMixer.SetFloat("MusicValue", Mathf.Log10(value) * 20);

        }
    }
}
