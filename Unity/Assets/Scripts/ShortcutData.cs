using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ShortcutData
{
	private string _name;
	public string name{
		get
		{
			return _name;
		}
		set{
			_name = value;
			setName(value,nameObject);
		}
	}
	public string path;
	public int iconId;
	// model
	public bool modelEnabled;
	public bool extensionStandard;
	private GameObject _nameObject;
	public GameObject nameObject{
		get{
			return _nameObject;
		}
		set{
			_nameObject = value;
			setName(name,value);
		}

	}

	public static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\3ddesktop";
	public static string iconPath = basePath+@"\icons";
	
	public ShortcutData(string name,string path)
	{
		this.name = name;
		this.path = path;
	}
	public ShortcutData(){}

	private void setName(string name, GameObject nameObject)
	{
		if(nameObject == null)
			return;
		if(name == "")
			name = "Missingna.";
		Debug.Log("name set to: "+name);
		TextMesh txt = (TextMesh)nameObject.transform.FindChild("text").GetComponent(typeof (TextMesh));
		txt.text = name;
	}

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

	public void run()
	{
		System.Diagnostics.Process.Start(path);
	}
}
