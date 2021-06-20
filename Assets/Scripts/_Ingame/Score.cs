using UnityEngine;
using System.Collections;

public class Score
{
	private int _score;
	public Score()
	{
		_score = 0;
	}
	
	public void setScore(int points)
	{
		switch(Ingame.DIFICULTY)
		{
			case 1:
				_score += points * 2;
				break;
			case 2:
				_score += points * 3;
				break;
			default:
				_score += points;
				break;
		}
	}
	
	public int getScore()
	{
		return _score;
	}
	
	public void resetScore()
	{
		_score = 0;
	}
}
