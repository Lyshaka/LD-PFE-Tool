
using UnityEngine;

public class Room
{
	public enum Type
	{
		None = 0,
		Hub,
		Fight,
		Altar,
		Seller,
		Treasure,
		Boss
	}

	public string name;
	public int ID;
	public Type type;

	public int x;
	public int y;

	public bool doorNorth;
	public bool doorEast;
	public bool doorSouth;
	public bool doorWest;

	public GameObject obj;

	public Room(int x, int y, GameObject roomObject, int id)
	{
		this.x = x;
		this.y = y;
		obj = roomObject;
		this.ID = id;
		name = "room_" + x + "_" + y;
	}
}
