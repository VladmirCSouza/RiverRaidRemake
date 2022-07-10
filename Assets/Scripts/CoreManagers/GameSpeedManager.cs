using System;
using Channel3.Utils;
using UnityEngine;

namespace Channel3.CoreManagers
{
    public class GameSpeedManager : Singleton<GameSpeedManager>
    {
        [Space]
        [SerializeField] private float defaultSpeed = 40f;
        [Range(0.1f, 1f)][SerializeField] private float maxVertSpeedMult = 0.3f;
        [Range(0.1f, 1f)][SerializeField] private float minVertSpeedMult = 0.25f;
        public float DefaultSpeed => defaultSpeed;
        private float currSpeed;
        public float CurrentSpeed => currSpeed;
        
        private void Start()
        {
            currSpeed = defaultSpeed;
        }

        private void Update()
        {
            if(GameManager.Instance.IsPaused)
            {
                currSpeed = 0;
                return;
            }
            
            UpdateVerticalSpeed();
        }
        
        private void UpdateVerticalSpeed()
        {
            float v = Input.GetAxisRaw("Vertical");
            
            if(v > 0)
                SpeedUpBLocks();
            else if(v < 0)
                SpeedDownBlocks();
            else
                SetDefaultBlockSpeed();
        }
        
        public void SpeedUpBLocks()
        {
            currSpeed = (1f + maxVertSpeedMult) * defaultSpeed;
        }

        public void SpeedDownBlocks()
        {
            currSpeed = (1f - minVertSpeedMult) * defaultSpeed;
        }

        public void SetDefaultBlockSpeed()
        {
            currSpeed = defaultSpeed;
        }
    }
}