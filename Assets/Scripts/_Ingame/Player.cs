using UnityEngine;
using System.Collections;

public class Player
{
	private GameObject player;
	private GameObject tiro;
	
	public bool getHit;
	public string hitTag;
	
	private float position = 0.0f;
	private float speed = 0.025f;
	private float fuelAcc = 0.00015f;
	private float fuelBrk = 0.00005f;
	private float fuelNrm = 0.00005f;
	private float shootDelay = 0.2f;
	private float shootTimer;
	public  float fuelCurr;
	
	
	public Player()
	{
		getPlayer();
	}
	
	public void update()
	{
		getPlayer();
		movement();
		hit();
		shoot();
		fuelCtrl();
	}
	
	private void fuelCtrl()
	{
		if(HUD.FUEL <= 0.44f)
		{
			GameObject.FindWithTag("Player").GetComponent<HitDetection>().GET_HIT = true;
			GameObject.FindWithTag("Player").GetComponent<HitDetection>().HIT_TAG = "You are out of gas!";
		}
	}
	
	private void shoot()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(Time.time > (shootTimer + shootDelay))
			{
				shootTimer = Time.time;
				tiro = (GameObject)Resources.Load("Prefabs/Player/Tiro");
				MonoBehaviour.Instantiate(tiro, player.transform.position, Quaternion.identity);
				tiro.tag = "tiro";
			}
		}
	}
	
	private void movement()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			CreateStage.SPEED = 0.08f;
			fuelCurr = fuelAcc;
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			CreateStage.SPEED = 0.03f;
			fuelCurr = fuelBrk;
		}
		else
		{
			CreateStage.SPEED = 0.05f;
			fuelCurr = fuelNrm;
		}
		
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			position -= (speed * Time.timeScale);
			GameObject.FindWithTag("plane").animation.Play("LEFT");
		}
		
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			position += (speed * Time.timeScale);
			GameObject.FindWithTag("plane").animation.Play("RIGHT");
		}
		else
		{
			GameObject.FindWithTag("plane").animation.Play("STOP");
		}
		
		player.transform.position = new Vector3 (position, 0.0f, 0.0f);
	}
	
	
    public void hit()
	{
		getHit = player.GetComponent<HitDetection>().GET_HIT;
		hitTag = player.GetComponent<HitDetection>().HIT_TAG;
    }
	
	public GameObject getPlayer()
	{
		if(player == null)
		{
			player = (GameObject)Resources.Load("Prefabs/Player/Player");
			MonoBehaviour.Instantiate(player, new Vector3(position,0.0f,0.0f),Quaternion.identity);
			player.tag = "Player";
			GameObject.FindWithTag("Player").GetComponent<HitDetection>().GET_HIT = false;
		}
		else
		{
			player = GameObject.FindWithTag("Player");
		}
		
		return player;
	}
	
	public void explosion()
	{
		GameObject explosion = (GameObject)Resources.Load("Prefabs/Explosion/Player");
		MonoBehaviour.Instantiate(explosion, player.transform.position, Quaternion.identity);
		explosion.tag = "explosion";
		explosion.name = "explosion";
	}
}
