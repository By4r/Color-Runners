using System;
using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onSetScore = delegate { };
        public UnityAction<int> onSetAtmScore = delegate { };
        public UnityAction<int> onSendFinalScore = delegate { };
        public UnityAction<int> onSendMiniScore = delegate { };
        public Func<int> onGetMiniScore = delegate { return 0; };
        
        public Func<float> onGetMultiplier = delegate { return 0; };
    }
}