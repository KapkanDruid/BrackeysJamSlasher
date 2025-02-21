using UnityEngine;

namespace Assets.Scripts.Content
{
    [CreateAssetMenu(fileName = "SceneResources", menuName = "Slasher/SceneResources")]
    public class SceneResources : ScriptableObject
    {
        [SerializeField] private PopupText _damageTextPrefab;
        [SerializeField] private int _damageTextPoolSize;
        [SerializeField] private SceneTransiter _sceneTransiter;

        public PopupText DamageTextPrefab => _damageTextPrefab;
        public int DamageTextPoolSize => _damageTextPoolSize;
        public SceneTransiter SceneTransiter => _sceneTransiter;
    }
}
