using UnityEngine;
using System.Collections;
using TMPro;


// TODO CHANGE ALL THIS OLD HUD
public class HUD
{
	
	private Font mFont;
	private string iTemsTag = "ingameHUD";
	
	private TMP_Text startText;
	private GameObject startTextGO;
	
	private TMP_Text score;
	private GameObject scoreGO;
	
	private TMP_Text life;
	private GameObject lifeGO;
	
	private TMP_Text hudBG;
	private GameObject hudBGGO;
	
	private TMP_Text fuelScale;
	private GameObject fuelScaleGO;
	
	private TMP_Text fuelMeter;
	private GameObject fuelMeterGO;
	
	public static float FUEL;//varia de 0.44 ~ 0.56.. meio 0.5
	
	private int _life;
	
	public HUD(int life)
	{
		_life = life;
		FUEL = 0.56f;
		mFont = (Font)Resources.Load("Fonts/pixelFont3");
		createHudElement();
	}
	
	
	public void startMessage(string message)
	{
		mFont = (Font)Resources.Load("Fonts/pixelFont3");
		startTextGO = new GameObject("hud_startMessage");
		startTextGO.transform.position = new Vector2(0.5f, 0.5f);
		
		/*
		startText = (GUIText)startTextGO.AddComponent(typeof(GUIText));
		startTextGO.guiText.font = mFont;
		startTextGO.guiText.fontSize = 20;
		startTextGO.guiText.anchor = TextAnchor.MiddleCenter;
		startTextGO.tag = iTemsTag;
		startTextGO.guiText.text = message;
		*/
	}
	
	public void hudCtrl(string message)
	{
		/*
		startTextGO.guiText.text = message;
		startTextGO.transform.position = new Vector2(0.5f, 0.5f);
		*/
	}
	
	//Scale x2 y 0.2
	
	public void createHudElement()
	{
		hudBGGO = new GameObject("hud_Bg");
		hudBGGO.transform.position = new Vector3(0, 0, 0);
		//hudBG = (GUITexture)hudBGGO.AddComponent(typeof(GUITexture));
		//hudBGGO.guiTexture.texture = (Texture)Resources.Load("Textures/Hud/hudBG");
		hudBGGO.transform.localScale = new Vector3(2.0f, 0.2f, 1);
		hudBGGO.tag = iTemsTag;
		
		fuelScaleGO = new GameObject("hud_fuelScale");
		fuelScaleGO.transform.position = new Vector3(0.5f, 0.05f, 101.0f);
		//fuelScale = (GUITexture)fuelScaleGO.AddComponent(typeof(GUITexture));
		//fuelScaleGO.guiTexture.texture = (Texture)Resources.Load("Textures/Hud/fuelScale");
		fuelScaleGO.transform.localScale = new Vector3(0.15f, 0.05f, 1);
		fuelScaleGO.tag = iTemsTag;
		
		fuelMeterGO = new GameObject("hud_fuelMeter");
		fuelMeterGO.transform.position = new Vector3(0.56f, 0.047f, 100.0f);
		//fuelMeter = (GUITexture)fuelMeterGO.AddComponent(typeof(GUITexture));
		//fuelMeterGO.guiTexture.texture = (Texture)Resources.Load("Textures/Hud/fuelMeter");
		fuelMeterGO.transform.localScale = new Vector3(0.008f, 0.04f, 1);
		fuelMeterGO.tag = iTemsTag;
		
		scoreGO = new GameObject("hud_score");
		scoreGO.transform.position = new Vector3(0.9f, 0.05f, 10.0f);
		/*
		score = (GUIText)scoreGO.AddComponent(typeof(GUIText));
		scoreGO.guiText.font = mFont;
		scoreGO.guiText.fontSize = 20;
		scoreGO.guiText.anchor = TextAnchor.MiddleRight;
		scoreGO.guiText.material.color = Color.yellow;
		scoreGO.tag = iTemsTag;
		scoreGO.guiText.text = Ingame._score.getScore().ToString();
		
		
		lifeGO = new GameObject("hud_life");
		lifeGO.transform.position = new Vector3(0.2f, 0.05f, 10.0f);
		life = (GUIText)lifeGO.AddComponent(typeof(GUIText));
		lifeGO.guiText.font = mFont;
		lifeGO.guiText.fontSize = 20;
		lifeGO.guiText.anchor = TextAnchor.MiddleRight;
		lifeGO.guiText.material.color = Color.yellow;
		lifeGO.tag = iTemsTag;
		lifeGO.guiText.text = _life.ToString();
		*/
		
	}
	
	public void updateHud()
	{
		/*
		scoreGO.guiText.text = Ingame._score.getScore().ToString();
		lifeGO.guiText.text = _life.ToString();
		fuelMeterGO.transform.position = new Vector3(FUEL, 0.047f, 100.0f);
		*/
	}
}