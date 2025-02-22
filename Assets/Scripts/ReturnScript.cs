using Assets.Scripts.Architecture;
using Assets.Scripts.Content.CoreProgression;
using Assets.Scripts.Content.PlayerLogic;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReturnScript : MonoBehaviour
{
private PlayerController _playerController;	
[SerializeField] private GameObject returnobj;
[Inject]
        private void Construct(PlayerController playerController)
        {
                   _playerController = playerController;
           
        }

/*private void Start()
        {
_playerController = playerController;
        }
*/
private void OnTriggerEnter2D(Collider2D collision)
        {           
            if (!PlayerEnterCondition.IsPlayer(collision.gameObject))
                return;

            returnobj.SetActive(true);
        }
}
