using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Content
{
    public class HeadUpDisplay : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        public void ChangeHealth(float normalizedHealth)
        {
            normalizedHealth = Mathf.Clamp01(normalizedHealth);

            _healthBar.fillAmount = normalizedHealth;
        }
    }
}
