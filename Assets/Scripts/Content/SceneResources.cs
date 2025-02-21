using UnityEngine;

namespace Assets.Scripts.Content
{
    [CreateAssetMenu(fileName = "SceneResources", menuName = "Slasher/SceneResources")]
    public class SceneResources : ScriptableObject
    {
        [SerializeField] private PopupText _damageTextPrefab;
        [SerializeField] private int _damageTextPoolSize;
        [SerializeField] private GameLoosePopup _gameLoosePopup;

        [Header("SceneNames")]
        [SerializeField] private string _mainMenu;
        [SerializeField] private string _firstLevel;

        public PopupText DamageTextPrefab => _damageTextPrefab;
        public int DamageTextPoolSize => _damageTextPoolSize;
        public GameLoosePopup GameLoosePopup => _gameLoosePopup;
        public string FirstLevel => _firstLevel;
        public string MainMenu => _mainMenu;
    }
}
