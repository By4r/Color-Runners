using System;
using DG.Tweening;
using Runtime.Commands.Stack;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody managerRigidbody;

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private bool _isColorMatchFailed;

        private readonly string _obstacle = "Obstacle";
        private readonly string _collectable = "Collectable";
        private readonly string _conveyor = "Conveyor";


        #region Color Changer Gates Tags

        private readonly string _gateRed = "Gate Red";
        private readonly string _gateBlue = "Gate Blue";
        private readonly string _gateGreen = "Gate Green";

        #endregion

        #region Colorful Ground Obstacle Types Tags

        private readonly string _colorfulObstacle = "Colorful Obstacle";
        private readonly string _colorfulDynamicObstacle = "Colorful Dynamic";

        #endregion

        #region Colorful Ground Obstacle Tag

        private readonly string _groundObstacle = "Colorful Ground";

        #endregion

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_obstacle))
            {
                managerRigidbody.transform.DOMoveZ(managerRigidbody.transform.position.z - 10f, 1f)
                    .SetEase(Ease.OutBack);

                StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();

                other.gameObject.SetActive(false);

                return;
            }

            if (other.CompareTag(_collectable))
            {
                CollectableManager collectableManager = other.transform.parent.GetComponent<CollectableManager>();

                if (collectableManager != null)
                {
                    CollectableColorTypes collectableColorType = collectableManager.collectableColorType;
                    CollectableColorTypes playerColorType = playerManager.playerColorType;

                    // Check if the color types match
                    if (collectableColorType == playerColorType)
                    {
                        Debug.Log("Color types match: " + collectableColorType);

                        // Continue with the rest of your logic
                        other.tag = "Collected";
                        StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
                        StackSignals.Instance.onUpdateAnimation?.Invoke();
                    }
                    else
                    {
                        Debug.Log("Color types do not match: " + collectableColorType);
                        StackSignals.Instance.onInteractionObstacleWithPlayer?.Invoke();
                        other.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Debug.LogError("CollectableManager component not found on the collectable GameObject.");
                }

                return;
            }

            if (other.CompareTag(_conveyor))
            {
                CoreGameSignals.Instance.onMiniGameEntered?.Invoke();
            }

            if (other.CompareTag(_gateBlue))
            {
                playerManager.UpgradePlayerVisual(CollectableColorTypes.Blue);

                Debug.LogWarning("BLUE PlAYER");
            }

            if (other.CompareTag(_gateGreen))
            {
                playerManager.UpgradePlayerVisual(CollectableColorTypes.Green);


                Debug.LogWarning("GREEN PLAYER");
            }

            if (other.CompareTag(_gateRed))
            {
                playerManager.UpgradePlayerVisual(CollectableColorTypes.Red);


                Debug.LogWarning("RED PLAYER");
            }


            if (other.CompareTag(_colorfulObstacle))
            {
                playerManager.StaticGroundObstacleState();

                Debug.LogWarning("PLAYER SPEED STATE IS SLOW ! ");
            }

            if (other.CompareTag(_colorfulDynamicObstacle))
            {
                playerManager.DynamicGroundObstacleState();

                Debug.LogWarning("DYNAMIC OBSTACLE !");
            }

            if (other.CompareTag(_groundObstacle))
            {
                Debug.Log("GROUND OBSTACLE");

                var otherMaterial = other.gameObject.GetComponent<MeshRenderer>().materials[0];
                var otherColor = CleanUpMaterialName(otherMaterial.name);

                var playerColor = playerManager.playerColorType.ToString();

                Debug.LogWarning("Other Color :" + otherColor);

                if (otherColor == playerManager.playerColorType.ToString())
                {
                    _isColorMatchFailed = false;

                    Debug.LogWarning("GROUND COLOR SAME !");

                    ObstacleSignals.Instance.onObstacleColorMatch?.Invoke(!_isColorMatchFailed);
                }
                else if (otherColor != playerManager.playerColorType.ToString())
                {
                    _isColorMatchFailed = true;

                    Debug.LogWarning("PLAYER COLOR " + playerColor);

                    Debug.LogWarning("GROUND COLOR DOESN'T MATCH!");

                    ObstacleSignals.Instance.onObstacleColorMatch?.Invoke(!_isColorMatchFailed);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_colorfulObstacle))
            {
                playerManager.SetNormalSpeed();

                Debug.LogWarning("PLAYER SPEED STATE IS NORMAL ! ");

                ResetColorMatchState();
            }
        }

        private string CleanUpMaterialName(string fullName)
        {
            // Example cleanup logic, you may need to adjust it based on your naming conventions
            int indexOfParenthesis = fullName.IndexOf(" (");
            return indexOfParenthesis >= 0 ? fullName.Substring(0, indexOfParenthesis) : fullName;
        }

        private void ResetColorMatchState()
        {
            _isColorMatchFailed = false;

            ObstacleSignals.Instance.onObstacleColorMatch?.Invoke(!_isColorMatchFailed);
        }
    }
}