using UnityEngine;
using System.Collections;

public class GenerateParticle : MonoBehaviour
{
	public GameObject particle;

	// Use this for initialization
	void Start () 
	{
		createParticle();
		Destroy(transform.gameObject, 2.0f);
	}
	
	private void createParticle()
	{
		Instantiate(particle, transform.position, transform.rotation);
	}
}
