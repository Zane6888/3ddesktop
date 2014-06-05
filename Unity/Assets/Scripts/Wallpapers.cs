using UnityEngine;
using System.Xml;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class Wallpapers : MonoBehaviour {
	
	public Texture TXnorth;	//x negativ
	public Texture TXsouth;	//x positiv
	public Texture TXeast; 	//z positiv
	public Texture TXwest;	//z negativ
	public static string configPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\config\";
	public static string shortcutConfig = "wallpapers.xml";
	//http://answers.unity3d.com/questions/14748/c-resourcesload-problem.html
	
	// Use this for initialization
	void Start () {

		
		try
		{
			setwallpapers();
		}
		catch (FileNotFoundException fnfe)
		{
			//Debug.Log("wallpaper.xml not found. generating...");

			genConfig();
			//Debug.Log("wallpaper.xml generated. Now starting to paint...");
			setwallpapers();

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setwallpapers ()
	{
		XmlDocument xml = new XmlDocument();
		try{
			xml.Load(configPath+shortcutConfig);
		}
		catch(Exception e)
		{
			throw new FileNotFoundException();
		}
		List<string> data = new List<string>();
		string path = "";
		foreach(XmlNode n1 in xml.FirstChild.ChildNodes)
		{
			switch(n1.Name)
			{
			case "northwall":
				foreach(XmlNode n2 in n1.ChildNodes)
				{
					switch(n2.Name)
					{
						
					case "path":
						path = n2.InnerText;
						//Debug.Log("Got wallpaper path: " + path);
						break;
					}
					data.Add(path);
				}
				break;
			
			case "eastwall":
				foreach(XmlNode n2 in n1.ChildNodes)
				{
					switch(n2.Name)
					{
						
					case "path":
						path = n2.InnerText;
						//Debug.Log("Got wallpaper path: " + path);
						break;
					}
					data.Add(path);
				}
				break;

			case "southwall":
				foreach(XmlNode n2 in n1.ChildNodes)
				{
					switch(n2.Name)
					{
						
					case "path":
						path = n2.InnerText;
						//Debug.Log("Got wallpaper path: " + path);
						break;
					}
					data.Add(path);
				}
				break;

			case "westwall":
				foreach(XmlNode n2 in n1.ChildNodes)
				{
					switch(n2.Name)
					{
						
					case "path":
						path = n2.InnerText;
						//Debug.Log("Got wallpaper path: " + path);
						break;
					}
					data.Add(path);
				}
				break;
			}
		}
		xml.Save(configPath+shortcutConfig);		
		
		try{
			//Northwall
			if (data[0] == "")
			{
				TXnorth = (Texture2D)Resources.Load("Textures/wallpapers/northwall");
			}
			else
			{
				TXnorth = GetTextureFromImage(data[0]);
			}

			//Eastwall
			if (data[1] == "")
			{
				TXeast = (Texture2D)Resources.Load("Textures/wallpapers/eastwall");
			}
			else
			{
				TXeast = GetTextureFromImage(data[1]);
			}

			//Southwall
			if (data[2] == "")
			{
				TXsouth = (Texture2D)Resources.Load("Textures/wallpapers/southwall");
			}
			else
			{
				TXsouth = GetTextureFromImage(data[2]);
			}

			//Westwall
			if (data[3] == "")
			{
				TXwest = (Texture2D)Resources.Load("Textures/wallpapers/westwall");
			}
			else
			{
				TXwest = GetTextureFromImage(data[3]);
			}
		}
		catch(ArgumentOutOfRangeException e)
		{
			throw new FileNotFoundException();
		}
		
		
		
		//Debug.Log("Textures defined");
		
		//GameObject.Find("NorthWall").renderer.material.mainTexture = TXnorth;
		GameObject.Find("NorthWall").renderer.material.mainTexture = TXnorth;
		GameObject.Find("EastWall").renderer.material.mainTexture = TXeast;
		GameObject.Find("SouthWall").renderer.material.mainTexture = TXsouth;
		GameObject.Find("WestWall").renderer.material.mainTexture = TXwest;
		
		//Debug.Log("Wallpapers are now colored");
	}

	void genConfig()
	{
		XmlDocument xml = new XmlDocument();
		XmlNode x = xml.CreateElement("wallpapers");
		xml.AppendChild(x);

		x.AppendChild(xml.CreateElement("northwall"));
		x.LastChild.AppendChild(xml.CreateElement("path"));
		//x.LastChild.LastChild.InnerText = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\icons\northwall.jpg";

		x.AppendChild(xml.CreateElement("eastwall"));
		x.LastChild.AppendChild(xml.CreateElement("path"));
		//x.LastChild.LastChild.InnerText = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\icons\eastwall.jpg";

		x.AppendChild(xml.CreateElement("southwall"));
		x.LastChild.AppendChild(xml.CreateElement("path"));
		//x.LastChild.LastChild.InnerText = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\icons\southwall.jpg";

		x.AppendChild(xml.CreateElement("westwall"));
		x.LastChild.AppendChild(xml.CreateElement("path"));
		//x.LastChild.LastChild.InnerText = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\icons\westwall.jpg";


		xml.Save(configPath+shortcutConfig);
	}

	public void changewallpapers (string northwallpath, string eastwallpath, string southwallpath, string westwallpath)
	{
		XmlDocument xml = new XmlDocument();
		xml.Load(configPath+shortcutConfig);
		try
		{
			//foreach (XmlElement element in xml.ChildNodes)
			foreach (XmlNode element in xml.FirstChild.ChildNodes)
			{


				switch (element.Name)
				{
				case "northwall":
					foreach (XmlNode child in element.ChildNodes)
					{
							if (child.Name == "path")
							{
							element.FirstChild.InnerText = northwallpath;
							}
					}

					break;
				case "eastwall":
					foreach (XmlNode child in element.ChildNodes)
					{
							if (child.Name == "path")
							{
							element.FirstChild.InnerText = eastwallpath;
							}
					}

					break;
				case "southwall":
					foreach (XmlNode child in element.ChildNodes)
					{
							if (child.Name == "path")
							{
							element.FirstChild.InnerText = southwallpath;
							}
					}

					break;
				case "westwall":
					foreach (XmlNode child in element.ChildNodes)
					{
							if (child.Name == "path")
							{
							element.FirstChild.InnerText = westwallpath;
							}
					}
					break;

			}

		}
		}
		catch
		{
		}
		xml.Save(configPath+shortcutConfig);
		setwallpapers();
	}

	static Texture2D GetTextureFromImage(string path)
	{
		
		Texture2D texture = new Texture2D(1920, 1080);
		texture.LoadImage(File.ReadAllBytes(path));
		return texture;
	}
	
}
