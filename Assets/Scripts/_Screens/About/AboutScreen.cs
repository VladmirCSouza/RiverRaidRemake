using UnityEngine;
using System.Collections;

public class AboutScreen
{
	private GUIText textLayout;
	private GameObject text;
	
	private GUIText backLayout;
	private GameObject backText;
	
	private Vector2 _position;
	private string iTemsTag = "aboutItems";
	
	private Font mFont;
	
	public AboutScreen()
	{
		mFont = (Font)Resources.Load("Fonts/pixelFont3");
		
		_position = new Vector2(0.5f, 0.5f);
		
		text = new GameObject("about_text");
		text.transform.position = _position;
		
		textLayout = (GUIText)text.AddComponent(typeof(GUIText));
		text.guiText.font = mFont;
		text.guiText.fontSize = 15;
		text.guiText.anchor = TextAnchor.MiddleCenter;
		text.guiText.alignment = TextAlignment.Center; 
		text.tag = iTemsTag;
		text.guiText.text = Texts.ABOUT;
		
		_position = new Vector2(1.0f, 0.0f);
		
		backText = new GameObject("about_text");
		backText.transform.position = _position;
		backLayout = (GUIText)backText.AddComponent(typeof(GUIText));
		backText.guiText.font = mFont;
		backText.guiText.fontSize = 10;
		backText.guiText.anchor = TextAnchor.LowerRight;
		backText.tag = iTemsTag;
		backText.guiText.text = Texts.BACK;
	}
	
	public void control()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			GameObject [] Foo;
		    Foo = GameObject.FindGameObjectsWithTag(iTemsTag);
		    foreach(GameObject i in Foo)
				GameObject.Destroy(i);
			
			GameState.CURR_GAME_STATE = GameState.MAIN_MENU;
		}
	}
}
