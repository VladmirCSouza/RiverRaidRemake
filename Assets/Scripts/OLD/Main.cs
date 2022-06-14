using UnityEngine;
using System.Collections;
using Channel3.RetroRaid.Legacy;


public class Main : MonoBehaviour
{
	private MainMenu _mainMenu;
	private AboutScreen _aboutScreen;
	private HelpScreen _helpScreen;
	private Ingame _ingame;
	private int menuItems;
	
	// Use this for initialization
	void Start ()
	{
		// TODO UPDATE THE SHOW CURSOR TO A THE NEW SYSTEM
		//Screen.showCursor = false;
		menuItems = 3;
		_mainMenu = new MainMenu(menuItems);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(Time.timeScale);
		
		switch(GameState.CURR_GAME_STATE)
		{
			case GameState.MAIN_MENU:
				if(_mainMenu == null)
				{
					_mainMenu = new MainMenu(menuItems);
					_ingame = null;
					_aboutScreen = null;
					_helpScreen = null;
				}
				else
					_mainMenu.UpdateMenuState();
				break;
			
			case GameState.ABOUT_SCREEN:
				if(_aboutScreen == null)
				{
					_mainMenu = null;
					_helpScreen = null;
					_aboutScreen = new AboutScreen();
				}
				else
					_aboutScreen.control();
				break;
			
		case GameState.HELP_SCREEN:
				if(_helpScreen == null)
				{
					_mainMenu = null;
					_aboutScreen = null;
					_helpScreen = new HelpScreen();
				}
				else
					_helpScreen.control();
				break;
			
		case GameState.NEW_GAME:
			if(_ingame == null)
			{
				_mainMenu = null;
				_ingame = new Ingame();
			}
			else
				_ingame.update();
			break;
			
			default:
				break;
				
		}
	}
}
