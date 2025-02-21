using Assets.Scripts.Content;
using UnityEngine;

namespace Assets.Scripts.Architecture
{
    public static class PlayerEnterCondition
    {
        public static bool IsPlayer(GameObject checkObject)
        {
            if (!checkObject.TryGetComponent(out IEntity entity))
                return false;

            Flags flags = entity.ProvideComponent<Flags>();

            if (flags == null)
                return false;

            if (!flags.Contain(EntityFlags.Player))
                return false;

            return true;
        }
    }
}
