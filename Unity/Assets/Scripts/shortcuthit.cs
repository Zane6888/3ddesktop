using UnityEngine;
using System.Collections;

public class shortcuthit : MonoBehaviour {
	
	public string target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void run()
	{
		System.Diagnostics.Process.Start(target);
	}
}
