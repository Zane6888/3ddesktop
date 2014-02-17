using UnityEngine;
using System.Collections;
using System;


public class PauseGui : MonoBehaviour {
	
	public ControlControl cc;
	// Use this for initialization
	public bool showCursorChGUI = false;
	public bool showOptionsGUI = false;
	public bool showShutdownGUI = false;
	public bool showControlsGUI = false;

	public string Red = "R";
	public string Green = "G";
	public string Blue = "B";
	public string Alpha = "A";

	public Texture2D Preview = new Texture2D(70,100);

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 - 105,200,265),"");	
		if(GUI.Button(new Rect(Screen.width/2 - 55,Screen.height/2 -80,110,50),"Continue"))
			Close();
		if(GUI.Button (new Rect (Screen.width/2-55,Screen.height/2 -25,110,50),"Shutdown Option"))
			ShutdownOptions();
		if(GUI.Button (new Rect(Screen.width/2-55,Screen.height/2 +30,110,50),"Options"))
			Options();
		if(GUI.Button(new Rect(Screen.width/2 -55, Screen.height/2+85,110,50),"End"))
			End ();

		if(showShutdownGUI)
		{
			GUI.Box (new Rect(Screen.width/2 +110,Screen.height/2 -50,200,210),"");
			if(GUI.Button (new Rect (Screen.width/2 +160,Screen.height/2+85,100,50),"Back"))
			   BackShutdown();
		}

		if(showOptionsGUI)
		{
		GUI.Box (new Rect(Screen.width/2 + 110, Screen.height/2 -50,200,210),"");
		if(GUI.Button (new Rect(Screen.width/2+160, Screen.height/2 -25,100,50),"Controls"))
			Controls();
		if(GUI.Button (new Rect (Screen.width/2 +160, Screen.height/2 + 30, 100,50),"Change Cursor"))
			ChCursor ();
		if(GUI.Button (new Rect (Screen.width /2 + 160, Screen.height /2 +85, 100,50),"Back"))
			BackOptions();
		}



		if(showControlsGUI)
		{
			GUI.Box (new Rect(Screen.width/2+320,Screen.height/2 -50,200,210),"");
			if(GUI.Button (new Rect(Screen.width/2+370,Screen.height/2+85,100,50),"Back"))
				BackControls();
		}


		if(showCursorChGUI)
		{
		GUI.Box (new Rect(Screen.width/2+320,Screen.height/2 -50,200,210),"");
		Red = GUI.TextField (new Rect(Screen.width/2+340, Screen.height/2 -25,80,20),Red,3);
		Green = GUI.TextField (new Rect(Screen.width/2+425, Screen.height/2-25,80,20),Green,3);
		Blue = GUI.TextField (new Rect(Screen.width/2+340,Screen.height/2,80,20),Blue,3);
		Alpha = GUI.TextField (new Rect(Screen.width/2+425,Screen.height/2,80,20),Alpha,3);
		GUI.DrawTexture (new Rect(Screen.width/2 + 430, Screen.height/2 +30, 70,100),Preview);
		if(GUI.Button (new Rect (Screen.width/2 +340, Screen.height/2 +30,80,50),"Apply"))
			ApplyCursor();
		if(GUI.Button (new Rect(Screen.width/2 +340,Screen.height/2 +85,80,50),"Back"))
			BackCursor();
		}

	}
	
	void Close()
	{
		showShutdownGUI = false;
		showCursorChGUI = false;
		showOptionsGUI = false;
		showControlsGUI =false;
		cc.toNormal();
	}

	void ShutdownOptions()
	{
		if(showShutdownGUI == false)
		{
			showControlsGUI =false;
			showCursorChGUI =false;
			showOptionsGUI =false;
			showShutdownGUI =true;
		}
		else
		{
			showShutdownGUI =false;
		}
	}

	void Options()
	{
		if(showOptionsGUI == false)
		{
			showShutdownGUI = false;
			showOptionsGUI = true;
		}
		else
		{
			showCursorChGUI = false;
			showControlsGUI = false;
			showOptionsGUI = false;
		}
	}


	void Controls()
	{
		if (showControlsGUI ==false)
		{
			showCursorChGUI =false;
			showControlsGUI = true;
		}
		else
		{
			showControlsGUI = false;
		}
	}

	void ChCursor()
	{
		if (showCursorChGUI == false)
		{
			showControlsGUI = false;
			showCursorChGUI = true;
		}
		else
			showCursorChGUI = false;
	}

	void BackShutdown()
	{
		showShutdownGUI = false;
	}

	void BackOptions()
	{
		showControlsGUI = false;
		showCursorChGUI = false;
		showOptionsGUI = false;
	}

	void BackControls()
	{
		showControlsGUI = false;
	}

	void ApplyCursor()
	{
		try
		{
		int intred = Convert.ToInt16(Red);
		int intgreen = Convert.ToInt16(Green);
		int intblue = Convert.ToInt16(Blue);
		int intalpha = Convert.ToInt16(Alpha);

		if (intred > 255)
		{
			intred = 255;
		}
		else if (intred < 0)
		{
			intred = 0;
		}

		if (intgreen > 255)
		{
			intgreen = 255;
		}
		else if (intgreen < 0)
		{
			intgreen = 0;
		}

		if (intblue > 255)
		{
			intblue = 255;
		}
		else if (intblue < 0)
		{
			intblue = 0;
		}

		if (intalpha > 255)
		{
			intalpha = 255;
		}
		else if (intalpha < 0)
		{
			intalpha = 0;
		}

		cc.cross.ChangeColor(intred,intgreen,intblue,intalpha);

		}
		catch (Exception e)
		{
			Debug.Log ("Can't use this color for the Crosshair. Did you set a string instead of a number in the options?");
		}
	}

	void BackCursor()
	{
		showCursorChGUI = false;
	}

	void End()
	{
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}
}
