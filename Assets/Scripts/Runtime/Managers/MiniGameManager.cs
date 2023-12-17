using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.Building;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Veriables

        [ShowInInspector] private GameObject buildingObject;

        [SerializeField] private GameObject wallObject;
        [SerializeField] private Material mat;
        [SerializeField] private TextMeshPro textMeshPro;

        #endregion

        #region Private Veriables

        private int _score;
        private float _multiplier;
        private Vector3 _initializePos;
        private int _scoreThresholdBuilding;
        //private int _currentBuildingScore;

        [ShowInInspector] private List<BuildData> _data;

        private readonly string _buildDataPath = "Data/CD_Build";

        private ChangeBuildingCommand _changeBuildingCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBuildData();

            _scoreThresholdBuilding = _data[0].buildRequirement;

            Init();
        }


        private List<BuildData> GetBuildData() => Resources.Load<CD_Build>(_buildDataPath).BuildDataList;


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onSendFinalScore += OnSendScore;
            ScoreSignals.Instance.onGetMultiplier += OnGetMultiplier;
            CoreGameSignals.Instance.onMiniGameStart += OnMiniGameStart;
            CoreGameSignals.Instance.onReset += OnReset;
        }


        private void OnMiniGameStart()
        {
            Debug.LogWarning("ON MINI GAME START SIGNAL");


            StartCoroutine(GoResult());
        }

        private IEnumerator GoResult()
        {
            yield return new WaitForSeconds(1f);

            Debug.Log("FINAL SCORE " + _score);


            if (_score >= _scoreThresholdBuilding)
            {
                _changeBuildingCommand.Execute();


                Debug.LogWarning("_SCORE BIGGER !");

                Debug.LogWarning("BUILDING CHANGED !");
            }
            else
            {
                Debug.LogWarning("_SCORE SMALLER !");
            }

            StackSignals.Instance.onClearStack?.Invoke();

            yield return new WaitForSeconds(2.5f);
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        }

        internal void SetMultiplier(float multiplierValue)
        {
            _multiplier = multiplierValue;
        }

        private float OnGetMultiplier()
        {
            return _multiplier;
        }

        private void OnSendScore(int scoreValue)
        {
            _score += scoreValue;

            textMeshPro.text =
                $"{_score} / {_scoreThresholdBuilding}";

            Debug.LogWarning("ON SEND SCORE FUNCTION WORKED!");
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onSendFinalScore -= OnSendScore;
            ScoreSignals.Instance.onGetMultiplier -= OnGetMultiplier;
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGameStart;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            buildingObject = _data[0].buildingPrefab;

            SpawnBuildObjects(buildingObject);


            _score = ScoreSignals.Instance.onGetMiniScore();

            Debug.LogWarning("MINI GAME SCORE :" + _score);

            if (textMeshPro != null)
            {
                textMeshPro.text =
                    $"{_score} / {_scoreThresholdBuilding}";
            }
        }

        private void SpawnBuildObjects(GameObject buildingGameObject)
        {
            var ob = Instantiate(buildingGameObject, transform);
        }

        private void Init()
        {
            _changeBuildingCommand = new ChangeBuildingCommand(this);
        }


        private void OnReset()
        {
            StopAllCoroutines();
            DOTween.KillAll();
        }
    }
}