using Runtime.Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Collectables
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

        #endregion

        #region Private Variables

        [ShowInInspector] private CollectableColorData _collectableColorData;

        #endregion

        #endregion


        internal void SetColorData(CollectableColorData colorData)
        {
            _collectableColorData = colorData;
        }


        internal void UpgradeCollectableVisualColor(int value)
        {
            skinnedMeshRenderer.material = _collectableColorData.MaterialsList[value];
            
            Debug.LogWarning("UPGRADED VISUAL!");
        }
    }
}