using System;
using Runtime.Commands.Obstacle;
using Runtime.Controllers.Obstacles;
using Runtime.Signals;
using Sirenix.OdinInspector;
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

        [ShowInInspector] internal bool IsColorMatched;

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
            ObstacleSignals.Instance.onObstacleColorMatch += OnObstacleColorMatchState;
        }

        private void OnObstacleColorMatchState(bool state)
        {
            IsColorMatched = state;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            ObstacleSignals.Instance.onObstacleDroneAttack -= _obstacleAttackCommand.Execute;
            ObstacleSignals.Instance.onObstacleColorMatch -= OnObstacleColorMatchState;

        }
    }
}