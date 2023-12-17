using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Building
{
    public class ChangeBuildingCommand
    {
        private MiniGameManager _miniGameManager;

        public ChangeBuildingCommand(MiniGameManager miniGameManager)
        {
            _miniGameManager = miniGameManager;
        }

        public void Execute()
        {
            if (_miniGameManager != null)
            {
                // Assuming Building1 is the second child of MiniGameManager
                Transform building1 = _miniGameManager.transform.GetChild(2);

                if (building1 != null)
                {
                    // Set the second child of Building1 to inactive
                    Transform secondChild = building1.GetChild(1);
                    if (secondChild != null)
                    {
                        secondChild.gameObject.SetActive(false);
                    }

                    // Set the third child of Building1 to active
                    Transform thirdChild = building1.GetChild(2);
                    if (thirdChild != null)
                    {
                        thirdChild.gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogError("Building1 not found as the second child of MiniGameManager.");
                }
            }
            else
            {
                Debug.LogError("Invalid MiniGameManager.");
            }
        }
    }
}