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
        [ShowInInspector] private int _scoreCache;

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
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed += RefreshMoney;
            UISignals.Instance.onClickIncome += OnSetValueMultiplier;
        }

        private void OnSendMiniScore(int value)
        {
            _miniScore = value;
        }

        private void OnSetScore(int setScore)
        {
            _scoreCache = setScore;
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
        }
    }
}