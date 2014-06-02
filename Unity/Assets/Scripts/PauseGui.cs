using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Diagnostics;




public class PauseGui : MonoBehaviour {
	
	public ControlControl cc;
	// Use this for initialization
	public bool showCursorChGUI = false;
	public bool showWallpapersChGUI = false;
	public bool showOptionsGUI = false;
	public bool showShutdownGUI = false;
	public bool showShutdownMenuGUI = false;
	public bool showControlsGUI = false;
	public bool showMainMenuGUI = true;

	public int ShutdownSource;
	public string ShutdownButtonText;

	public string Red = "";
	public string Green = "";
	public string Blue = "";
	public string Alpha = "";
	public string Crosshairpath = "";
	public string Northwall = "";
	public string Eastwall = "";
	public string Southwall = "";
	public string Westwall = "";

	public Texture2D Preview;

	public FileDialog Open;


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
		if(showMainMenuGUI)
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
		}

		if(showShutdownGUI)
		{
			GUI.Box (new Rect(Screen.width/2 +110,Screen.height/2 -105,200,265),"");
			if(GUI.Button (new Rect (Screen.width/2 +160,Screen.height/2-80,100,50),"Logout"))
				Logout();
			if(GUI.Button (new Rect (Screen.width/2 +160,Screen.height/2-25,100,50),"Restart"))
				Restart();
			if(GUI.Button (new Rect (Screen.width/2 +160,Screen.height/2+30,100,50),"Shutdown"))
				Shutdown();
			if(GUI.Button (new Rect (Screen.width/2 +160,Screen.height/2+85,100,50),"Back"))
			   BackShutdown();
		}
		if(showShutdownMenuGUI)
		{
			GUI.Box (new Rect (Screen.width/2-100, Screen.height/2 -55,200,110),"");
			if(GUI.Button (new Rect (Screen.width/2 -90,Screen.height/2,85,30),ShutdownButtonText))
				ShutdownMenuClick();
			if(GUI.Button (new Rect (Screen.width/2 +5, Screen.height/2,85,30),"Cancel"))
				CancelShutdown();

			GUI.Label (new Rect (Screen.width/2 -70, Screen.height/2-45,140,30),"What do you want to do?");
		}

		if(showOptionsGUI)
		{
		GUI.Box (new Rect(Screen.width/2 + 110, Screen.height/2 -105,200,265),"");
		if(GUI.Button (new Rect(Screen.width/2+150, Screen.height/2 -80,120,50),"Controls"))
			Controls();
		if(GUI.Button (new Rect (Screen.width/2 +150, Screen.height/2 -25, 120,50),"Change Cursor"))
			ChCursor ();
		if(GUI.Button (new Rect (Screen.width/2 +150, Screen.height/2 + 30, 120,50),"Change Wallpapers"))
			ChWallpapers ();
		if(GUI.Button (new Rect (Screen.width /2 + 150, Screen.height /2 +85, 120,50),"Back"))
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

		if(showWallpapersChGUI)
		{
			GUI.Box (new Rect(Screen.width/2+320,Screen.height/2 -105,200,265),"");
			if(GUI.Button (new Rect (Screen.width/2 +340, Screen.height/2 +30,80,50),"Apply"))
				ApplyWallpapers();
			if(GUI.Button (new Rect(Screen.width/2 +340,Screen.height/2 +85,80,50),"Back"))
				BackWallpapers();
			if(GUI.Button (new Rect (Screen.width/2 + 470, Screen.height/2+-95,45,20),"Open"))
				OpenWallpapersPath();

			Northwall = GUI.TextField (new Rect(Screen.width/2+385, Screen.height/2 -70,130,20),Northwall,3);
			Eastwall = GUI.TextField (new Rect(Screen.width/2+385, Screen.height/2-45,130,20),Eastwall,3);
			Southwall = GUI.TextField (new Rect(Screen.width/2+385,Screen.height/2-20,130,20),Southwall,3);
			Westwall = GUI.TextField (new Rect(Screen.width/2+385,Screen.height/2+5,130,20),Westwall,3);
			
			GUI.Label(new Rect(Screen.width/2+345,Screen.height/2-70, 35, 20), "North:");
			GUI.Label(new Rect(Screen.width/2+345,Screen.height/2-45, 35, 20), "East:");
			GUI.Label(new Rect(Screen.width/2+345,Screen.height/2-20, 35, 20), "South:");
			GUI.Label(new Rect(Screen.width/2+345,Screen.height/2+5, 35, 20), "West:");
			
			
			//Debug.Log ("Width = " + Preview.width);
			//Debug.Log ("Height = " + Preview.height);
			
			//GUI.DrawTexture (new Rect(Screen.width/2 + 430, Screen.height/2 +50, cc.cross.crosshair_shown.width, cc.cross.crosshair_shown.height), cc.cross.crosshair_shown);//Preview.width, Preview.height),Preview);
			
		}

	}
	
	void Close()
	{
		showShutdownGUI = false;
		showCursorChGUI = false;
		showWallpapersChGUI = false;
		showOptionsGUI = false;
		showControlsGUI =false;
		showShutdownGUI=false;
		showShutdownMenuGUI=false;
		cc.toNormal();
	}

	void ShutdownOptions()
	{
		if(showShutdownGUI == false)
		{
			showControlsGUI =false;
			showCursorChGUI =false;
			showWallpapersChGUI = false;
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
			showWallpapersChGUI = false;
			showControlsGUI = false;
			showOptionsGUI = false;
		}
	}


	void Controls()
	{
		if (showControlsGUI ==false)
		{
			showCursorChGUI =false;
			showWallpapersChGUI = false;
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
			showWallpapersChGUI = false;
			showCursorChGUI = true;
		}
		else
			showCursorChGUI = false;
	}

	void ChWallpapers()
	{
		if (showWallpapersChGUI == false)
		{
			showControlsGUI = false;
			showCursorChGUI = false;
			showWallpapersChGUI = true;
		}
		else
			showWallpapersChGUI = false;
	}

	void Logout()
	{
		ShutdownSource = 0;
		ShutdownButtonText = "Logout";
		showShutdownGUI=false;
		showOptionsGUI=false;
		showCursorChGUI = false;
		showWallpapersChGUI = false;
		showControlsGUI =false;
		showMainMenuGUI=false;
		showShutdownMenuGUI = true;

	}	
	void Restart()
	{
		ShutdownSource=1;
		ShutdownButtonText = "Restart";
		showShutdownGUI=false;
		showOptionsGUI=false;
		showCursorChGUI = false;
		showWallpapersChGUI = false;
		showControlsGUI =false;
		showMainMenuGUI=false;
		showShutdownMenuGUI = true;


	}	
	void Shutdown()
	{
		ShutdownSource=2;
		ShutdownButtonText = "Shutdown";
		showShutdownGUI=false;
		showOptionsGUI=false;
		showCursorChGUI = false;
		showWallpapersChGUI = false;
		showControlsGUI =false;
		showMainMenuGUI=false;
		showShutdownMenuGUI = true;

	}

	void ShutdownMenuClick()
	{
		if(ShutdownSource==0)
		{
			Process.Start ("shutdown","/l");
		}
		if(ShutdownSource==1)
		{
			Process.Start ("shutdown","/r");
		}
		if(ShutdownSource==2)
		{
			Process.Start ("shutdown","/s");
		}
	}

	void CancelShutdown()
	{
		showShutdownGUI=true;
		showMainMenuGUI=true;
		showShutdownMenuGUI = false;
	}

	void BackShutdown()
	{
		showShutdownGUI = false;
	}


	void BackOptions()
	{
		showControlsGUI = false;
		showCursorChGUI = false;
		showWallpapersChGUI = false;
		showOptionsGUI = false;
	}

	void BackControls()
	{
		showControlsGUI = false;
	}


	void OpenCursorPath()
	{
		/*
		Open = new FileDialog();
		Open.FileChooserTitle = "Choose the new Crosshair";
		Crosshairpath = Open.openFile ();

		ApplyCursor();
		*/
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
			UnityEngine.Debug.Log ("Failed to draw crosshair. String instead of number? Path valid?");
		}
	}

	void BackCursor()
	{
		showCursorChGUI = false;
		showWallpapersChGUI = false;
	}

	void OpenWallpapersPath()
	{
		/*
		Open = new FileDialog();
		Open.FileChooserTitle = "Choose the new Crosshair";
		Crosshairpath = Open.openFile ();

		ApplyCursor();
		*/
	}

	void ApplyWallpapers()
	{
		try
		{

		}
		catch (Exception e)
		{
			UnityEngine.Debug.Log ("Failed to draw crosshair. String instead of number? Path valid?");
		}
	}

	void BackWallpapers()
	{
		showWallpapersChGUI = false;
	}

	void End()
	{
		//UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}
}
