using JetBrains.Annotations;
using Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runtime.Controllers.Obstacles
{
    public class ObstacleDroneTEST : MonoBehaviour
    {
        [Button("DRONE ATTACK!")]
        public void DroneAttackFunction()
        {
            ObstacleSignals.Instance.onObstacleDroneAttack?.Invoke();
        }
    }
}