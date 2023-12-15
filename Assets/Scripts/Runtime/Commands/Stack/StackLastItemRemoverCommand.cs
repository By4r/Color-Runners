using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackLastItemRemoverCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableStack;
        private Transform _levelHolder;

        public StackLastItemRemoverCommand(StackManager stackManager, ref List<GameObject> collectableStack)
        {
            _stackManager = stackManager;
            _collectableStack = collectableStack;
            _levelHolder = GameObject.Find("LevelHolder").transform;
        }


        public void Execute()
        {
            // Remove the last item from the stack
            if (_collectableStack.Count > 0)
            {
                int last = _collectableStack.Count - 1;
                GameObject lastItem = _collectableStack[last];
                _collectableStack.RemoveAt(last);
                _collectableStack.TrimExcess();

                // Additional logic for removing the last item
                lastItem.transform.SetParent(_levelHolder.transform.GetChild(0));
                lastItem.SetActive(false);
                _stackManager.StackJumperCommand.Execute(last - 1,
                    last); // Assuming StackJumperCommand needs adjustments
                _stackManager.StackTypeUpdaterCommand.Execute();
            }
        }
    }
}