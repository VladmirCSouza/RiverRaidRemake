using UnityEngine;
using System.Collections;

public class GameState
{
	public const int MAIN_MENU 			= 		0;
	public const int ABOUT_SCREEN 		= 		1;
	public const int HELP_SCREEN 		= 		2;
	public const int NEW_GAME	 		= 		3;
	
	public static int CURR_GAME_STATE			=		MAIN_MENU;
}
