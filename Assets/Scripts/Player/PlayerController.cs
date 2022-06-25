using System;
using Channel3.RetroRaid.LevelBlock;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float fuelLeft = 1000;
        
        [Space]
        [SerializeField] private float hSpeed = 10f;

        [SerializeField]
        private GameObject explosion;
        
        private Rigidbody rb;
        private float currentSpeed;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            currentSpeed = hSpeed * h;
        }

        private void FixedUpdate()
        {
            Move(currentSpeed);
        }

        private void Move(float speed)
        {
            rb.velocity = new Vector3(speed, 0, 0);
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