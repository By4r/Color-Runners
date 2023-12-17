using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private int _miniScore;
        [ShowInInspector] private int _stackValueMultiplier;
        [ShowInInspector] private int _scoreCache = 0;
        [ShowInInspector] private int _atmScoreValue = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _miniScore = GetMiniScoreValue();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onSendMiniScore += OnSendMiniScore;
            ScoreSignals.Instance.onGetMiniScore += () => _miniScore;
            ScoreSignals.Instance.onSetScore += OnSetScore;
            //ScoreSignals.Instance.onSetAtmScore += OnSetAtmScore;

            //CoreGameSignals.Instance.onMiniGameReady += OnMiniGameReady;

            // CoreGameSignals.Instance.onMiniGameStart +=
            //     () => ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed += RefreshMoney;
            UISignals.Instance.onClickIncome += OnSetValueMultiplier;
        }

        // private void OnMiniGameReady()
        // {
        //     Debug.LogWarning("ON MINI GAME READY");
        //     
        //     ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
        //
        //     MiniGameSignals.Instance.onMiniGameStart?.Invoke();
        // }

        private void OnSendMiniScore(int value)
        {
            _miniScore = value;
        }

        private void OnSetScore(int setScore)
        {
            _scoreCache = setScore;
            //PlayerSignals.Instance.onSetTotalScore?.Invoke(_scoreCache);
        }

        private void OnSetAtmScore(int atmValues)
        {
            _atmScoreValue += atmValues * _stackValueMultiplier;
            AtmSignals.Instance.onSetAtmScoreText?.Invoke(_atmScoreValue);
        }

        private void OnSetValueMultiplier()
        {
            _stackValueMultiplier = CoreGameSignals.Instance.onGetIncomeLevel();
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onSendMiniScore -= OnSendMiniScore;
            ScoreSignals.Instance.onGetMiniScore -= () => _miniScore;
            ScoreSignals.Instance.onSetScore -= OnSetScore;
            //ScoreSignals.Instance.onSetAtmScore -= OnSetAtmScore;

            //CoreGameSignals.Instance.onMiniGameReady -= OnMiniGameReady;


            // CoreGameSignals.Instance.onMiniGameStart -=
            //     () => ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed -= RefreshMoney;
            UISignals.Instance.onClickIncome -= OnSetValueMultiplier;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            OnSetValueMultiplier();
            RefreshMoney();
        }

        private int GetMiniScoreValue()
        {
            if (!ES3.FileExists()) return 0;
            return (int)(ES3.KeyExists("MiniScore") ? ES3.Load<int>("MiniScore") : 0);
        }

        private void RefreshMoney()
        {
            _miniScore += (int)(_scoreCache * ScoreSignals.Instance.onGetMultiplier());
            UISignals.Instance.onSetMoneyValue?.Invoke(_miniScore);
        }

        private void OnReset()
        {
            _scoreCache = 0;
            _atmScoreValue = 0;
        }
    }
}