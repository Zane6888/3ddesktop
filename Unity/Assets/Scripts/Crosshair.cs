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
		ChangeColor(255,255,255,255);
	}

	
	public void ChangeColor (int r, int g, int b, int a) {
		crosshair_shown = new Texture2D(crosshair.width, crosshair.height);
		colwhite = new Color(1,1,1,1);
		colblack = new Color(0,0,0,1);
		coltrans = new Color(1,1,1,0);
		colcustom = ColorRGBtoDecimal(r,g,b,a);


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



		Debug.Log ("Width = " + cc.pausegui.Preview.width);
		Debug.Log ("Height = " + cc.pausegui.Preview.height);
		for (int i = 0; i < cc.pausegui.Preview.height; i ++)
		{
			for (int j = 0; j < cc.pausegui.Preview.width; j ++)
			{
				cc.pausegui.Preview.SetPixel (i, j, colcustom);
			}
		}
		cc.pausegui.Preview.Apply();
		Debug.Log ("Preview applyed");
	}


	
	// Update is called once per frame
	void Update () {
	}

	Color ColorRGBtoDecimal (int r, int g, int b, int a)
	{
		Color col = new Color (r/255f, g/255f, b/255f, a/255f);
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
