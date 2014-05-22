using UnityEngine;
using System.Collections;

public class raycast : MonoBehaviour {

	// Use this for initialization
	public GameObject Cam;
	public GameObject player;
	public string hitname;
	private GameObject carriedObject;
	private GameObject currentMenuObject;
	
	public ControlControl cc;
	//public GameObject room;
	
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		

		
		if(Input.GetMouseButtonUp(0))
		{
			Debug.Log ("left hit");
			RaycastHit hit;
			Vector3 rayDirection = Cam.transform.forward;
			Ray selectionRay = new Ray(Cam.transform.position, rayDirection);
		
			if(Physics.Raycast(selectionRay, out hit))
			{
				Debug.Log(hit.collider.tag);
				if(hit.collider.tag == "shortcut" && carriedObject == null)
				{
					Debug.Log(hit.collider.gameObject.name);
					hit.collider.gameObject.GetComponent<ShortcutScript>().data.run();

				}
				if(hit.collider.tag == "menu_rename" && carriedObject == null)
				{
					cc.renameGui.show(currentMenuObject.GetComponentInChildren<ShortcutScript>().data);
					IconGui.i.close();
					cc.pause();

				}
				if(hit.collider.tag == "menu_move" && carriedObject == null)
				{
					IconGui.i.close();
					carriedObject = currentMenuObject;
					start.store.fillPlaceHolders("objectmove");
					start.store.removeShortCut(carriedObject);
				}
				
				if(hit.collider.tag == "objectmove" && carriedObject != null)
				{ 
					Vector3 targetLoc = start.store.findShortcut(hit.collider.gameObject);
					start.store.removePlaceHolders("objectmove");
					Debug.Log ("loc"+targetLoc);
					if(targetLoc.x != -1)
						start.store.addShortCut(carriedObject,(int)targetLoc.z,(int)targetLoc.x,(int)targetLoc.y);
					carriedObject = null;
				}
			}
		}
		
		if(Input.GetMouseButtonUp(1))
		{
			Debug.Log ("right hit");
			RaycastHit hit;
			Vector3 rayDirection = Cam.transform.forward;
			Ray selectionRay = new Ray(Cam.transform.position, rayDirection);
		
			if(Physics.Raycast(selectionRay, out hit))
			{
				Debug.Log(hit.collider.gameObject.transform.position+":"+hit.collider.gameObject.transform.localScale);

				if(hit.collider.tag == "shortcut")
				{
					currentMenuObject = hit.collider.gameObject;
					
					IconGui.i.showFor(player,currentMenuObject);
				}
			}
		}
	}
}
