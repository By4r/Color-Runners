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
            // Deactivate all GameObjects in the list before clearing it
            _collectableStack.ForEach(collectableGameObject => collectableGameObject.SetActive(false));

            foreach (var collectableGameObject in _collectableStack)
            {
                int index = _collectableStack.IndexOf(collectableGameObject);
                int last = _collectableStack.Count - 1;
                collectableGameObject.transform.SetParent(_levelHolder.transform.GetChild(0));
                _stackManager.StackJumperCommand.Execute(last, index);
            }

            _collectableStack.Clear(); // Clear all elements from the list
            //_stackManager.StackTypeUpdaterCommand.Execute();
            //_stackManager.OnSetStackAmount();
        }


        /*public void Execute()
        {

            foreach (var collectableGameObject in _collectableStack)
            {
                int index = _collectableStack.IndexOf(collectableGameObject);
                int last = _collectableStack.Count - 1;
                collectableGameObject.transform.SetParent(_levelHolder.transform.GetChild(0));
                collectableGameObject.SetActive(false);
                _stackManager.StackJumperCommand.Execute(last, index);
            }

            _collectableStack.ForEach(collectableGameObject => collectableGameObject.SetActive(false));

            _collectableStack.Clear(); // Clear all elements from the list
            _stackManager.StackTypeUpdaterCommand.Execute();
            _stackManager.OnSetStackAmount();
        }*/
    }
}