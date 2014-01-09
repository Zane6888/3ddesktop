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
		
		
		
		
		XmlDocument xml = new XmlDocument();
		try{
		xml.Load(configPath+shortcutConfig);
		}
		catch(Exception e)
		{
			genConfig();
			xml.Load(configPath+shortcutConfig);
		}
		List<string> data = new List<string>();
		string path = "";
		Debug.Log(xml.ToString());
		foreach(XmlNode n1 in xml.FirstChild.ChildNodes)
		{
			Debug.Log(n1.Name+"/"+n1.InnerXml);
			foreach(XmlNode n2 in n1.ChildNodes)
			{
				
				switch(n2.Name)
				{
				case "path":
					path = n2.InnerText;
					Debug.Log("Got texture path: " + path);
					break;
				}
				
			}
			data.Add(path);
		}
		xml.Save(configPath+shortcutConfig);		
		
		
		TXnorth = GetTextureFromImage(data[0]);
		TXeast = GetTextureFromImage(data[1]);
		TXsouth = GetTextureFromImage(data[2]);
		TXwest = GetTextureFromImage(data[3]);
		
		Debug.Log("Textures defined");
		
		//GameObject.Find("NorthWall").renderer.material.mainTexture = TXnorth;
		GameObject.Find("NorthWall").renderer.material.mainTexture = TXnorth;
		GameObject.Find("EastWall").renderer.material.mainTexture = TXeast;
		GameObject.Find("SouthWall").renderer.material.mainTexture = TXsouth;
		GameObject.Find("WestWall").renderer.material.mainTexture = TXwest;
		
		Debug.Log("Wallpapers are now colored");
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void genConfig()
	{
		XmlDocument xml = new XmlDocument();
		XmlNode x = xml.CreateElement("wallpapers");
		
		xml.AppendChild(x);
		xml.Save(configPath+shortcutConfig);
		
	}
	
	static Texture2D GetTextureFromImage(string path)
	{
		
		Texture2D texture = new Texture2D(1920, 1080);
		texture.LoadImage(File.ReadAllBytes(path));
		return texture;
	}
	
}
