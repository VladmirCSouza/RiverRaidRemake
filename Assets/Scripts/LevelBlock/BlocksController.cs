using System;
using System.Collections.Generic;
using Channel3.CoreManagers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Channel3.RetroRaid.LevelBlock
{
    public class BlocksController : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnItemPoint;

        private Rigidbody rb;

        public void MoveBlock()
        {
            if(rb == null)
                rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.back * GameSpeedManager.Instance.CurrentSpeed;
        }

        public Vector3 GetSpawnPoint()
        {
            if (spawnItemPoint.Length < 1)
            {
                Debug.LogError($"NO SPAWN POINTS SET on {gameObject.name.ToUpper()} prefab");
                return Vector3.zero;
            }

            if (!HasAnyPointAvailable())
                return Vector3.zero;
            
            do
            {
                int index = Random.Range(0, spawnItemPoint.Length - 1);

                if (spawnItemPoint[index].gameObject.activeSelf)
                {
                    spawnItemPoint[index].gameObject.SetActive(false);
                    return spawnItemPoint[index].position;
                }

            } while (true);
        }

        public Vector3[] GetAllAvailableSpawnPoints()
        {
            List<Vector3> points = new List<Vector3>(spawnItemPoint.Length);
            for (int i = 0; i < spawnItemPoint.Length; i++)
            {
                if (spawnItemPoint[i].gameObject.activeSelf)
                    points.Add(spawnItemPoint[i].position);
            }

            return points.ToArray();
        }
        
        private bool HasAnyPointAvailable()
        {
            for (int i = 0; i < spawnItemPoint.Length; i++)
            {
                if (spawnItemPoint[i].gameObject.activeSelf)
                    return true;
            }
            return false;
        }
        
    }
}