using System.Collections.Generic;
using System.Xml;
using System;
using System.Linq;
using UnityEngine;

public static class ReadConfig
{
	public static string configPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"\3ddesktop\config\";
	public static string shortcutConfig = "shortcut.xml";

	public static List<ShortcutData> currentConfig;


	public static List<ShortcutData> getShortcutData()
	{
		Debug.Log("Reading xml");
		XmlDocument xml = new XmlDocument();
		try{
		xml.Load(configPath+shortcutConfig);
		}
		catch(Exception e)
		{
		createConfig();
			return getShortcutData();
		}
		List<ShortcutData> data = new List<ShortcutData>();
		ShortcutData d;
		Debug.Log(xml.ToString());
		foreach(XmlNode n1 in xml.FirstChild.ChildNodes)
		{
			Debug.Log(n1.Name+"/"+n1.InnerXml);
			d = new ShortcutData();
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
					catch(Exception ex)
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
				}
				
			}
			data.Add(d);
		}
		xml.Save(configPath+shortcutConfig);
		currentConfig = data;
		return data;
	}

	public static void updateConfig(string oldPath, ShortcutData newData)
	{
		currentConfig.RemoveAll(s => s.path == oldPath);
		currentConfig.Add(newData);

	}

	private static void writeCurrentConfig()
	{
		XmlDocument xml = new XmlDocument();
		XmlNode baseNode = xml.CreateElement("shortcuts");
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
			x.InnerText = s.modelEnabled ? "true" : "flase";
			shortcut.AppendChild(x);

			x = xml.CreateElement("extension_standard");
			x.InnerText = s.extensionStandard ? "true" : "flase";
			shortcut.AppendChild(x);
		}

		xml.Save(configPath+shortcutConfig);
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
		//TODO 
		return -1;
	}

}
