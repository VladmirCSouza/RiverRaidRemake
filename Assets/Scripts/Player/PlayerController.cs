using System;
using Channel3.RetroRaid.LevelBlock;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Space, SerializeField] private float hSpeed = 10f;
        [Space, SerializeField] private GameObject explosion;
        [SerializeField] private Transform gunPoint;
        [SerializeField] private Rigidbody bulletPrefab;
        [Range(0.1f, 1f)][SerializeField] private float bulletSpeedPerc = 0.6f;
        
        private Rigidbody rb;
        private float currentSpeed;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Shoot();
            
            UpdateHorizontalSpeed();
            UpdateVerticalSpeed();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void UpdateHorizontalSpeed()
        {
            currentSpeed = hSpeed * Input.GetAxis("Horizontal");
        }

        private void UpdateVerticalSpeed()
        {
            float v = Input.GetAxisRaw("Vertical");
            
            if(v > 0)
                LevelBlockManager.Instance.SpeedUpBLocks();
            else if(v < 0)
                LevelBlockManager.Instance.SpeedDownBlocks();
            else
                LevelBlockManager.Instance.SetDefaultBlockSpeed();
        }

        private void Move()
        {
            rb.velocity = new Vector3(currentSpeed, 0, 0);
        }

        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
            float bulletSpeed = (1f + bulletSpeedPerc) * LevelBlockManager.Instance.DefaultBlockSpeed;
            bullet.velocity = Vector3.forward * bulletSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("bridge"))
            {
                explosion.transform.SetParent(null);
                explosion.SetActive(true);
                LevelBlockManager.Instance.StopMoving();
                gameObject.SetActive(false);
            }
        }
    }
}