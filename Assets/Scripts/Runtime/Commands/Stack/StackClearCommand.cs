using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackClearCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableStack;
        private Transform _levelHolder;

        public StackClearCommand(StackManager stackManager, ref List<GameObject> collectableStack)
        {
            _stackManager = stackManager;
            _collectableStack = collectableStack;
            _levelHolder = GameObject.Find("LevelHolder").transform;
        }

        public void Execute()
        {
            foreach (var collectableGameObject in _collectableStack)
            {
                collectableGameObject.transform.SetParent(_levelHolder.transform.GetChild(0));
                collectableGameObject.SetActive(false);
            }

            _collectableStack.Clear();
            _stackManager.StackTypeUpdaterCommand.Execute();
            _stackManager.OnSetStackAmount();
        }
    }
}