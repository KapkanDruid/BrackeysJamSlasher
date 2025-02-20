using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterHandler : MonoBehaviour, IEntity
    {
        [SerializeField] private CharacterData _characterData;

        private CharacterHealthHandler _healthHandler;
        private CancellationToken _cancellationToken;
        private bool _isKnocked = false;

        public  CancellationToken CancellationToken => _cancellationToken;
        public CharacterData CharacterDatas => _characterData;

        public bool IsKnocked { get => _isKnocked; set => _isKnocked = value; }

        [Inject]
        public void Construct(CharacterHealthHandler healthHandler)
        {
            _healthHandler = healthHandler;
            _characterData.ThisEntity = this;
            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }

        public void MoveTo(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _characterData.Speed * Time.deltaTime);
        }

        public T ProvideComponent<T>() where T : class
        {
            if (_characterData.Flags is T flags)
                return flags;

            if (_healthHandler is T healthHandler)
                return healthHandler;

            return null;
        }

        private void OnDrawGizmos()
        {
            if (_characterData == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _characterData.SensorRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + _characterData.HitColliderOffset, _characterData.HitColliderSize);
        }
    }
}

