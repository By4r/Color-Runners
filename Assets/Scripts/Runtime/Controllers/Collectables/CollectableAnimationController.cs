using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Collectables
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] internal Animator animator;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            animator.SetTrigger("Idle");
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onChangeCollectableAnimationState += OnChangeAnimationState;
        }


        internal void AnimationState(CollectableAnimationStates animationStates)
        {
            animator.SetTrigger(animationStates.ToString());
        }

        private void OnChangeAnimationState(CollectableAnimationStates animationState)
        {
            //animator.SetTrigger(animationState.ToString());
            //Debug.LogWarning("SELECTED ANIMATION STATE :" + animationState);
        }

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onChangeCollectableAnimationState -= OnChangeAnimationState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        internal void OnReset()
        {
            CollectableSignals.Instance.onChangeCollectableAnimationState?.Invoke(CollectableAnimationStates.Idle);
        }
    }
}