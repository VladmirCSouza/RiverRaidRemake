using UnityEngine;
using System.Collections;

public class ChangeView : MonoBehaviour
{
	private bool classicView;
	private GameObject player;
	
	private float playerPos;
	
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	public float distance = 5.0F;
	private float yVelocity = 0.0F;
	
	// Use this for initialization
	void Start ()
	{
		classicView = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			if(classicView)
				classicView = false;
			else
				classicView = true;
		}
		changeView();
	}
	
	public void changeView()
	{
		Vector3 camPos;
		float camAngle;
		
		if(classicView)
		{
			Time.timeScale = 0.8f;
			camAngle = 90.0f;
			camPos = new Vector3(0, 7, 2);
			smoothTime = 0.2F;
		}
		else
		{
			Time.timeScale = 1.0f;
			if(player)
			{
				playerPos = player.transform.position.x;
				//camPos = new Vector3(player.transform.position.x, 2, -2);
			}
			else
			{
				//camPos = new Vector3(0, 2, -2);
				player = GameObject.FindWithTag("Player");
			}
			camPos = new Vector3(playerPos, 2, -2);
			camAngle = 35.0f;
			smoothTime = 0.2F;
		}
		updateView(camAngle, camPos);
	}
	
	//update da posicao da camera
	private void updateView(float angle, Vector3 position)
	{
		transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, angle, ref yVelocity, smoothTime);
		transform.eulerAngles = new Vector3(xAngle, 0, 0);
		
		//transform.eulerAngles = new Vector3(angle, 0, 0);
		//transform.position = position;
	}
}
