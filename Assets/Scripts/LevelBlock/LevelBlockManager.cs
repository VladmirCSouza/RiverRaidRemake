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
        [SerializeField] private float blockMoveSpeed = 40f;
        [SerializeField] private float maxVertSpeedMult = 0.3f;
        [SerializeField] private float minVertSpeedMult = 0.25f;
        
        private List<Rigidbody> currentBlocks = new List<Rigidbody>(10);

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
            {
                MoveBlocks(0);
                return;
            }
            
            MoveBlocks(blockMoveSpeed);
            if(currentBlocks[0].transform.position.z < -BLOCK_SIZE_LIMIT)
                RemoveFirstAddLast();
        }

        private void MoveBlocks(float speed)
        {
            for (int i = 0; i < currentBlocks.Count; i++)
            {
                currentBlocks[i].velocity = Vector3.back * speed;
            }
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

        public void StopMoving()
        {
            isMoving = false;
        }
    }
}