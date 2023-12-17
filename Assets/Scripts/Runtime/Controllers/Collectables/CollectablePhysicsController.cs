using Runtime.Enums;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Controllers.Collectables
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableManager manager;

        #endregion

        #region Private Variables

        private readonly string _player = "Player";

        private readonly string _collectable = "Collectable";
        private readonly string _collected = "Collected";
        private readonly string _gate = "Gate";

        private readonly string _gateRed = "Gate Red";
        private readonly string _gateBlue = "Gate Blue";
        private readonly string _gateGreen = "Gate Green";

        private readonly string _obstacle = "Obstacle";
        private readonly string _conveyor = "Conveyor";

        private GateTypes _gateTypes;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_gate) && CompareTag(_collected))
            {
                manager.CollectableUpgrade(manager.GetCurrentValue());
            }
            
            if (other.CompareTag(_obstacle) && CompareTag(_collected))
            {
                manager.InteractionWithObstacle(transform.parent.gameObject);
            }

            if (other.CompareTag(_conveyor) && CompareTag(_collected))
            {
                manager.InteractionWithConveyor();
            }

            // ------------------------------------------------------------

            if (other.CompareTag(_gateBlue) && CompareTag(_collected))
            {
                manager.CollectableUpgrade((int)GateTypes.GateBlue);
                Debug.LogWarning("BLUE GATE");
            }

            if (other.CompareTag(_gateGreen) && CompareTag(_collected))
            {
                manager.CollectableUpgrade((int)GateTypes.GateGreen);
                Debug.LogWarning("GREEN GATE");
            }

            if (other.CompareTag(_gateRed) && CompareTag(_collected))
            {
                manager.CollectableUpgrade((int)GateTypes.GateRed);
                Debug.LogWarning("RED GATE");
            }
        }
    }
}