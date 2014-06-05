using UnityEngine;
using System.Collections.Generic;
using System.IO;
using _3DRoom;
using System;

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

	public static GameObject shortc;
	public static GameObject scName;
	public static readonly string noicon = "416e09677f119c42bcced015bc3f3b2c";
	
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
		if(wall == -1 || x == -1 || y == -1)
			return;

		shortcuts[wall][x,y] = shortCut;
		shortCut.transform.position = (startPoints[wall] + x * horizontalAdd[wall] + y * verticalAdd[wall]);
		//Debug.Log (shortCut.transform.position);
		shortCut.transform.rotation = (shortcutRotation[wall]);
		ShortcutScript s =  shortCut.GetComponent<ShortcutScript>();
		if(s != null)
			s.moved(wall,x,y);
	}

	public void addShortCut(GameObject shortCut,int wall)
	{
		Vector2 v = getFreeSpot(wall);
		if(v == new Vector2(-1,-1))
		{
			Debug.Log("invalid location: sddShortcut(), wall = "+ wall);
			return;
		}
		addShortCut(shortCut,wall,(int)v.x,(int)v.y);
	}

	public void addShortCut(ShortcutData data)
	{
		GameObject sc = (GameObject)GameObject.Instantiate(shortc);
		data.nameObject = (GameObject)GameObject.Instantiate(scName);
		
		sc.GetComponent<ShortcutScript>().data = data;
		sc.renderer.material = data.getMaterial();

		addShortCut(sc,data.wall,data.x,data.y);
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

	public Vector2 getFreeSpot(int wall)
	{
		if(wall < 0 || wall > 3)
			return new Vector2(-1,-1);

		for(int x = 0;x < shortcuts[wall].GetLength(0);x++)
			for(int y = 0;y < shortcuts[wall].GetLength(1);y++)
				if(shortcuts[wall][x,y] == null)
					return new Vector2(x,y);

		return new Vector2(-1,-1);
	}

	public int countFreeSpots(int wall)
	{
		if(wall < 0 || wall > 3)
			return -1;
		int c = 0;
		for(int x = 0;x < shortcuts[wall].GetLength(0);x++)
			for(int y = 0;y < shortcuts[wall].GetLength(1);y++)
				if(shortcuts[wall][x,y] == null)
					c++;

		return c;
	}

	public bool containsShortcut(int wall, string path)
	{
		for(int i = 0;i < shortcuts[wall].GetLength(0);i++)
			for(int j = 0;j < shortcuts[wall].GetLength(1);j++)
				if(shortcuts[wall][i,j] != null && shortcuts[wall][i,j].GetComponent<ShortcutScript>() != null &&
					shortcuts[wall][i,j].GetComponent<ShortcutScript>().data.path == path)
					return true;
		return false;
	}

	public void loadFromDir(int wall, string dir)
	{
		ReadConfig.setLocked(true);

		List<string> content = new List<string>(Directory.GetFiles(dir));
		List<string> files = new List<string>();
		List<ShortcutData> config = ReadConfig.getShortcutData();

		foreach(string file in content)
			if(new FileInfo(file).Extension == ".lnk")
			{
				try
				{
					files.Add(FileHelper.GetShortcutTargetFile(file));
				}
				catch(Exception)
				{
					Debug.Log("file to big: " + file); 
				}
			}
			else
				files.Add(file);
		
		foreach(string file in files)
		{
			if(containsShortcut(wall,file))
			{
				Debug.Log("already contained");
				continue;
			}
			if(file == "")
			{
				Debug.Log("empty file");
				continue;
			}


			ShortcutData data = config.Find(d => d.path == file);
			if(data == null)
				data = new ShortcutData(new FileInfo(file).Name,file);

			if(!data.hasTexture())
			{	try
				{
					ByteArrayImg img = FileHelper.GetFileIcon(file);
					if(img.GetHash() != noicon)
					{
						Texture2D tex = new Texture2D(img.width, img.height);
						tex.LoadImage(img.bytes);
						data.setIcon(tex);
					}
				}
				catch (Exception){}
			}

			if(data.wall == -1)
			{
				Vector2 v = getFreeSpot(wall);
				data.setPosition(wall,(int)v.x,(int)v.y,false);
			}

			addShortCut(data);

		}

		ReadConfig.setLocked(false);

	}

	public void addAll(IEnumerable<ShortcutData> data)
	{
		data = new List<ShortcutData>(data);
		foreach(ShortcutData d in data)
			addShortCut(d);
	}
}
	
