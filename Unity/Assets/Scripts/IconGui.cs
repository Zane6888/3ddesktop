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
		points.Add("rename");
		currentMenu = new List<GameObject>();
	}

	public void showFor(GameObject player,GameObject shortCut)
	{
		Vector3 sLoc = shortCut.transform.position;
		Vector3 pLoc = player.transform.position;
		Vector3 add;
		float baseAdd = 0.2f;
		GameObject menu = right;
		GameObject cam = player.transform.FindChild("Main Camera").gameObject;

		while(currentMenu.Count != 0)
		{
			Destroy(currentMenu[0]);
			currentMenu.RemoveAt(0);
		}
		Quaternion rot = shortCut.transform.rotation;
		if(rot ==  Quaternion.AngleAxis(0,Vector3.up))
		   	{
			menu = pLoc.z >= sLoc.z ? left : right;
			add = new Vector3(baseAdd,0f,menu == left ? -1 * baseAdd : baseAdd);
		}
		else if(rot == Quaternion.AngleAxis(90,Vector3.up))
		    {
			menu = pLoc.x >= sLoc.x ? left : right;
			add = new Vector3(menu == left ? baseAdd : -1 * baseAdd,0f,-1 * baseAdd);
		}
		else if(rot == Quaternion.AngleAxis(180,Vector3.up))
			{
			menu = pLoc.z <= sLoc.z ? left : right;
			add = new Vector3(-1 * baseAdd,0f,menu == left ? baseAdd : -1 * baseAdd);
		}
		else if(rot == Quaternion.AngleAxis(-90,Vector3.up))
		    {
			menu = pLoc.x <= sLoc.x ? left : right;
			add = new Vector3(menu == left ? -1 * baseAdd : baseAdd,0f,baseAdd);
		}
		else
		{
			throw new UnityException("you fucked up pretty hard");
		}
		int i = 0;
		Debug.Log(menu == right ? "right" : "left");
		Quaternion finalRot = cam.transform.rotation * Quaternion.Euler(0,360-player.transform.rotation.eulerAngles.x,0);
		GameObject container = new GameObject();

		foreach(string s in points)
		{

			GameObject o = (GameObject)Instantiate(menu,new Vector3(0,i * 0.21f,0),new Quaternion());
			o.tag = "menu_" + s;
			o.transform.parent = container.transform;
			TextMesh txt = (TextMesh)o.transform.FindChild("text").GetComponent(typeof (TextMesh));
			txt.text = s;
			currentMenu.Add(o);
			i++;
		}
		container.transform.position = new Vector3(sLoc.x +0.1f,sLoc.y,sLoc.z+0.1f) + add;
		container.transform.rotation = finalRot;

	}

}
