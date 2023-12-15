using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onInteractionATM = delegate { };
        public UnityAction<GameObject> onInteractionObstacle = delegate { };
        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction onInteractionObstacleWithPlayer = delegate { };
        public UnityAction<Vector2> onStackFollowPlayer = delegate { };
        public UnityAction onUpdateType = delegate { };
        public UnityAction onInteractionConveyor = delegate { };
        public UnityAction onUpdateAnimation = delegate { };
    }
}