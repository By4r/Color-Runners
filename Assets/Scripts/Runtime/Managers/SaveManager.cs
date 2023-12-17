using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runtime.Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region EventSubscribtion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData += SaveData;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData -= SaveData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        

        [Button("SAVE DATA ")]
        private void SaveData()
        {
            Debug.LogWarning(ScoreSignals.Instance.onGetMiniScore());
            OnSaveGame(
                new SaveGameDataParams()
                {
                    MiniGameScore = ScoreSignals.Instance.onGetMiniScore(),
                    Level = CoreGameSignals.Instance.onGetLevelID(),
                }
            );
        }

        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            ES3.Save("MiniScore", saveDataParams.MiniGameScore);
            ES3.Save("Level", saveDataParams.Level);
        }
    }
}