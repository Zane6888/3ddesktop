using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing;
using _3DRoom;



public class start : MonoBehaviour {
	
	public GameObject shortc;
	//public GameObject room;
	public UnityEngine.Font font;
	public string noicon = "416e09677f119c42bcced015bc3f3b2c";
	public Material notex;
	
	
	public int scalex = 16;
	public int scaley = 9;
	public int countx = 15;
	public int county = 8;

	public static SCStorage store;
	
	// Use this for initialization
	void Start () {
		store = new SCStorage(8,15,null);
		float xadd = scalex/countx;
		float yadd = scaley/county;
		
		string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
		string publicdesktop = Environment.ExpandEnvironmentVariables(@"%PUBLIC%\Desktop");
		//Debug.Log(desktop);
		//Debug.Log(publicdesktop);
		
		int i = 0;
		GameObject r;
		Texture2D tex;
		ByteArrayImg img;
		List<ShortcutData> d = ReadConfig.getShortcutData();

		//Byte[] b ;
		List<string> files =  new List<string>(Directory.GetFiles(desktop));
		files.AddRange(Directory.GetFiles(publicdesktop));
		
		foreach(string file in files)
		{	
			try{		
				String s = file;
				string ext = new FileInfo(file).Extension;
				if(ext == ".lnk")
				{		
					s = FileHelper.GetShortcutTargetFile(file);
					s = s == "" ? file : s;
					ext = new FileInfo(s).Extension;
				}
					
				img = FileHelper.GetFileIcon(s);
				tex = new Texture2D(img.width,img.height);
				string hash = img.GetHash();	
				//Debug.Log("hash:"+hash);	
				tex.LoadImage(img.bytes);
				r = (GameObject)Instantiate(shortc,new Vector3((float)(i%countx + 1) * xadd ,
						yadd * (float)(Math.Floor((double)i/countx)+1),0f),Quaternion.identity);
				//Debug.Log("dcount:"+d.Count);
				if(d.Find(da => {return da.path == s;}) != null)				
					r.renderer.material = d.Find(da => {return da.path == s;}).getMaterial();				
				else if(File.Exists(ShortcutData.iconPath+@"\"+ext.Substring(1)+".png"))
					{
						tex = new Texture2D(img.width,img.height);
						tex.LoadImage(File.ReadAllBytes(ShortcutData.iconPath+@"\"+ext.Substring(1)+".png"));
						r.renderer.material.mainTexture = tex;
					}
					else{
						if(noicon == hash)
							r.renderer.material = notex;
						else
							r.renderer.material.mainTexture = tex;						
					}
				
					
				r.GetComponent<shortcuthit>().target = s;
	
				//newText(s,2 * (i%20) - 45 ,2 * (int)Math.Floor(i/20m)+ 6,40);
				store.addShortCut(r,0,i%15,(int)Math.Floor(i/15m));	
			}
			catch(Exception e)
			{
				Debug.LogException(e);
			}
			i++;
		}
		Screen.lockCursor = true;
	/*	
	Rigidbody b = (Rigidbody)Instantiate(shortc, new Vector3(10,10,10),Quaternion.identity);
	b.GetComponent<shortcuthit>().target = @"D:\Users\Simon\Desktop\w";
	b = (Rigidbody)Instantiate(shortc, new Vector3(10.5f,20.0f,10.0f),Quaternion.identity);
	b.GetComponent<shortcuthit>().target = @"D:\Users\Simon\Desktop\v";	
	*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	GameObject newText(string text,float x,float y,float z)
	{
		
		GameObject wallTxt = new GameObject("TextField");
		wallTxt.AddComponent("TextMesh");
		//wallTxt.AddComponent("MeshRenderer");
		MeshRenderer meshRender = (MeshRenderer)wallTxt.GetComponent("MeshRenderer");

		meshRender.material = font.material;
		((TextMesh)wallTxt.GetComponent ("TextMesh")).text = text;
		
		((TextMesh)wallTxt.GetComponent("TextMesh")).font = font;
		
		wallTxt.transform.position.Set(x,y,z);
		
		//Debug.Log (x+" "+y+" "+z);
		return wallTxt;
	
	}
}
