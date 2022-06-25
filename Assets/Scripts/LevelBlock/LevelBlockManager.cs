using System;
using System.Collections.Generic;
using JAD.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Channel3.RetroRaid.LevelBlock
{
    public class LevelBlockManager : Singleton<LevelBlockManager>
    {
        private const int BLOCK_SIZE_LIMIT = 100;
        
        [SerializeField] private Rigidbody startBlock;
        [SerializeField] private Rigidbody checkpointBlock;
        
        [Space]
        [SerializeField] private Rigidbody[] easyBlocks;
        [SerializeField] private Rigidbody[] mediumBlocks;
        [SerializeField] private Rigidbody[] hardBlocks;
        
        [Space]
        [SerializeField] private int blockInstanceLimit = 5;
        
        [Space]
        [SerializeField] private float defaultBlkMoveSpeed = 40f;
        [Range(0.1f, 1f)][SerializeField] private float maxVertSpeedMult = 0.3f;
        [Range(0.1f, 1f)][SerializeField] private float minVertSpeedMult = 0.25f;
        
        private List<Rigidbody> currentBlocks = new(10);
        private float currentBlkMovingSpeed;
        private bool isMoving = false;

        void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            startBlock.transform.position = Vector3.zero;
            startBlock.gameObject.SetActive(true);
            checkpointBlock.gameObject.SetActive(false);
            currentBlkMovingSpeed = defaultBlkMoveSpeed;
            
            currentBlocks.Add(startBlock);

            for (int i = 1; i <= blockInstanceLimit; i++)
            {
                AddBlock(i * BLOCK_SIZE_LIMIT);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                isMoving = !isMoving;
        }

        private void FixedUpdate()
        {
            if (!isMoving)
                currentBlkMovingSpeed = 0;
            
            MoveBlocks();
            if(currentBlocks[0].transform.position.z < -BLOCK_SIZE_LIMIT)
                RemoveFirstAddLast();
        }

        private void MoveBlocks()
        {
            for (int i = 0; i < currentBlocks.Count; i++)
                currentBlocks[i].velocity = Vector3.back * currentBlkMovingSpeed;
        }

        private void RemoveFirstAddLast()
        {
            Destroy(currentBlocks[0].gameObject);
            currentBlocks.RemoveAt(0);
            AddBlock(blockInstanceLimit * BLOCK_SIZE_LIMIT);
        }

        private void AddBlock(int position)
        {
            Rigidbody newBlock = Instantiate(easyBlocks[Random.Range(0, easyBlocks.Length)], Vector3.forward * position, Quaternion.identity);
            currentBlocks.Add(newBlock);
        }
        
        public void SpeedUpBLocks()
        {
            if(!isMoving)
                return;

            currentBlkMovingSpeed = (1f + maxVertSpeedMult) * defaultBlkMoveSpeed;
        }

        public void SpeedDownBlocks()
        {
            if(!isMoving)
                return;
            
            currentBlkMovingSpeed = (1f - minVertSpeedMult) * defaultBlkMoveSpeed;
        }

        public void SetDefaultBlockSpeed()
        {
            if (!isMoving)
                currentBlkMovingSpeed = 0;
            else
                currentBlkMovingSpeed = defaultBlkMoveSpeed;
        }

        public void StopMoving()
        {
            isMoving = false;
        }
    }
}