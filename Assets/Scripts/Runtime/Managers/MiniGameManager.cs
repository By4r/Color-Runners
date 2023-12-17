using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Controllers.MiniGame;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Veriables

        [ShowInInspector] private GameObject buildingObject;

        [SerializeField] private GameObject wallObject;
        [SerializeField] private GameObject fakeMoneyObject;
        [SerializeField] private Transform fakePlayer;
        [SerializeField] private Material mat;

        [SerializeField] private short wallCount, fakeMoneyCount;

        [SerializeField] private WallCheckController wallChecker;

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

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetBuildData();

            _scoreThresholdBuilding = _data[0].buildRequirement;
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
            //MiniGameSignals.Instance.onMiniGameStart += OnMiniGameStart;
            CoreGameSignals.Instance.onReset += OnReset;
        }


        private void OnMiniGameStart()
        {
            Debug.LogWarning("ON MINI GAME START SIGNAL");

            fakePlayer.gameObject.SetActive(true);
            StartCoroutine(GoResult());
        }

        private IEnumerator GoResult()
        {
            yield return new WaitForSeconds(1f);

            Debug.Log("FINAL SCORE " + _score);

            //CoreGameSignals.Instance.on

            if (_score >= _scoreThresholdBuilding)
            {
                //CoreGameSignals.Instance.onLevelFailed?.Invoke();

                Debug.LogWarning("_SCORE BIGGER !");
            }
            else
            {
                //fakePlayer.DOLocalMoveY(Mathf.Clamp(_score, 0, 900), 2.7f).SetEase(Ease.Flash).SetDelay(1f);
            }

            yield return new WaitForSeconds(4.5f);
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
            _score = scoreValue;
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onSendFinalScore -= OnSendScore;
            ScoreSignals.Instance.onGetMultiplier -= OnGetMultiplier;
            //MiniGameSignals.Instance.onMiniGameStart -= OnMiniGameStart;
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


            if (textMeshPro != null)
            {
                textMeshPro.text =
                    $"{_score} / {_scoreThresholdBuilding}";
            }

            //SpawnWallObjects();
            //SpawnFakeMoneyObjects();
            //Init();
        }

        private void SpawnBuildObjects(GameObject buildingGameObject)
        {
            var ob = Instantiate(buildingGameObject, transform);
        }

        private void Init()
        {
            _initializePos = fakePlayer.localPosition;
        }

        private void SpawnWallObjects()
        {
            for (int i = 0; i <= wallCount; i++)
            {
                var ob = Instantiate(wallObject, transform);
                ob.transform.localPosition = new Vector3(0, i * 10, 0);
                ob.transform.GetChild(0).GetComponent<TextMeshPro>().text = "x" + ((i / 10f) + 1f);
            }
        }

        private void SpawnFakeMoneyObjects()
        {
            for (int i = 0; i < fakeMoneyCount; i++)
            {
                var ob = Instantiate(fakeMoneyObject, fakePlayer);
                ob.transform.localPosition = new Vector3(0, -i * 1.58f, -7);
            }
        }

        private void ResetWalls()
        {
            for (int i = 1; i < wallCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = mat;
                transform.GetChild(i).transform.position = Vector3.zero;
            }
        }

        private void OnReset()
        {
            StopAllCoroutines();
            DOTween.KillAll();
            ResetWalls();
            ResetFakePlayer();
        }

        private void ResetFakePlayer()
        {
            fakePlayer.gameObject.SetActive(false);
            fakePlayer.localPosition = _initializePos;
            wallChecker.OnReset();
        }
    }
}