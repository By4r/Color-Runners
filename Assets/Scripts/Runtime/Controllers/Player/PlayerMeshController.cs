using System;
using Runtime.Data.ValueObject;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeField] private TextMeshPro scoreText;

        #endregion

        #region Private Variables

        [ShowInInspector] private CollectableColorData _collectableColorData;

        #endregion

        #endregion
        
        internal void SetColorData(CollectableColorData colorData)
        {
            _collectableColorData = colorData;
        }

        internal void SetTotalScore(int value)
        {
            scoreText.text = value.ToString();
        }

        internal void UpgradePlayerVisualColor(int value)
        {
            skinnedMeshRenderer.material = _collectableColorData.MaterialsList[value];

            //meshRenderer.material = _collectableColorData.MaterialsList[value];
            //meshRenderer.materials[0] = _collectableColorData.MaterialsList[value];
            Debug.LogWarning("UPGRADED VISUAL PLAYER!");
        }
    }
}