using UnityEngine;
using System.Collections;

namespace Channel3.RetroRaid.Legacy
{
	public class MainMenu
	{
		private Button[] btn;
		private int menuState;
		private string iTemsTag = "mainMenuItem";
	
		private GameObject _cam;

		public MainMenu(int itens)
		{
			Init(itens);
		}
	
		public void Init(int itens)
		{
			_cam = GameObject.FindWithTag("MainCamera");
			_cam.transform.eulerAngles = new Vector3(90.0f, 0, 0);
			_cam.transform.position = new Vector3(0, 7, 2);
		
			createButtons(itens);
			menuState = 0;
			changeState();
		}
	
		public void createButtons(int itens)
		{
			float btnSpace = (float)1 / (4 + 1);
		
			btn = new Button[itens];
		
			btn[0] = new Button("New Game", iTemsTag, new Vector2(0.5f, btnSpace* 4), 40);
			btn[1] = new Button("Help", iTemsTag, new Vector2(0.5f, btnSpace * 3), 40);
			btn[2] = new Button("About", iTemsTag, new Vector2(0.5f, btnSpace * 2), 40);
			//btn[3] = new Button("Exit", iTemsTag, new Vector2(0.5f, btnSpace ), 40);
		}
	
		//Update current menu state
		public void UpdateMenuState ()
		{
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(menuState < 1)
					menuState = btn.Length - 1;
				else
					menuState --;
				changeState();
			}
		
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if(menuState >= (btn.Length - 1))//sub by 1 to get the last array element
					menuState = 0;
				else
					menuState ++;
			
				changeState();
			}
		
			if(Input.GetKeyDown(KeyCode.Return))
			{
				selectOption();
			}
		}
	
		//Change the button color
		private void changeState()
		{
			for(int i = 0; i < btn.Length; i++)
			{
				if(i == menuState)
					btn[i].setColor(Color.black);
				else
					btn[i].setColor(Color.white);
			}
		}
	
		//Selec the option by the current menu state
		private void selectOption()
		{
			GameObject [] Foo;
			Foo = GameObject.FindGameObjectsWithTag(iTemsTag);
			foreach(GameObject i in Foo)
				GameObject.Destroy(i);
					
		
			switch(menuState)
			{
				case 1:
					//Debug.Log("Help Screen");
					GameState.CURR_GAME_STATE = GameState.HELP_SCREEN;
					break;
			
				case 2:
					//Debug.Log("About Screen");
					GameState.CURR_GAME_STATE = GameState.ABOUT_SCREEN;
					break;
			
				case 3:
					//Debug.Log("QUIT");
					Application.Quit();
					break;
			
				default:
					//Debug.Log("New Game");
					GameState.CURR_GAME_STATE = GameState.NEW_GAME;
					break;
			}
		}
	}
}

//[System.Serializable]