using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Content
{
    [Serializable]
    public class EntitySubList
    {
        [SerializeField] private List<GameObject> _entities;

        public List<IEntity> Entities
        {
            get
            {
                List<IEntity> entities = new();

                foreach (var gameObject in _entities)
                {
                    if (gameObject.TryGetComponent(out IEntity entity))
                        entities.Add(entity);
                    else
                        Debug.LogError("Entities array must contain only IEntity objects!");
                }

                return entities;
            }
        }
    }
}
