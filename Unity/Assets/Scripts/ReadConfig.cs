using System.Collections.Generic;
using System.Xml;
using System;
using System.Linq;
using System.IO;
using UnityEngine;

public static class ReadConfig
{
	public static string configPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\config\";
	public static string iconpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\icons\";
	public static string shortcutConfig = "shortcut.xml";

	public static bool locked = false;

	public static List<ShortcutData> currentConfig;


	public static List<ShortcutData> getShortcutData()
	{
		Debug.Log("Reading xml");
		XmlDocument xml = new XmlDocument();
		try{
		xml.Load(configPath+shortcutConfig);
		}
		catch(Exception)
		{
		createConfig();
			return getShortcutData();
		}
		List<ShortcutData> data = new List<ShortcutData>();
		ShortcutData d;
		Debug.Log(xml.ToString());
		foreach(XmlNode n1 in xml.FirstChild.ChildNodes)
		{
			//Debug.Log(n1.Name+"/"+n1.InnerXml);
			d = new ShortcutData();
			int x = -1;
			int y = -1;
			int wall = -1;
			foreach(XmlNode n2 in n1.ChildNodes)
			{
				switch(n2.Name)
				{
				case "name":
					d.name = n2.InnerText;
					break;
					
				case "path":
					d.path = n2.InnerText;
					break;
					
				case "icon":
					try
					{
						d.iconId = System.Convert.ToInt32(n2.InnerText);
					}
					catch(Exception)
					{
						d.loadIcon(n2.InnerText);
						n2.InnerText = d.iconId.ToString ();
					}
					break;
					
				case "model":
				//TODO
				break;
					
				case "model_enable":
				if(n2.InnerText == "true")
					d.modelEnabled = true;
					else d.modelEnabled = false;
					break;
					
				case "extension_standard":
				if(n2.InnerText == "true")
					d.modelEnabled = true;
					else d.extensionStandard = false;
					break;
				case "x":
					x = Convert.ToInt32(n2.InnerText);
					break;
				case "y":
					y = Convert.ToInt32(n2.InnerText);
					break;
				case "wall":
					wall = Convert.ToInt32(n2.InnerText);
					break;
				}
				
			}
			if(x != -1 && y != -1 && wall != -1)
				d.setPosition(wall,x,y,false);
			data.Add(d);
		}
		xml.Save(configPath+shortcutConfig);
		currentConfig = data;
		return data;
	}
	public static void updateConfig(ShortcutData data)
	{
		updateConfig(data.path,data);
	}

	public static void updateConfig(string oldPath, ShortcutData newData)
	{
		currentConfig.RemoveAll(s => s.path == oldPath);
		currentConfig.Add(newData);
		writeCurrentConfig();
	}

	private static void writeCurrentConfig()
	{
		if(locked)
		{
			Debug.Log("config locked, not writing");
			return;
		}
		Debug.Log("writing config");
		XmlDocument xml = new XmlDocument();
		XmlNode baseNode = xml.CreateElement("shortcuts");
		xml.AppendChild(baseNode);
		foreach(ShortcutData s in currentConfig)
		{
			XmlNode shortcut = xml.CreateElement("shortcut");
			baseNode.AppendChild(shortcut);

			XmlNode x = xml.CreateElement("name");
			x.InnerText = s.name;
			shortcut.AppendChild(x);

			x = xml.CreateElement("path");
			x.InnerText = s.path;
			shortcut.AppendChild(x);

			x = xml.CreateElement("icon");
			x.InnerText = s.iconId+"";
			shortcut.AppendChild(x);

			/*
			x = xml.CreateElement("model");
			TODO
			shortcut.AppendChild(x);
			*/

			x = xml.CreateElement("model_enable");
			x.InnerText = s.modelEnabled ? "true" : "false";
			shortcut.AppendChild(x);

			x = xml.CreateElement("extension_standard");
			x.InnerText = s.extensionStandard ? "true" : "false";
			shortcut.AppendChild(x);

			x = xml.CreateElement("x");
			x.InnerText = s.x.ToString();
			shortcut.AppendChild(x);

			x = xml.CreateElement("y");
			x.InnerText = s.y.ToString();
			shortcut.AppendChild(x);

			x = xml.CreateElement("wall");
			x.InnerText = s.wall.ToString();
			shortcut.AppendChild(x);
		}

		xml.Save(configPath+shortcutConfig);
	}

	public static void setLocked(bool flag)
	{
		if(!flag && locked)
		{
			locked = false;
			writeCurrentConfig();
		}
		locked = flag;
	}
	
	public static void createConfig()
	{
		XmlDocument xml = new XmlDocument();
		XmlNode x = xml.CreateElement("shortcuts");
		x.AppendChild(xml.CreateElement("shortcut"));
		xml.AppendChild(x);
		xml.Save(configPath+shortcutConfig);
	}
	
	public static int getNextIconId()
	{
		List<string> content = new List<string>(Directory.GetFiles(iconpath));
		int num = 0;

		foreach(string s in content)
		{
			String name = new FileInfo(s).Name.Replace(new FileInfo(s).Extension,"");
			try
			{
			int i = Convert.ToInt32(name);
			if(i > num)
				num = i;
			}
			catch(Exception){}
		}
		num++;

		return num;
	}

}
