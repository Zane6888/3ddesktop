using UnityEngine;
using System.Collections;

public class ShortcutScript : MonoBehaviour 
{
	public ShortcutData data;

	public void moved(int wall, int x, int y)
	{
		data.nameObject.transform.position = this.gameObject.transform.position;
		data.nameObject.transform.rotation = this.gameObject.transform.rotation * Quaternion.AngleAxis(-90,Vector3.up);
		data.setPosition(wall, x, y);
	}
}
