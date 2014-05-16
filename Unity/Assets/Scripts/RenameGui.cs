using UnityEngine;
using System.Collections;

public class RenameGui : MonoBehaviour {

	public ControlControl cc;

	private static int guiWidth = 300;
	private static int guiHeight = 150;
	private static int tbHeight = 20;
	private static int textWidth = 50;
	private static int space = 20;
	private static int tSpace = (int)(space * 1.5);
	private static int tbWidth = guiWidth - 3*space - textWidth;
	private static int y = Screen.height/2 + tSpace - guiHeight/2;

	public string newName = "";
	public bool move = true;

	void OnGUI()
	{
		int xText = (Screen.width - guiWidth)/2 + space;
	
		GUI.Label(new Rect(xText,y,textWidth,tbHeight),"Name:");
		newName = GUI.TextField(new Rect(xText+ textWidth + space,y,tbWidth,tbHeight),newName);
		GUI.Box(new Rect((Screen.width-guiWidth)/2,(Screen.height-guiHeight)/2,guiWidth,guiHeight),"Rename");
		int y2 = y + space/2 + tbHeight;
		move = GUI.Toggle(new Rect(xText,y2,20,20),move,"");
		GUI.Label(new Rect(xText + space,y2,guiWidth - 2*space,tbHeight),"change filename in OS");
	}
}
