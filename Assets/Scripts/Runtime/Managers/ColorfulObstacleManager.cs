using System;
using Runtime.Commands.Obstacle;
using Runtime.Controllers.Obstacles;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Runtime.Managers
{
    public class ColorfulObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        //[SerializeField] private ObstacleDroneController obstacleDroneController;

        [SerializeField] private GameObject droneGameObject;

        #endregion


        #region Drone Path Variables

        [SerializeField] internal Transform startPoint, midPoint, endPoint;

        #endregion


        #region Private Variables

        private ObstacleAttackCommand _obstacleAttackCommand;

        #endregion

        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _obstacleAttackCommand = new ObstacleAttackCommand(this, droneGameObject);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ObstacleSignals.Instance.onObstacleDroneAttack += _obstacleAttackCommand.Execute;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            ObstacleSignals.Instance.onObstacleDroneAttack -= _obstacleAttackCommand.Execute;
        }
    }
}