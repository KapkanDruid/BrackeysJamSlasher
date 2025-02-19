using System;

namespace Assets.Scripts.Architecture
{
    public interface IDamageable
    {
        public void TakeDamage(float damage, Action callBack = null);
    }
}