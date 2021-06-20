using UnityEngine;
using System.Collections;

public class Button
{
	private GUIText buttonLayout;
	private GameObject button;
	private Font mFont;
	
	
	public Button(string label, string tag, Vector2 position, int fontSize)
	{
		mFont = (Font)Resources.Load("Fonts/pixelFont3");
		
		button = new GameObject("btn_" + label);
		button.transform.position = position;
		
		buttonLayout = (GUIText)button.AddComponent(typeof(GUIText));
		button.guiText.font = mFont;
		button.guiText.fontSize = fontSize;
		button.guiText.anchor = TextAnchor.MiddleCenter;
		button.guiText.text = label;
		button.tag = tag;
	}
	
	public void hideBtn()
	{
		button.active = false;
	}
	
	public void showBtn()
	{
		button.active = true;
	}
	
	/*
	 * Set the button color
	 * @param color - the color from button
	*/
	public void setColor(Color color)
	{
		buttonLayout.guiText.material.color = color;
	}
}
