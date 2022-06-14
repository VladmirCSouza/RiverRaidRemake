using UnityEngine;
using System.Collections;

public class CreateStage
{
	private GameObject empty;
	private GameObject _cBlock; // checkpoint blocked
	private GameObject _cOpen; //checkpoint open
	
	private GameObject _mLight;
	
	private string _tag = "inGameItem";
	
	private int elementsLimit = 4;
	private int screenElement = 0;
	private int checkpoint;
	
	public static float SPEED = 0.05f;
	
	public CreateStage()
	{
		init();
	}
	
	public void init()
	{
		checkpoint = 0;
		insertLights();
		loadIngameBase();
		
		new IngameAssets(true, false, 0.0f);
		screenElement ++;
		
		for(int i = 1; i < elementsLimit; i++)
		{
			new IngameAssets(false, false, (i * 10.0f));
		}
	}
	
	public void update()
	{
		stageMovement();
	}
	
	private void loadIngameBase()
	{
		GameObject asset = (GameObject)Resources.Load("Prefabs/sceneParts/common/base");
		MonoBehaviour.Instantiate(asset, new Vector3(0,0,0),Quaternion.identity);
		asset.tag = "base";
	}
	
	private void insertLights()
	{
		//TODO ADD THE CORRECT LIGHT SETUP TO THE GAME
		/*
		_mLight = new GameObject("Light");
		_mLight.tag = "light";
		_mLight.AddComponent<Light>();
		_mLight.light.type = LightType.Directional;
		_mLight.light.color = Color.white;
		_mLight.light.intensity = 0.2f;
		_mLight.transform.eulerAngles = new Vector3(45.0f, 0, 0);
		
		_mLight = new GameObject("Light");
		_mLight.tag = "light";
		_mLight.AddComponent<Light>();
		_mLight.light.type = LightType.Directional;
		_mLight.light.color = Color.white;
		_mLight.light.intensity = 0.2f;
		_mLight.transform.eulerAngles = new Vector3(45.0f, 45.0f, 0);
		
		_mLight = new GameObject("Light");
		_mLight.tag = "light";
		_mLight.AddComponent<Light>();
		_mLight.light.type = LightType.Directional;
		_mLight.light.color = Color.white;
		_mLight.light.intensity = 0.2f;
		_mLight.transform.eulerAngles = new Vector3(45.0f, -45.0f, 0);
		*/
	}
	
	private void stageMovement()
	{
		//Time.timeScale = 0.5f;
		GameObject [] obj;
	    obj = GameObject.FindGameObjectsWithTag(_tag);
		
		for(int i = 0; i < obj.Length; i++)
		{
			GameObject asset = obj[i];
			if(obj[i].transform.position.z <= -10.0f)
			{
				GameObject.Destroy(obj[i]);
				if(checkpoint > 10)
				{
					new IngameAssets(false, true, (obj[(obj.Length -1)].transform.position.z + 10.0f));
					checkpoint = 0;
				}
				else
				{
					new IngameAssets(false, false, (obj[(obj.Length -1)].transform.position.z + 10.0f));
					checkpoint  ++;
				}
					
			}
			else
			{
				float posZ = obj[i].transform.position.z;
				posZ -= (SPEED * Time.timeScale);
				obj[i].transform.position = new Vector3(0,0, posZ);
			}
		}
	}
}
