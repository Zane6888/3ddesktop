using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Gtk;


public static class FileDialog  {

	public static string FileChooserTitle;
	public static string Path;
	//windows.Forms.OpenFileDialog funktioniert nicht, da Unity3d
	//nicht damit kompatibel ist. 

	// Use this for initializatio
	public static string openFile()
	{
		Debug.Log("Went in openFile method");
		FileChooserDialog FileChooser = new FileChooserDialog(FileChooserTitle,null,FileChooserAction.Open);
		Debug.Log("Created FileDialog");
		FileChooser.AddButton (Stock.Cancel, ResponseType.Cancel);
		FileChooser.AddButton (Stock.Open, ResponseType.Ok);
		Debug.Log("Running FileDialog");
		ResponseType RetVal = (ResponseType)FileChooser.Run ();
		Debug.Log("Dialog shown");
		//if(RetVal == ResponseType.Ok)
		//{
		//	return FileChooser.Filename ;
		//}
		return "";
	}

}
