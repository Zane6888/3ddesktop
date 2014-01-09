using UnityEngine;
using System.Collections;

public class PauseGui : MonoBehaviour {
	
	public ControlControl cc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 - 50,200,100),"");	
		if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 -25,100,50),"Continue"))
			Close();
		
	}
	
	void Close()
	{
		cc.toNormal();
	}
}
