using UnityEngine;
using System.Collections;

public class RenameGui : MonoBehaviour {

	public ControlControl cc;
		
	void OnGUI()
	{
		GUI.TextField(new Rect(Screen.width/2,Screen.height/2,200,40),"");
	}
}
