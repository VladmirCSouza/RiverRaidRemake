using System;
using System.Collections.Generic;
using Channel3.RetroRaid.LevelBlock;
using Channel3.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Channel3.CoreManagers
{
    public class LevelBlockManager : Singleton<LevelBlockManager>
    {
        private const int BLOCK_SIZE_LIMIT = 100;
        
        [SerializeField] private BlocksController startBlock;
        [SerializeField] private BlocksController checkpointBlock;
        
        [Space]
        [SerializeField] private BlocksController[] easyBlocks;
        [SerializeField] private BlocksController[] mediumBlocks;
        [SerializeField] private BlocksController[] hardBlocks;

        [Space]
        [SerializeField] private GameObject fuelTankPrefab;
        
        [Space]
        [SerializeField] private int blockInstancesLimit = 6;
        
        private List<BlocksController> currentBlocks = new(10);

        private int blockSpawnCount = 0;
        
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
            blockSpawnCount++;

            for (int i = 1; i <= blockInstancesLimit; i++)
            {
                InstantiateBlock(i * BLOCK_SIZE_LIMIT);
            }
        }

        private void FixedUpdate()
        {
            MoveBlocks();
            if(currentBlocks[0].transform.position.z < -BLOCK_SIZE_LIMIT)
                RemoveFirstAddLast();
        }

        private void MoveBlocks()
        {
            for (int i = 0; i < currentBlocks.Count; i++)
                currentBlocks[i].MoveBlock();
        }

        private void RemoveFirstAddLast()
        {
            Destroy(currentBlocks[0].gameObject);
            currentBlocks.RemoveAt(0);
            InstantiateBlock(blockInstancesLimit * BLOCK_SIZE_LIMIT);
        }

        private void InstantiateBlock(int position)
        {
            BlocksController newBlock = Instantiate(easyBlocks[Random.Range(0, easyBlocks.Length)], Vector3.forward * position, Quaternion.identity);
            currentBlocks.Add(newBlock);
            
            blockSpawnCount++;
            if (blockSpawnCount > 10)
                blockSpawnCount = 1;
            
            InstantiateFuel(newBlock);
        }

        private void InstantiateFuel(BlocksController newBlock)
        {
            switch (GameManager.Instance.CurrentDifficulty)
            {
                case GameManager.Difficulty.MEDIUM:
                    break;
                case GameManager.Difficulty.HARD:
                    break;
                case GameManager.Difficulty.EASY:
                default:
                    if (blockSpawnCount % 3 == 0)
                        Instantiate(fuelTankPrefab, newBlock.GetSpawnPoint(), Quaternion.identity, newBlock.transform);
                    break;
            }
        }
    }
}