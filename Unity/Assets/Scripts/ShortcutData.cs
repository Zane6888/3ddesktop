using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ShortcutData 
{
	public string name;
	public string path;
	public int iconId;
	// model
	public bool modelEnabled;
	public bool extensionStandard;
	
	public static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\3ddesktop";
	public static string iconPath = basePath+@"\icons";
	
	public ShortcutData(string name,string path)
	{
		this.name = name;
		this.path = path;
	}
	public ShortcutData(){}
	
	public Material getMaterial()
	{
		string p;
		if(!extensionStandard)
			p = iconPath+@"\"+iconId+".png";
		else
		{
			
			p = iconPath+@"\"+Path.GetExtension(path).Substring(1)+".png";
		}
		Debug.Log(p);
		Material m = new Material(Shader.Find("Transparent/Diffuse"));
		Texture2D tex = new Texture2D(128,128);
		tex.LoadImage(File.ReadAllBytes(p));
		m.SetTexture("_MainTex",tex);
		return m;
	}
	
	public void loadIcon(string _iconPath)
	{
		Texture2D tex = new Texture2D(128,128);
		tex.LoadImage(File.ReadAllBytes(_iconPath));
 		using(FileStream f = new FileStream(iconPath+@"\"+ReadConfig.getNextIconId()+".png",FileMode.Create,FileAccess.Write))
		{
			byte[] b = tex.EncodeToPNG();
			f.Write(b,0,b.Length);
		}
	}
}
