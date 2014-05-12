using UnityEngine;
using System.Collections;

public class ControlControl : MonoBehaviour {
	
	public PauseGui pausegui;
	public RenameGui renameGui;
	public Crosshair cross;
	public GameObject player;
	public raycast ray;

	// Use this for initialization
	void Start () 
	{
	cross.cc = this;
	pausegui.cc = this;
	ray.cc = this;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pausegui.enabled = true;
			pause();
		}

	}
	
	public void toNormal()
	{

		Screen.lockCursor = true;
		renameGui.enabled = false;
		pausegui.enabled = false;	
		cross.enabled = true;
		setPlayerEnabled(true);
		ray.enabled = true;
	}
	
	public void pause()
	{
		Screen.lockCursor = false;
		cross.enabled = false;
		setPlayerEnabled(false);
		ray.enabled = false;	
		//cross.gimmeThoseSizes();
	}
	
	public void setPlayerEnabled(bool enabled)
	{
		player.GetComponent<MouseLook>().enabled = enabled;
		player.GetComponent<CharacterMotor>().enabled = enabled;
		player.GetComponent<FPSInputController>().enabled = enabled;
		foreach(MouseLook m in player.GetComponentsInChildren<MouseLook>())
			m.enabled = enabled;
	}
	
	void onApplicationFocus(bool hasfocus)
	{
		if(!hasfocus)
		{
			pausegui.enabled = true;
			pause();
		}else
		{
			toNormal();
		}
	}
}
