using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ObstacleSignals : MonoSingleton<ObstacleSignals>
    {
        public UnityAction onObstacleDroneAttack = delegate { };
        public UnityAction<bool> onObstacleColorMatch = delegate { };
    }
}