using UnityEngine;
using System.Collections;

[System.Serializable]
public class IngameAssets
{
	private GameObject asset;
	private string _tag = "inGameItem";
	private float moveSpeed = 0.1f;
	private static int fuelCounter = 0;
	
	//elementos carregados por dificuldade
	private int range;
	
	public IngameAssets(bool isStart, bool isCheckPoint, float position)
	{
		if(isStart)
		{
			asset = (GameObject)Resources.Load("Prefabs/sceneParts/common/start");
			MonoBehaviour.Instantiate(asset, new Vector3(0,0,position),Quaternion.identity);
			asset.tag = _tag;
		}
		else if(isCheckPoint)
		{
			asset = (GameObject)Resources.Load("Prefabs/sceneParts/common/checkpointClosed");
			MonoBehaviour.Instantiate(asset, new Vector3(0,0,position),Quaternion.identity);
			asset.tag = _tag;
		}
		else
		{
			asset = loadAssets();
			MonoBehaviour.Instantiate(asset, new Vector3(0,0,position),Quaternion.identity);
			asset.tag = _tag;
			loadEnemy(position);
			loadFuel(position);
		}
	}
	
	private GameObject loadAssets()
	{
		updateRange();
		int gRange = Random.Range(0, range);
		
		switch (gRange)
		{
			//Easy Assets
			case 1:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_01/easy01");
			break;
			
			case 2:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_01/easy02");
			break;
			
			case 3:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_01/easy03");
			break;
			
			case 4:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_01/easy04");
			break;
			
			//Medium Assets
			case 5:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_02/med01");
			break;
			
			case 6:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_02/med02");
			break;
			
			case 7:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_02/med03");
			break;
			
			case 8:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_02/med04");
			break;
			
			case 9:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_02/med05");
			break;
			
			//Hard Assets
			
			case 10:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_03/hard01");
			break;
			
			case 11:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_03/hard02");
			break;
			
			case 12:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_03/hard03");
			break;
			
			case 13:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_03/hard04");
			break;
			
			case 14:
				return (GameObject)Resources.Load("Prefabs/sceneParts/level_03/hard05");
			break;
			
			default:
				return (GameObject)Resources.Load("Prefabs/sceneParts/common/empty");
			break;
		}
	}
	
	private void updateRange()
	{
		switch(Ingame.DIFICULTY)
		{
			case 1:
				range = 10;	//MEDIUM
			break;
			
			case 2:
				range = 15; //HARD
			break;
			
			default:
				range = 5;//EASY
			break;
		}
	}
	
	private void loadEnemy(float position)
	{
		GameObject enemy;
		
		position += Random.Range(-5, -1);
		enemy = selectEnemy();
		MonoBehaviour.Instantiate(enemy, new Vector3(Random.Range(-3, 3),0,position),Quaternion.identity);
		enemy.tag = "enemy";
		
		position += Random.Range(1, 5);
		enemy = selectEnemy();
		MonoBehaviour.Instantiate(enemy, new Vector3(Random.Range(-3, 3),0,position),Quaternion.identity);
		enemy.tag = "enemy";
	}
	
	private GameObject selectEnemy()
	{
		int enemy = Random.Range(0, 2);
		
		switch(enemy)
		{
			case 1:
				return (GameObject)Resources.Load("Prefabs/Enemies/Heli");
			break;
			
			default:
				return (GameObject)Resources.Load("Prefabs/Enemies/Boat");
			break;
		}
	}
	
	private void loadFuel(float position)
	{
		if(fuelCounter > ((Ingame.DIFICULTY + 1)))
		{
			GameObject fuel = (GameObject)Resources.Load("Prefabs/sceneParts/Elements/fuel");
			MonoBehaviour.Instantiate(fuel, new Vector3(Random.Range(-3, 3),0,position),Quaternion.identity);
			fuelCounter = 0;
		}
		else
		{
			fuelCounter ++;
		}
	}
}
