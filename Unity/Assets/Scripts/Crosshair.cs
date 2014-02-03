using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	
	public Color colwhite;
	public Color colblack;
	public Color coltrans;
	public Color colcustom;
	public Texture2D crosshair;
	public Texture2D crosshair_shown;

	public ControlControl cc;


	// Use this for initialization
	
	void Start () {
		ChangeColor();
	}

	
	void ChangeColor () {
		crosshair_shown = new Texture2D(crosshair.width, crosshair.height);
		colwhite = new Color(1,1,1,1);
		colblack = new Color(0,0,0,1);
		coltrans = new Color(1,1,1,0);
		colcustom = ColorRGBtoDecimal(255,255,255,255);
		for(int i=0; i < crosshair.width; i ++)
		{
			for (int j=0; j < crosshair.height; j++)
			{
				Color pixel = crosshair.GetPixel (i,j);
				if(pixel.Equals(colwhite))
				{
					crosshair_shown.SetPixel (i,j,coltrans);
					//Debug.Log ("TO trans at " + i + ", " + j + ", COLOR " + crosshair.GetPixel (i, j));
					
				}
				else if (pixel.Equals(colblack))
				{
					crosshair_shown.SetPixel (i,j,colcustom);
					//Debug.Log ("TO black at " + i + ", " + j + ", COLOR " + crosshair.GetPixel (i, j));
				}
				else
				{
					crosshair_shown.SetPixel (i,j,crosshair.GetPixel(i,j));
				}
			}

		}
		crosshair_shown.Apply ();
		//Debug.Log ("Crozzhiar colrz apiplizd");
	}
	
	// Update is called once per frame
	void Update () {
	}

	Color ColorRGBtoDecimal (int r, int g, int b, int a)
	{
		Color col = new Color (r/255, g/255, b/255, a/255);
		return col;
	}

	void OnGUI()
	{
		/*
		for(int i=0; i <= crosshair.width; i ++)
		{

			for (int j=0; j <= crosshair.height; j++)
			{
				Color pixel = crosshair.GetPixel (i,j);
				//Debug.Log(pixel.ToString()+"->"+pixcol.ToString());
				
				if(!pixel.Equals(pixcol))
				{
					crosshair.SetPixel (i,j,newcol);
					//pixel = crosshair.GetPixel (i,j);
					//Debug.Log (pixel);
					Debug.Log ("hallo");
					
				}
				
			}

		}
		crosshair.Apply ();
		*/
		float xMin = (Screen.width)/2 - (crosshair_shown.width /2);
		float yMin = (Screen.height)/2 - (crosshair_shown.height /2);
		
		
		GUI.DrawTexture (new Rect(xMin,yMin,crosshair_shown.width, crosshair_shown.height),crosshair_shown);
	}
}
