using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Obstacle
{
    public class ObstacleNormalAttackCommand
    {
        private ColorfulObstacleManager _colorfulObstacleManager;

        public ObstacleNormalAttackCommand(ColorfulObstacleManager obstacleManager)
        {
            _colorfulObstacleManager = obstacleManager;
        }

        public void Execute()
        {
            if (!_colorfulObstacleManager.IsColorMatched)
            {
                StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();

                Debug.LogWarning("TURRET SHOOT !");
            }
        }
    }
}