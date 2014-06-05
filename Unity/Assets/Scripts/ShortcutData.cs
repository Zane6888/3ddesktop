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

	private int _wall;
	public int wall{
		get{
			return _wall;
		}
	}

	private int _x;
	public int x{
		get{
			return _x;
		}
	}

	private int _y;
	public int y{
		get{
			return _y;
		}
	}

	public static string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\3ddesktop";
	public static string iconPath = basePath+@"\icons";
	
	public ShortcutData(string name,string path):this()
	{
		this.name = name;
		this.path = path;
	}
	public ShortcutData()
	{
		_wall = -1;
		_x = -1;
		_y = -1;
		modelEnabled = false;
		extensionStandard = false;
		iconId = -1;
	}

	private void setName(string name, GameObject nameObject)
	{
		if(nameObject == null)
			return;
		if(name == "")
			name = "Missingna.";
		//Debug.Log("name set to: "+name);
		TextMesh txt = (TextMesh)nameObject.transform.FindChild("text").GetComponent(typeof (TextMesh));
		txt.text = name;
	}
	public void setPosition(int wall, int x, int y)
	{
		setPosition(wall,x,y,true);
	}
	public void setPosition(int wall, int x, int y,bool updateconfig)
	{
		_wall = wall;
		_x = x;
		_y = y;
		if(updateconfig)
			ReadConfig.updateConfig(this);
	}

	public void rename(string newName, bool keepOSFilename)
	{
		name = newName;
		string oldPath = path;

		if(!keepOSFilename)
		{
			path = oldPath.Replace(Path.GetFileName(path),newName);
			File.Move(oldPath,path);
		}

		ReadConfig.updateConfig(oldPath,this);
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

		Material m = new Material(Shader.Find("Transparent/Diffuse"));
		Texture2D tex = new Texture2D(128,128);
		tex.LoadImage(File.ReadAllBytes(p));
		m.SetTexture("_MainTex",tex);
		return m;
	}

	public bool hasTexture()
	{
		if(iconId != -1)
			if(extensionStandard)
				return File.Exists(iconPath+@"\"+Path.GetExtension(path).Substring(1)+".png");
			else 
				return File.Exists(iconPath+@"\"+iconId+".png");
		return false;
	}
	
	public void loadIcon(string _iconPath)
	{
		Texture2D tex = new Texture2D(128,128);
		tex.LoadImage(File.ReadAllBytes(_iconPath));
		setIcon(tex);
	}

	public void setIcon(Texture2D tex)
	{
		int id = ReadConfig.getNextIconId();
		using(FileStream f = new FileStream(iconPath+@"\"+id+".png",FileMode.Create,FileAccess.Write))
		{
			byte[] b = tex.EncodeToPNG();
			f.Write(b,0,b.Length);
		}
		iconId = id;
	}

	public void run()
	{
		System.Diagnostics.Process.Start(path);
	}
}
