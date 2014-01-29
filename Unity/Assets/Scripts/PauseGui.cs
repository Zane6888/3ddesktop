using UnityEngine;
using System.Collections;

public class PauseGui : MonoBehaviour {
	
	public ControlControl cc;
	// Use this for initialization
	public bool showCursorChGUI = false;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 - 50,200,210),"");	
		if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 -25,100,50),"Continue"))
			Close();
		if(GUI.Button (new Rect(Screen.width/2-50,Screen.height/2 +30,100,50),"Change Cursor"))
			ChCursor();
		if(GUI.Button(new Rect(Screen.width/2 -50, Screen.height/2+85,100,50),"End"))
			End ();

		if(!showCursorChGUI)return;
		GUI.Box (new Rect(Screen.width/2+110,Screen.height/2 +5,200,200),"");
		if(GUI.Button (new Rect(Screen.width/2 +160,Screen.height/2 +30,100,50),"Back"))
			Back();


	}
	
	void Close()
	{
		showCursorChGUI = false;
		cc.toNormal();
	}
	void ChCursor()
	{
		showCursorChGUI = true;
	
	}

	void Back()
	{
		showCursorChGUI = false;
	}

	void End()
	{
		Application.Quit();
	}
}
