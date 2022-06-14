using UnityEngine;
using System.Collections;

namespace Channel3.RetroRaid.CameraControl
{
	public class ChangeView : MonoBehaviour
	{
		[SerializeField] private GameObject player;
		
		[SerializeField] private float classicViewAngle = 90f;
		[SerializeField] private float classicViewDistance = 35f;
		[SerializeField] private float classicViewHeight = 100f;
		
		[Space, SerializeField] private float newViewAngle = 35f;
		[SerializeField] private float newViewDistance = 40f;
		[SerializeField] private float newViewHeight = 50f;
		
		[Space, SerializeField] private bool classicView = true;
		
		private float smoothTime = 0.2F;
		
		private float yVelocity = 0.0F;
		private Vector3 velocity = Vector3.zero;
	
		void Update ()
		{
			if(Input.GetKeyDown(KeyCode.C))
				classicView = !classicView;
			
			ChangeCameraView();
		}

		private void ChangeCameraView()
		{
			Vector3 camPos;
			float camAngle;
		
			if(classicView)
			{
				Time.timeScale = 0.8f;
				camAngle = classicViewAngle;
				camPos = new Vector3(0, classicViewHeight, classicViewDistance);
			}
			else
			{
				Time.timeScale = 1.0f;
				camAngle = newViewAngle;
				camPos = new Vector3(player.transform.position.x, newViewHeight, -newViewDistance);
			}
			
			UpdateCameraView(camAngle, camPos);
		}
	
		private void UpdateCameraView(float angle, Vector3 position)
		{
			transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
			float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, angle, ref yVelocity, smoothTime);
			transform.eulerAngles = new Vector3(xAngle, 0, 0);
		}
	}
}