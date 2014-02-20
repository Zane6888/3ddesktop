using UnityEngine;
using System.Collections;

public class SCStorage{

	private GameObject[][,] shortcuts;
	private Quaternion[] shortcutRotation;
	private Vector3[] startPoints;
	private Vector3[] verticalAdd;
	private Vector3[] horizontalAdd;
	
	private static readonly int X_SPACE = 1;
	private static readonly int Y_SPACE = 1; 
	private static readonly int X_OFFSET = 1;
	private static readonly int Y_OFFSET = 1;
	private static readonly int ROOM_WIDTH = 16;
	private static readonly int ROOM_HEIGHT = 9;
	
	//public GameObject parentRoom;
	
	public int Width{
		get{return shortcuts[0].GetLength(0);}
	}
	public int Height{
		get{return shortcuts[0].GetLength(0);}
	}
	
	public SCStorage(int height,int width,GameObject room)
	{
		shortcuts = new GameObject[4][,];
		shortcutRotation = new Quaternion[4];
		startPoints = new Vector3[4];
		horizontalAdd = new Vector3[4];
		verticalAdd = new Vector3[4];
		//parentRoom = room;
		for(int i = 0; i < 4; i++)
			shortcuts[i] = new GameObject[width,height];
		
		shortcutRotation[0] = Quaternion.AngleAxis(0,Vector3.up);
		shortcutRotation[1] = Quaternion.AngleAxis(90,Vector3.up);
		shortcutRotation[2] = Quaternion.AngleAxis(180,Vector3.up);
		shortcutRotation[3] = Quaternion.AngleAxis(270,Vector3.up);
	
		startPoints[0] = new Vector3(0,Y_OFFSET,X_OFFSET);
		startPoints[1] = new Vector3(X_OFFSET,Y_OFFSET,ROOM_WIDTH);
		startPoints[2] = new Vector3(ROOM_WIDTH,Y_OFFSET,ROOM_WIDTH - X_OFFSET);
		startPoints[3] = new Vector3(ROOM_WIDTH - X_OFFSET,Y_OFFSET,0);
		
		verticalAdd[0] = new Vector3(0,Y_SPACE,0);
		verticalAdd[1] = new Vector3(0,Y_SPACE,0);
		verticalAdd[2] = new Vector3(0,Y_SPACE,0);
		verticalAdd[3] = new Vector3(0,Y_SPACE,0);
		
		horizontalAdd[0] = new Vector3(0,0,X_SPACE);
		horizontalAdd[1] = new Vector3(X_SPACE,0,0);
		horizontalAdd[2] = new Vector3(0,0,-1*X_SPACE);
		horizontalAdd[3] = new Vector3(-1*X_SPACE,0,0);
	}
	
	public void addShortCut(GameObject shortCut,int wall,int x,int y)
	{		
		shortcuts[wall][x,y] = shortCut;
		shortCut.transform.position = (startPoints[wall] + x * horizontalAdd[wall] + y * verticalAdd[wall]);
		//Debug.Log (shortCut.transform.position);
		shortCut.transform.rotation = (shortcutRotation[wall]);
		shortCut.GetComponent<ShortcutScript>().moved();
	}
	
	public void removeShortCut(GameObject shortCut)
	{
		Vector3 loc = findShortcut (shortCut);
		if(loc.x != -1)
			removeShortcutAt((int)loc.z,(int)loc.x,(int)loc.y);
	}
	
	public void removeShortcutAt(int wall,int x, int y)
	{
		shortcuts[wall][x,y] = null;
	}
	
	public void destroyShortcut(GameObject shortCut)
	{
		Vector3 loc = findShortcut (shortCut);
		if(loc.x != -1)
			destroyShortcutAt((int)loc.z,(int)loc.x,(int)loc.y);
	}
	
	public void destroyShortcutAt(int wall,int x, int y)
	{
		GameObject o = shortcuts[wall][x,y];
		shortcuts[wall][x,y] = null;
		UnityEngine.Object.Destroy(o);
	}
	
	public Vector3 findShortcut(GameObject shortCut)
	{
		for(int i = 0;i < shortcuts.Length;i++)
			for(int j = 0;j < shortcuts[i].GetLength(0);j++)
				for(int k = 0;k < shortcuts[i].GetLength(1);k++)
					if(shortcuts[i][j,k] == shortCut)
						return new Vector3(j,k,i);
		return new Vector3(-1,-1,-1);
	}
	
	private void addPlaceHolder(int wall,int x,int y,string colliderTag)
	{
		GameObject placeHolder = GameObject.CreatePrimitive(PrimitiveType.Cube);
		placeHolder.collider.tag = colliderTag;
		placeHolder.transform.localScale /= 2;
		addShortCut(placeHolder,wall,x,y);
	}
	
	public void fillPlaceHolders(string colliderTag)
	{
		for(int i = 0;i < shortcuts.Length;i++)
			for(int j = 0;j < shortcuts[i].GetLength(0);j++)
				for(int k = 0;k < shortcuts[i].GetLength(1);k++)
					if(shortcuts[i][j,k] == null)
						addPlaceHolder(i,j,k,colliderTag);								
	}
	
	public void removePlaceHolders(string colliderTag)
	{
		for(int i = 0;i < shortcuts.Length;i++)
			for(int j = 0;j < shortcuts[i].GetLength(0);j++)
				for(int k = 0;k < shortcuts[i].GetLength(1);k++)
					if(shortcuts[i][j,k] != null && shortcuts[i][j,k].collider.tag == colliderTag)
					{
						destroyShortcutAt(i,j,k);
					}	
	}
}
	
