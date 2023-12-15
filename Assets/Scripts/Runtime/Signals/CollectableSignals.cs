using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    {
        public UnityAction<int> onCollectableUpgrade = delegate { };
        public UnityAction<CollectableAnimationStates> onChangeCollectableAnimationState = delegate { };
    }
}