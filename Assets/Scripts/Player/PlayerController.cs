using Channel3.CoreManagers;
using UnityEngine;

namespace Channel3.RetroRaid.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float maxFuel;
        [SerializeField] private float addFuel = 10;
        [SerializeField] private float normalFuelUse = 1;
        [SerializeField] private float maxSpeedFuelUsePerc = 1.25f;
        [SerializeField] private float lowSpeedFuelUsePerc = 0.8f;
        
        [SerializeField] private float hSpeed = 10f;
        [Space]
        [SerializeField] private GameObject explosion;
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
            if(GameManager.Instance.IsPaused)
                return;
            
            if(Input.GetKeyDown(KeyCode.Space))
                Shoot();
            
            UpdateHorizontalSpeed();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void UpdateHorizontalSpeed()
        {
            currentSpeed = hSpeed * Input.GetAxis("Horizontal");
        }

        private void Move()
        {
            rb.velocity = new Vector3(currentSpeed, 0, 0);
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
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("wall") || collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("bridge"))
            {
                explosion.transform.SetParent(null);
                explosion.SetActive(true);
                gameObject.SetActive(false);
                GameManager.Instance.IsPaused = true;
            }
        }
    }
}