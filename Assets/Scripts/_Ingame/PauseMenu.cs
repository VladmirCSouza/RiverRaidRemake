using UnityEngine;
using System.Collections;

public class PauseMenu
{
	private Button[] btn;
	private string iTemsTag = "pauseMenu";
	private int menuState;
	
	public PauseMenu()
	{
		btn = new Button[2];
		
		btn[0] = new Button("Resume", iTemsTag, new Vector2(0.5f, 0.7f), 40);
		btn[1] = new Button("Exit", iTemsTag, new Vector2(0.5f, 0.5f), 40);
		btn[0].hideBtn();
		btn[1].hideBtn();
		
		menuState = 0;
	}
	
	public void update(bool isPaused)
	{
		if(isPaused)
		{
			btn[0].showBtn();
			btn[1].showBtn();
			
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(menuState != 0)
				{
					menuState = 0;
				}
				else
				{
					menuState = 1;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if(menuState != 0)
				{
					menuState = 0;
				}
				else
				{
					menuState = 1;
				}
			}
			
			changeSelected();
		}
		else
		{
			btn[0].hideBtn();
			btn[1].hideBtn();
		}
	}
	
	public void changeSelected()
	{
		if(menuState == 0)
		{
			btn[0].setColor(Color.black);
			btn[1].setColor(Color.white);
		}
		else
		{
			btn[0].setColor(Color.white);
			btn[1].setColor(Color.black);
		}
	}
	
	public int getState()
	{
		return menuState;
	}
}
