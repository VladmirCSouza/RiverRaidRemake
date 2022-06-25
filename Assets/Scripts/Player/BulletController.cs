using System;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float maxLifeDistance = 100f;
        [SerializeField] private Transform waterExplosion;
        [SerializeField] private Transform wallExplosion;
        [SerializeField] private Transform enemyExplosion;
        
        private Rigidbody rb;
        private float startPosition;
        
        private enum ExplosionReason
        {
            water,
            wall,
            enemy
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            startPosition = transform.position.z;
            
            Destroy(gameObject, 10);
        }

        private void Update()
        {
            if (!rb.useGravity && transform.position.z >= startPosition + maxLifeDistance)
                rb.useGravity = true;
            else if (transform.position.y <= 0)
                DestroyBullet(ExplosionReason.water);

        }
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.tag);
            switch (collision.gameObject.tag)
            {
                case "wall":
                    DestroyBullet(ExplosionReason.wall);
                    break;
                case "enemy":
                    DestroyBullet(ExplosionReason.enemy);
                    break;
                case "bridge":
                    DestroyBullet(ExplosionReason.enemy);
                    break;
            }
        }

        private void DestroyBullet(ExplosionReason reason)
        {
            Transform explosion = null;
            
            switch (reason)
            {
                case ExplosionReason.wall:
                    explosion = wallExplosion;
                    break;
                case ExplosionReason.enemy:
                    explosion = enemyExplosion;
                    break;
                case ExplosionReason.water:
                default:
                    explosion = waterExplosion;
                    break;
            }
            
            explosion.parent = null;
            explosion.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}