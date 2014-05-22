using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Gtk;


public class FileDialog : Gtk.Window {

	public FileDialog():base("FileDialogTitle")
	{

	}

	public string FileChooserTitle;
	public string path;

	// Use this for initializatio
	public string openFile()
	{

		FileChooserDialog Fc = new FileChooserDialog("Choose new Crosshair",this,FileChooserAction.Open,"Cancel",ResponseType.Cancel,"Open",ResponseType.Accept);




		//if(Fc.Run() == (int)ResponseType.Accept)        //Unity stirbt hier
		//{
		//	FileChooser chooser = new FileChooser(Fc);
		//	path = chooser.Filename;
		//
		    path = Fc.Filename;
			Fc.Destroy ();
			//return path;                //Inhalt von CrosshairGUI ist leer
		//}

		Fc.Destroy ();
		return "";

	}

}
