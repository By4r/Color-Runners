using DG.Tweening;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Obstacle
{
    public class ObstacleDroneAttackCommand
    {
        private ColorfulObstacleManager _colorfulObstacleManager;
        private GameObject _droneGameObject;

        public ObstacleDroneAttackCommand(ColorfulObstacleManager obstacleManager, GameObject droneGameObject)
        {
            _colorfulObstacleManager = obstacleManager;
            _droneGameObject = droneGameObject;
        }

        public void Execute()
        {
            Transform startPoint = _colorfulObstacleManager.startPoint;
            Transform midPoint = _colorfulObstacleManager.midPoint;
            Transform endPoint = _colorfulObstacleManager.endPoint;


            _droneGameObject.SetActive(true);

            _droneGameObject.transform.DOMove(midPoint.position, 2.0f)
                .OnComplete(() =>
                {
                    _droneGameObject.transform.DOMove(endPoint.position, 2.0f)
                        .OnComplete(() =>
                        {
                            if (!_colorfulObstacleManager.IsColorMatched)
                            {
                                StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
                                
                                Debug.LogWarning("DRONE SHOOT !");
                            }
                            
                            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
                            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Run);
                            
                            Debug.Log("DRONE ACTION COMPLETED!");
                            
                        });
                });
        }
    }
}