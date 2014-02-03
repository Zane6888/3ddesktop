using UnityEngine;
using System.Collections.Generic;

public class IconGui : MonoBehaviour {

	public static IconGui i;

	public GameObject right;
	public GameObject left;

	public List<string> points;
	public List<GameObject> currentMenu;
	// Use this for initialization
	void Start () {
		IconGui.i = this;
		points = new List<string>();
		points.Add("move");
		points.Add("delete");
		currentMenu = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showFor(GameObject player,GameObject shortCut)
	{
		Vector3 sLoc = shortCut.transform.position;
		GameObject menu = right;


		while(currentMenu.Count != 0)
		{
			Destroy(currentMenu[0]);
			currentMenu.RemoveAt(0);
		}
		//todo
/*		switch(shortCut.transform.rotation)
		{
		case Quaternion.AngleAxis(Vector3.up,0):

			break;
		case Quaternion.AngleAxis(Vector3.up,90):
			
			break;
		case Quaternion.AngleAxis(Vector3.up,180):
			
			break;
		case Quaternion.AngleAxis(Vector3.up,-90):
			
			break;
		default:
			throw new UnityException("you fucked up pretty hard");
			break;
		}*/
		int i = 0;
		foreach(string s in points)
		{

			GameObject o = (GameObject)Instantiate(right,new Vector3(sLoc.x +0.1f,sLoc.y + i * 0.21f,sLoc.z+0.1f),player.transform.rotation);
			TextMesh txt = (TextMesh)o.transform.FindChild("text").GetComponent(typeof (TextMesh));
			txt.text = s;
			currentMenu.Add(o);
			i++;
		}


	}

}
