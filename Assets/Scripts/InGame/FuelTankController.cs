using System;
using UnityEngine;

public class FuelTankController : MonoBehaviour
{
    private const string ANIMATION_TRIGGER_NAME = "Explode";

    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider collider;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("tiro"))
        {
            animator.SetTrigger(ANIMATION_TRIGGER_NAME);
            collider.enabled = false;
        }
        
        Destroy(gameObject, 2f);
    }
}
