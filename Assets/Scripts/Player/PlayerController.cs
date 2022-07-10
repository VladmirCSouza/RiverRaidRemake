using System;
using System.Collections;
using Channel3.CoreManagers;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxFuel = 1000;
        [SerializeField] private float addFuel = 10;
        [SerializeField] private float defaultFuelUse = 1;
        [SerializeField] private float maxSpeedFuelUsePerc = 1.25f;
        [SerializeField] private float lowSpeedFuelUsePerc = 0.8f;
        
        [Space]
        [SerializeField] private float hSpeed = 10f;
        
        [Space]
        [SerializeField] private GameObject explosion;
        [SerializeField] private Transform gunPoint;
        [SerializeField] private Rigidbody bulletPrefab;
        [Range(0.1f, 1f)][SerializeField] private float bulletSpeedPerc = 0.6f;
        
        private Rigidbody rb;
        
        private float currentSpeed;
        private float cachedSpeed;
        private float currentFuel;
        
        private bool isAddingFuel = false;
        private bool isOutOfFuel = false;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            currentFuel = maxFuel;
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
                cachedSpeed = currentSpeed;    
                currentSpeed = 0f;
            }
            else
            {
                currentSpeed = cachedSpeed;
            }
        }

        private void Update()
        {
            if(GameManager.Instance.IsPaused)
                return;
            
            if(isOutOfFuel)
                return;;
            
            if(Input.GetKeyDown(KeyCode.Space))
                Shoot();
            
            UpdateHorizontalSpeed();
        }

        private void FixedUpdate()
        {
            if(GameManager.Instance.IsPaused)
                return;

            if (isOutOfFuel)
            {
                if(transform.position.y < 0)
                    ExplodePlayer();
                return;
            }
            
            FuelControl();
            Move();
        }

        private void UpdateHorizontalSpeed()
        {
            currentSpeed = hSpeed * Input.GetAxis("Horizontal");
        }

        private void Move()
        {
            rb.velocity = new Vector3(currentSpeed, rb.velocity.y, 0);
        }

        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
            float bulletSpeed = (1f + bulletSpeedPerc) * GameSpeedManager.Instance.CurrentSpeed;
            bullet.velocity = Vector3.forward * bulletSpeed;
            Debug.Log($"SHOOT {bullet.velocity}");
        }

        private void FuelControl()
        {
            if (isAddingFuel)
                FuelAdding();
            else 
                FuelConsumption();

            if (currentFuel <= 0)
            {
                rb.useGravity = true;
                isOutOfFuel = true;
            }
        }

        private void FuelAdding()
        {
            if(currentFuel < maxFuel)
                currentFuel += addFuel;
        }

        private void FuelConsumption()
        {
            float fuelSub = 0;
            switch (GameSpeedManager.Instance.Speed)
            {
                case WorldSpeed.Slow:
                    fuelSub = lowSpeedFuelUsePerc;
                    break;
                case WorldSpeed.Fast:
                    fuelSub = maxSpeedFuelUsePerc;
                    break;
                case WorldSpeed.Default:
                default:
                    fuelSub = defaultFuelUse;
                    break;
            }

            currentFuel -= fuelSub;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("bridge"))
            {
                ExplodePlayer();
            }
        }

        private void ExplodePlayer()
        {
            explosion.transform.SetParent(null);
            explosion.SetActive(true);
            gameObject.SetActive(false);
            GameManager.Instance.IsPaused = true;
        }
    }
}