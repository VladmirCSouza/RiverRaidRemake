using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour
{
	private float enemySpeed;

	// Use this for initialization
	void Start ()
	{
		enemySpeed = 0.02f;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!Ingame.RESPAWN && !Ingame.PAUSE)
		{
			float posX = transform.position.x; 
			float posY = transform.position.y;
			float posZ = transform.position.z;
			posX += (enemySpeed  * Time.timeScale);
			posZ -= (CreateStage.SPEED * Time.timeScale);
			transform.position = new Vector3(posX, 0, posZ);
			
			if(transform.position.z < -5)
				Destroy(transform.gameObject);
		}
	}
	
	public void OnTriggerEnter(Collider obj)
	{
		if(obj.transform.tag == "wall")
		{
			enemySpeed *= -1;
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
		}
		if(obj.transform.tag == "Player")
		{
			GameObject explosion;
			if(transform.name == "Boat(Clone)")
				explosion = (GameObject)Resources.Load("Prefabs/Explosion/Boat");
			else
				explosion = (GameObject)Resources.Load("Prefabs/Explosion/Heli");
			
			Instantiate(explosion, transform.position, Quaternion.identity);
			explosion.tag = "explosion";
			explosion.name = "explosion";
		}
		if(obj.transform.tag == "tiro")
		{
			GameObject explosion;
			if(transform.name == "Boat(Clone)")
				explosion = (GameObject)Resources.Load("Prefabs/Explosion/Boat");
			else
				explosion = (GameObject)Resources.Load("Prefabs/Explosion/Heli");
			
			Instantiate(explosion, transform.position, Quaternion.identity);
			explosion.tag = "explosion";
			explosion.name = "explosion";
		}
	}
}
