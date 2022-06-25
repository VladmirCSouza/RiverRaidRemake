using System;
using Channel3.RetroRaid.LevelBlock;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float fuelLeft = 1000;
        [Space][SerializeField] private float hSpeed = 10f;
        [SerializeField] private GameObject explosion;
        
        private Rigidbody rb;
        private float currentSpeed;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
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