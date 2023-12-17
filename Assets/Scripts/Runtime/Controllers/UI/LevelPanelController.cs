using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue += OnSetNewLevelValue;
            //UISignals.Instance.onSetMoneyValue += OnSetMoneyValue;
            //UISignals.Instance.onGetMoneyValue += OnGetMoneyValue;
        }


        private void OnSetNewLevelValue(byte levelValue)
        {
            levelText.text = "LEVEL " + ++levelValue;
        }


        private void UnsubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue -= OnSetNewLevelValue;
            //UISignals.Instance.onSetMoneyValue -= OnSetMoneyValue;
            //UISignals.Instance.onGetMoneyValue -= OnGetMoneyValue;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}