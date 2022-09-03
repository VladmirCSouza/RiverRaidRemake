using System;
using System.ComponentModel;
using Channel3.CoreManagers;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float maxLifeDistance = 70f;
        [SerializeField] private Transform waterExplosion;
        [SerializeField] private Transform wallExplosion;
        [SerializeField] private Transform enemyExplosion;
        [SerializeField] private Transform fuelTankExplosion;
        
        private Rigidbody rb;
        private float startPosition;

        private Vector3 cachedVelocity;
        
        private enum ExplosionReason
        {
            water,
            wall,
            enemy,
            fuelTank
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            startPosition = transform.position.z;
            cachedVelocity = rb.velocity;
            GameManager.Instance.OnGamePausedEvent += OnGamePaused;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGamePausedEvent -= OnGamePaused;
        }

        private void OnGamePaused(bool isPaused)
        {
            if (isPaused)
            {
                cachedVelocity = rb.velocity;    
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            }
            else
            {
                rb.velocity = cachedVelocity;
                rb.useGravity = true;
            }
        }

        private void Update()
        {
            if (GameManager.Instance.IsPaused)
                return;
            
            if (!rb.useGravity && transform.position.z >= startPosition + maxLifeDistance)
                rb.useGravity = true;
            else if (transform.position.y <= 0)
                DestroyBullet(ExplosionReason.water);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
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

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("fuel"))
                DestroyBullet(ExplosionReason.fuelTank);
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
                case ExplosionReason.fuelTank:
                    explosion = fuelTankExplosion;
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