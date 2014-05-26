using UnityEngine;
using System.Collections;

public class RenameGui : MonoBehaviour {

	public ControlControl cc;

	private static int guiWidth = 300;
	private static int guiHeight = 110;
	private static int tbHeight = 20;
	private static int textWidth = 50;
	private static int space = 20;
	private static int tSpace = (int)(space * 1.5);
	private static int tbWidth = guiWidth - 3*space - textWidth;
	private static int bwidth = 50;

	private string newName = "";
	private bool change = true;
	private bool apply = false;
	private bool cancel = false;

	private ShortcutData data;

	void OnGUI()
	{
		int y = (int)((Screen.height - guiHeight)/2) + tSpace;
		int xText = (Screen.width - guiWidth)/2 + space;
		
		GUI.Label(new Rect(xText,y,textWidth,tbHeight),"Name:");
		newName = GUI.TextField(new Rect(xText+ textWidth + space,y,tbWidth,tbHeight),newName);
		GUI.Box(new Rect((Screen.width-guiWidth)/2,(Screen.height-guiHeight)/2,guiWidth,guiHeight),"Rename");
		int y2 = y + space/2 + tbHeight;
		change = GUI.Toggle(new Rect(xText,y2,20,20),change,"");
		GUI.Label(new Rect(xText + space,y2,guiWidth - 2*space,tbHeight),"change filename in OS");

		cancel = GUI.Button(new Rect(Screen.width/2 + 5,y2 + tbHeight,bwidth,tbHeight),"cancel");
		apply = GUI.Button(new Rect(Screen.width/2 - 5 - bwidth,y2 + tbHeight,bwidth,tbHeight),"apply");

		if(data != null && (cancel || apply))
		{
			if(apply)
			{
				data.rename(newName, !change);
			}

			change = true;
			apply = false;
			cancel = false;

			Debug.Log("rename end");
			cc.toNormal();
		}
	}

	public void show(ShortcutData d)
	{
		this.enabled = true;
		data = d;
		newName = d.name;
	}
}
