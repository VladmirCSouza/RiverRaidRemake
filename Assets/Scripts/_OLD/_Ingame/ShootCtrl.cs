using UnityEngine;
using System.Collections;

public class ShootCtrl : MonoBehaviour 
{
	private float shootSpeed;
	private float destroyDist;
	private float timer;
	
	private void Start()
	{
		shootSpeed = 0.5f;
		destroyDist = 6.0f;
		timer = Time.time;
	}
	
	private void Update () 
	{
		if(!Ingame.PAUSE)
		{
			float posX = transform.position.x;
			float posZ = transform.position.z;
			posZ += (shootSpeed * Time.timeScale);
			transform.position = new Vector3(posX, 0, posZ);
			
			if(transform.position.z > destroyDist)
				destroyShoot();
		}
	}
	
	private void destroyShoot()
	{
		GameObject explosion = (GameObject)Resources.Load("Prefabs/Explosion/ShootOnWater");
		MonoBehaviour.Instantiate(explosion, transform.position, Quaternion.identity);
		explosion.tag = "explosion";
		explosion.name = "ShootOnWater";
		Destroy(transform.gameObject);
	}
	
	public void OnTriggerEnter(Collider obj)
	{
		if(obj.transform.tag == "wall")
		{
			Destroy(transform.gameObject);
		}
		
		if(obj.transform.name == "Boat(Clone)")
		{
			Destroy(obj.transform.gameObject);
			Destroy(transform.gameObject);
			Ingame._score.setScore(100);
		}
		if(obj.transform.name == "Heli(Clone)")
		{
			Destroy(obj.transform.gameObject);
			Destroy(transform.gameObject);
			Ingame._score.setScore(150);
		}
		
		if(obj.transform.tag == "fuel")
		{
			Ingame.FILL = false;
			Destroy(obj.transform.gameObject);
			Destroy(transform.gameObject);
			Ingame._score.setScore(250);
		}
		
		if(obj.transform.tag == "bridge")
		{
			GameObject explosion = (GameObject)Resources.Load("Prefabs/Explosion/Bridge");
			Instantiate(explosion, transform.position, Quaternion.identity);
			explosion.tag = "explosion";
			explosion.name = "explosion";
			Destroy(obj.transform.gameObject);
			Destroy(transform.gameObject);
			Ingame._score.setScore(500);
		}
	}
}
