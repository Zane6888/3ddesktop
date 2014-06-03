using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing;
using _3DRoom;



public class start : MonoBehaviour {
	
	public GameObject shortc;
	public GameObject scName;
	//public GameObject room;
	public UnityEngine.Font font;
	public string noicon = "416e09677f119c42bcced015bc3f3b2c";
	public Material notex;

	public int countx = 15;
	public int county = 8;

	public static SCStorage store;
	
	// Use this for initialization
	void Start () 
	{
		SCStorage.scName = scName;
		SCStorage.shortc = shortc;


		store = new SCStorage(county,countx,null);
		store.addAll(ReadConfig.getShortcutData());

		string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
		string publicdesktop = Environment.ExpandEnvironmentVariables(@"%PUBLIC%\Desktop");

		store.loadFromDir(0,desktop);
		//store.loadFromDir(0,publicdesktop);

		Screen.lockCursor = true;
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
