using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Gtk;


public class FileDialog : MonoBehaviour {

	public static string FileChooserTitle;
	public string Path;
	//windows.Forms.OpenFileDialog funktioniert nicht, da Unity3d
	//nicht damit kompatibel ist. 

	// Use this for initialization
	void Start () {
		FileChooserTitle = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static string openFile()
	{

		FileChooserDialog FileChooser = new FileChooserDialog(FileChooserTitle,null,FileChooserAction.Open);

		FileChooser.AddButton (Stock.Cancel, ResponseType.Cancel);
		FileChooser.AddButton (Stock.Open, ResponseType.Ok);

		ResponseType RetVal = (ResponseType)FileChooser.Run ();

		if(RetVal == ResponseType.Ok)
		{
			return FileChooser.Filename ;
		}
		return "";
	}

}
