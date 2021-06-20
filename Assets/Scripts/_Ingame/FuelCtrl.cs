using UnityEngine;
using System.Collections;

public class FuelCtrl : MonoBehaviour 
{
	private float fuelAdjust;

	// Use this for initialization
	void Start ()
	{
		fuelAdjust = 0.5f;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!Ingame.RESPAWN && !Ingame.PAUSE)
		{
			float posX = transform.position.x; 
			float posY = transform.position.y;
			float posZ = transform.position.z;
			posZ -= (CreateStage.SPEED * Time.timeScale);
			transform.position = new Vector3(posX, 0, posZ);
				
			if(transform.position.z < -5)
				Destroy(transform.gameObject);
		}
	}
	
	public void OnTriggerStay(Collider obj)
	{
		if(obj.transform.tag == "wall")
		{
			transform.position = new Vector3(transform.position.x + fuelAdjust,0, transform.position.z);
		}
	}
	
	public void OnTriggerEnter(Collider obj)
	{
		if(obj.transform.tag == "tiro")
		{
			GameObject explosion = (GameObject)Resources.Load("Prefabs/Explosion/Fuel");
			Instantiate(explosion, transform.position, Quaternion.identity);
			explosion.tag = "explosion";
			explosion.name = "explosion";
		}
	}
}
