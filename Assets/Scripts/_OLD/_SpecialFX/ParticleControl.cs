using UnityEngine;
using System.Collections;

public class ParticleControl : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//TODO Add the get component to use the rigidibody
		//transform.rigidbody.AddForce(Vector3.up * 0.2f);	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!Ingame.RESPAWN)
		{		
			float posX = transform.position.x; 
			float posY = transform.position.y;
			float posZ = transform.position.z;
			posZ -= (CreateStage.SPEED * Time.timeScale);
			transform.position = new Vector3(posX, posY, posZ);
			
			if(transform.position.y < -10)
				Destroy(transform.gameObject);
		}
	}
}
