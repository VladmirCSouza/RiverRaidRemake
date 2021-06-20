using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour 
{
	public bool GET_HIT;
	public string HIT_TAG;
	
	public void OnTriggerEnter(Collider obj)
	{
		//Debug.Log(obj.transform.tag);
		if(obj.transform.tag == "wall")
		{
			GET_HIT = true;
			HIT_TAG = "YOU HIT the wall";
		}
		
		if(obj.transform.name == "Boat(Clone)")
		{
			Destroy(obj.transform.gameObject);
			GET_HIT = true;
			HIT_TAG = "YOU HIT a boat";
		}
		if(obj.transform.name == "Heli(Clone)")
		{
			Destroy(obj.transform.gameObject);
			GET_HIT = true;
			HIT_TAG = "YOU HIT a helicopter";
		}
		if(obj.transform.tag == "bridge")
		{
			GameObject explosion = (GameObject)Resources.Load("Prefabs/Explosion/Bridge");
			Instantiate(explosion, transform.position, Quaternion.identity);
			explosion.tag = "explosion";
			explosion.name = "explosion";
			Destroy(obj.transform.gameObject);
			GET_HIT = true;
			HIT_TAG = "YOU HIT the bridge";
		}
	}
	
	public void  OnTriggerStay(Collider obj)
	{
		if(obj.transform.tag == "fuel")
		{
			Ingame.FILL = true;
		}
	}
	
	public void  OnTriggerExit(Collider obj)
	{
		if(obj.transform.tag == "fuel")
		{
			Ingame.FILL = false;
		}
	}
}
