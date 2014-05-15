using UnityEngine;
using System.Collections;
using System;
using System.Xml;


public class PauseGui : MonoBehaviour {
	
	public ControlControl cc;
	// Use this for initialization
	public bool showCursorChGUI = false;
	public bool showOptionsGUI = false;
	public bool showShutdownGUI = false;
	public bool showControlsGUI = false;

	public string Red = "";
	public string Green = "";
	public string Blue = "";
	public string Alpha = "";
	public string Crosshairpath = "";

	public Texture2D Preview;

	public static string configPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\config\";
	public static string shortcutConfig = "crosshair.xml";


	void Start () {
		XmlDocument xml = new XmlDocument();
		xml.Load(configPath+shortcutConfig);
		foreach(XmlNode n1 in xml.FirstChild.ChildNodes)
		{
			switch (n1.Name)
			{
			case "path":
				Crosshairpath = n1.InnerText;
				//Debug.Log("Got crosshair path: " + Crosshairpath);
				break;

			case "color_part_red":
				Red = n1.InnerText;
				//Debug.Log("Got crosshair color red: " + Red);
				break;
				
			case "color_part_green":
				Green = n1.InnerText;
				//Debug.Log("Got crosshair color green: " + Green);
				break;
				
			case "color_part_blue":
				Blue = n1.InnerText;
				//Debug.Log("Got crosshair color blue: " + Blue);
				break;
				
			case "color_part_alpha":
				Alpha = n1.InnerText;
				//Debug.Log("Got crosshair color alpha: " + Alpha);
				break;
			}
		}
		xml.Save(configPath+shortcutConfig);	


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
		GUI.Box (new Rect(Screen.width/2+320,Screen.height/2 -105,200,265),"");
			if(GUI.Button (new Rect (Screen.width/2 +340, Screen.height/2 +30,80,50),"Apply"))
				ApplyCursor();
			if(GUI.Button (new Rect(Screen.width/2 +340,Screen.height/2 +85,80,50),"Back"))
				BackCursor();
			if(GUI.Button (new Rect (Screen.width/2 + 470, Screen.height/2+-95,45,20),"Open"))
				OpenCursorPath();
		Crosshairpath = GUI.TextField (new Rect(Screen.width/2+325, Screen.height/2-95,140,20),Crosshairpath);
		Red = GUI.TextField (new Rect(Screen.width/2+385, Screen.height/2 -70,130,20),Red,3);
		Green = GUI.TextField (new Rect(Screen.width/2+385, Screen.height/2-45,130,20),Green,3);
		Blue = GUI.TextField (new Rect(Screen.width/2+385,Screen.height/2-20,130,20),Blue,3);
		Alpha = GUI.TextField (new Rect(Screen.width/2+385,Screen.height/2+5,130,20),Alpha,3);

		GUI.Label(new Rect(Screen.width/2+345,Screen.height/2-70, 35, 20), "Red:");
		GUI.Label(new Rect(Screen.width/2+345,Screen.height/2-45, 35, 20), "Green:");
		GUI.Label(new Rect(Screen.width/2+345,Screen.height/2-20, 35, 20), "Blue:");
		GUI.Label(new Rect(Screen.width/2+345,Screen.height/2+5, 35, 20), "Alpha:");
		

		//Debug.Log ("Width = " + Preview.width);
		//Debug.Log ("Height = " + Preview.height);

		GUI.DrawTexture (new Rect(Screen.width/2 + 430, Screen.height/2 +50, cc.cross.crosshair_shown.width, cc.cross.crosshair_shown.height), cc.cross.crosshair_shown);//Preview.width, Preview.height),Preview);

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


	void OpenCursorPath()
	{
		ApplyCursor();
	}

	void ApplyCursor()
	{
		try
		{
		int intred = Convert.ToInt16(Red);
		int intgreen = Convert.ToInt16(Green);
		int intblue = Convert.ToInt16(Blue);
		int intalpha = Convert.ToInt16(Alpha);
		string path = Crosshairpath;

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

		cc.cross.SetColor(path, intred,intgreen,intblue,intalpha);
		
		
		}
		catch (Exception e)
		{
			Debug.Log ("Failed to draw crosshair. String instead of number? Path valid?");
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
