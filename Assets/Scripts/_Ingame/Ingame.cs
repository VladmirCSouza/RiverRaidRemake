using UnityEngine;
using System.Collections;

public class Ingame
{
	private Player _player;
	private CreateStage _cStage;
	private HUD _hud;
	//private CameraCtrl _camCtrl;
	public static Score _score;
	
	private PauseMenu _pMenu;
	
	public static bool PAUSE;
	public static bool RESPAWN;
	public static bool FILL;
	private bool firstLaunch;
	
	
	public static int DIFICULTY;
	private int playerLifes;
	
	private float timer;
	
	private Font mFont;
	
	public Ingame()
	{
		playerLifes = 3;
		init();
		
	}
	
	public void init()
	{
		_cStage = new CreateStage();
		_player = new Player();
		
		if(_pMenu == null)
			_pMenu = new PauseMenu();
		
		/*if(_camCtrl == null)
			_camCtrl = new CameraCtrl();*/
		
		if(_score == null)
			_score = new Score();
		
		_hud = new HUD(playerLifes);
		
		DIFICULTY = 0;
		PAUSE = true;
		firstLaunch = true;
		RESPAWN = false;
		FILL = false;
		_hud.startMessage(Texts.START);
		
	}
	
	public void update()
	{
		PAUSECtrl(); 
		playerGetHit();
		if(!PAUSE && !_player.getHit)
		{
			firstLaunch = false;
			_cStage.update();
			_player.update();
			fuelCtrl();
			_pMenu.update(false);
			changeDificulty();
				
			_hud.updateHud();
			//_camCtrl.update();
		}
		else
		{
			if(!firstLaunch && !_player.getHit)
				_pMenu.update(true);
		}
	}
	
	public void playerGetHit()
	{
		if(_player.getHit)
		{
			if(!RESPAWN)
			{
				timer = Time.time;
				RESPAWN = true;
				_player.explosion();
				GameObject [] Foo;
				Foo = GameObject.FindGameObjectsWithTag("Player");
				clearElement(Foo);
			}
			else if(playerLifes == 0)
			{
				_hud.hudCtrl("GAME OVER! \n" +
					"Press enter to return to main menu");
				_score.resetScore();
				if(Input.GetKeyDown(KeyCode.Return))
				{
					clearStage();
	    			GameState.CURR_GAME_STATE = GameState.MAIN_MENU;
				}
			}
			else if(Time.time > (timer + 5))
			{
				playerLifes --;
				clearStage();
				init();
				//_camCtrl.resetCam();
			}
			else
			{
				_hud.hudCtrl(_player.hitTag +"!\n" + "RESPAWN IN: " 
				             		+ Mathf.FloorToInt(( (timer + 6) - Time.time)) + " seconds");				
			}
		}
	}
	
	
	public void fuelCtrl()
	{
		if(FILL)
		{
			if(HUD.FUEL < 0.56f)
				HUD.FUEL += 0.0005f;
		}
		else
		{
			if(HUD.FUEL > 0.44f)
				HUD.FUEL -= _player.fuelCurr;
		}
	}
	
	public void PAUSECtrl()
	{
		if(!RESPAWN)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				if(PAUSE)
				{
					if(_pMenu.getState() == 0)
					{
						PAUSE = false;
						_hud.hudCtrl(" ");
					}
					else
					{
						clearStage();
		    			GameState.CURR_GAME_STATE = GameState.MAIN_MENU;
					}
				}
				else
				{
					Time.timeScale = 0;
					PAUSE = true;
				}	
			}
		}
	}
	
	private void changeDificulty()
	{
		if(_score.getScore() < 50000)
			DIFICULTY = 0;
		else if(_score.getScore() < 100000)
			DIFICULTY = 1;
		else if(_score.getScore() < 1000000)
			DIFICULTY = 2;
	}
	
	public void clearStage()
	{
		GameObject [] Foo;
		Foo = GameObject.FindGameObjectsWithTag("inGameItem");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("Player");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("light");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("ingameHUD");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("pauseMenu");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("tiro");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("base");
		clearElement(Foo);
		Foo = GameObject.FindGameObjectsWithTag("enemy");
		clearElement(Foo);
	}
	
	
	
	private void clearElement(GameObject[] obj)
	{
		foreach(GameObject i in obj)
			GameObject.Destroy(i);
	}
}
