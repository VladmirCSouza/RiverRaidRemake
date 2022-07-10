using System;
using Channel3.Utils;
using UnityEngine;

namespace Channel3.CoreManagers
{
    public class GameManager : Singleton<GameManager>
    {
        private bool isPaused = true;

        public Action<bool> OnGamePausedEvent;

        public bool IsPaused
        {
            get => isPaused;
            set => isPaused = value;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isPaused = !isPaused;
                OnGamePausedEvent?.Invoke(isPaused);
            }
        }
    }
}