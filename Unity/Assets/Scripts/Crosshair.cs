using UnityEngine;
using System.Xml;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class Crosshair : MonoBehaviour {
	
	public Color colwhite;
	public Color colblack;
	public Color coltrans;
	public Color colcustom;
	public Texture2D crosshair;
	public Texture2D crosshair_shown;

	public ControlControl cc;
	public static string configPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\config\";
	public static string shortcutConfig = "crosshair.xml";

	// Use this for initialization
	
	void Start () {
		try
		{
			setcrosshair();
		}
		catch (FileNotFoundException fnfe)
		{
			Debug.Log("crosshair.xml not found. generating...");
			
			genConfig();
			Debug.Log("crosshair.xml generated. Now starting to paint...");
			setcrosshair();
		}

	}

	
	public void ChangeColor (int r, int g, int b, int a) {
		crosshair_shown = new Texture2D(crosshair.width, crosshair.height);
		colwhite = new Color(1,1,1,1);
		colblack = new Color(0,0,0,1);
		coltrans = new Color(1,1,1,0);
		colcustom = ColorRGBtoDecimal(r,g,b,a);


		for(int i=0; i < crosshair.width; i ++)
		{
			for (int j=0; j < crosshair.height; j++)
			{
				Color pixel = crosshair.GetPixel (i,j);
				if(pixel.Equals(colwhite))
				{
					crosshair_shown.SetPixel (i,j,coltrans);
					//Debug.Log ("TO trans at " + i + ", " + j + ", COLOR " + crosshair.GetPixel (i, j));
					
				}
				else if (pixel.Equals(colblack))
				{
					crosshair_shown.SetPixel (i,j,colcustom);
					//Debug.Log ("TO black at " + i + ", " + j + ", COLOR " + crosshair.GetPixel (i, j));
				}
				else
				{
					crosshair_shown.SetPixel (i,j,crosshair.GetPixel(i,j));
				}
			}
		}
		crosshair_shown.Apply ();


		//Debug.Log ("Width = " + cc.pausegui.Preview.width);
		//Debug.Log ("Height = " + cc.pausegui.Preview.height);
		for (int i = 0; i < cc.pausegui.Preview.height; i ++)
		{
			for (int j = 0; j < cc.pausegui.Preview.width; j ++)
			{
				cc.pausegui.Preview.SetPixel (i, j, colcustom);
			}
		}
		cc.pausegui.Preview.Apply();
		//Debug.Log ("Preview applyed");
	}

	
	// Update is called once per frame
	void Update () {
	}

	Color ColorRGBtoDecimal (int r, int g, int b, int a)
	{
		Color col = new Color (r/255f, g/255f, b/255f, a/255f);
		return col;
	}

	void OnGUI()
	{
		float xMin = (Screen.width)/2 - (crosshair_shown.width /2);
		float yMin = (Screen.height)/2 - (crosshair_shown.height /2);
		
		
		GUI.DrawTexture (new Rect(xMin,yMin,crosshair_shown.width, crosshair_shown.height),crosshair_shown);
	}

	public void setcrosshair()
	{
		XmlDocument xml = new XmlDocument();
		try{
			xml.Load(configPath+shortcutConfig);
		}
		catch(Exception e)
		{
			throw new FileNotFoundException();
		}
		string data = "";
		string path = "";
		foreach(XmlNode n1 in xml.FirstChild.ChildNodes)
		{
			if(n1.Name =="path")
				{
					path = n1.InnerText;
					Debug.Log("Got crosshair path: " + path);
					break;
				}
		}
		data = path;
		xml.Save(configPath+shortcutConfig);		
		
		try
		{
			crosshair = GetTextureFromImage(data);
		}
		catch(ArgumentOutOfRangeException e)
		{
			throw new FileNotFoundException();
		}
		ChangeColor(255,255,255,255);
	}

	public void genConfig()
	{
		XmlDocument xml = new XmlDocument();
		XmlNode x = xml.CreateElement("crosshair");
		xml.AppendChild(x);
		
		//x.AppendChild(xml.CreateElement("wallpaper"));
		x.AppendChild(xml.CreateElement("path"));
		x.LastChild.InnerText = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\icons\crosshair.png";
		
		xml.Save(configPath+shortcutConfig);
	}

	static Texture2D GetTextureFromImage(string path)
	{
		
		Texture2D texture = new Texture2D(/*xlength, ylenght*/200, 100);
		texture.LoadImage(File.ReadAllBytes(path));
		return texture;
	}
}
