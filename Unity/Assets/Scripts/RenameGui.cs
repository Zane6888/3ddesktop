using UnityEngine;
using System.Collections;

public class RenameGui : MonoBehaviour {

	public ControlControl cc;

	private int guiWidth = 300;
	private int guiHeight = 150;
	private int tbWidth = 200;
	private int tbHeight = 40;
	private int textWidth = 50;

	void OnGUI()
	{
		int xText = (tbWidth+textWidth+Screen.width)/2;

		GUI.TextField(new Rect(xText+ textWidth,Screen.height/2,tbWidth,tbHeight),"");
		GUI.Box(new Rect((Screen.width-guiWidth)/2,(Screen.height-guiHeight)/2,guiWidth,guiHeight),"Rename");
	}
}
