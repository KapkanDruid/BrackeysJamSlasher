using Assets.Scripts.Architecture;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Content
{
    public class HeadUpDisplay : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private PausePopup _popup;
        [SerializeField] private Button _pauseButton;

        private SceneTransiter _transiter;
        private SceneResources _resources;
        private InputSystemActions _inputActions;

        public void Initialize(SceneTransiter sceneTransiter, SceneResources sceneResources, InputSystemActions inputActions)
        {
            _resources = sceneResources;
            _transiter = sceneTransiter;
            _inputActions = inputActions;
            _pauseButton.onClick.AddListener(_popup.Show);

            _popup.Initialize(OnExit, () => _popup.Hide());
            _inputActions.Player.Pause.performed += context => DeterminePopup();
        }

        private void DeterminePopup()
        {
            if (_popup.isActiveAndEnabled)
                _popup.Hide();
            else
                _popup.Show();
        }

        private void OnExit()
        {
            Time.timeScale = 1;
            _transiter.SwitchToScene(_resources.MainMenu);
        }

        public void ChangeHealth(float normalizedHealth)
        {
            normalizedHealth = Mathf.Clamp01(normalizedHealth);

            _healthBar.fillAmount = normalizedHealth;
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
                _inputActions.Player.Pause.performed -= context => DeterminePopup();
        }
    }
}
