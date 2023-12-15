using System;
using Runtime.Controllers.Collectables;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableMeshController meshController;
        [SerializeField] private CollectablePhysicsController physicsController;
        [SerializeField] private CollectableAnimationController animationController;

        public CollectableColorTypes collectableColorType;

        [Space] [HideInInspector] public CollectableColorTypes playerColorType;

        #endregion

        #region Private Variables

        [ShowInInspector] private CollectableData _data;
        [ShowInInspector] private byte _currentValue = 0;

        private readonly string _collectableDataPath = "Data/CD_Collectable";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetCollectableData();
            SendDataToController();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerColorType += OnPlayerColorType;
        }

        private void OnPlayerColorType(CollectableColorTypes colorTypeValue)
        {
            playerColorType = colorTypeValue;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerColorType -= OnPlayerColorType;
        }

        private void Start()
        {
            meshController.UpgradeCollectableVisualColor((int)collectableColorType);
        }

        private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>(_collectableDataPath).Data;

        private void SendDataToController()
        {
            meshController.SetColorData(_data.ColorData);
        }


        internal void CollectableUpgrade(int value)
        {
            meshController.UpgradeCollectableVisualColor(value);
            Debug.LogWarning("VALUE :" + value);
            StackSignals.Instance.onUpdateType?.Invoke();
        }

        internal void CollectableAnimChange()
        {
            if (IsCurrentTrigger("Idle"))
            {
                animationController.AnimationState(CollectableAnimationStates.Run);
            }
        }

        // Add a method to check the current trigger
        private bool IsCurrentTrigger(string triggerName)
        {
            // You may need to adapt this based on your actual implementation
            // This assumes that you have a way to retrieve the current trigger from the animator
            return animationController.animator.GetCurrentAnimatorStateInfo(0).IsName(triggerName);
        }


        public byte GetCurrentValue()
        {
            return _currentValue;
        }

        public void InteractionWithCollectable(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionCollectable?.Invoke(collectableGameObject);
            StackSignals.Instance.onUpdateAnimation?.Invoke();

            Debug.LogWarning("SELECTED ANIMATION STATE CHANGED");
        }

        public void InteractionWithAtm(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionATM?.Invoke(collectableGameObject);
        }

        public void InteractionWithObstacle(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
        }

        public void InteractionWithConveyor()
        {
            StackSignals.Instance.onInteractionConveyor?.Invoke();
        }

        public CollectableColorTypes CollectableColorType => collectableColorType;
    }
}