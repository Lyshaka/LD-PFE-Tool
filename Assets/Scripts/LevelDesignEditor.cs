using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDesignEditor : MonoBehaviour
{
	public Vector2Int dimensions = new Vector2Int(15, 15);

	public TextMeshProUGUI idText;
	public TextMeshProUGUI nameText;
	public TMP_Dropdown typeDropdown;


	Room[,] grid;
	Room selectedRoom;

	bool isMouseInsideScreen
	{
		get
		{
			Vector3 mp = Input.mousePosition;
			return !(mp.x < 0f || mp.y < 0f || mp.x > Screen.width || mp.y > Screen.height);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		grid = new Room[dimensions.x, dimensions.y];
		SetDropdownOptions();
		CreateGrid();
		SetCamera();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseInsideScreen)
		{	
			Vector2Int mousePos = MouseToGridPos();
			if (mousePos.x >= 0 && mousePos.x < dimensions.x && mousePos.y >= 0 && mousePos.y < dimensions.y)
			{
				Debug.Log("Mouse Pos : " + MouseToGridPos());
				if (selectedRoom != null)
				{
					selectedRoom.obj.GetComponentInChildren<SpriteRenderer>().color = Color.white;
				}
				selectedRoom = grid[mousePos.x, mousePos.y];
				selectedRoom.obj.GetComponentInChildren<SpriteRenderer>().color = Color.red;
				idText.text = "#" + selectedRoom.ID;
				nameText.text = selectedRoom.name;
				typeDropdown.value = (int)selectedRoom.type;
			}
		}
	}

	public void SetDropdownOptions()
	{
		typeDropdown.options.Clear();
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.None));
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.Hub));
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.Fight));
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.Altar));
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.Seller));
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.Treasure));
		typeDropdown.options.Add(new TMP_Dropdown.OptionData("" + Room.Type.Boss));
		typeDropdown.value = 0;
	}

	public void SetType()
	{
		selectedRoom.type = (Room.Type)typeDropdown.value;
	}

	public void CreateGrid()
	{
		grid = new Room[dimensions.x, dimensions.y];
		for (int i = 0; i < dimensions.x; i++)
		{
			for (int j = 0; j < dimensions.y; j++)
			{
				grid[i, j] = new Room(i, j, Instantiate(Resources.Load<GameObject>("RoomObject"), new Vector3(i, j, 0f), Quaternion.identity, gameObject.transform), dimensions.x * j + i);
			}
		}
	}

	void SetCamera()
	{
		Camera cam = Camera.main;
		cam.orthographicSize = Mathf.Max(dimensions.y / 1.9f, (dimensions.x / 1.9f) / cam.aspect);
		cam.transform.position = new Vector3((dimensions.x - 1f) / 2f, (dimensions.y - 1f) / 2f, -10f);
	}
	public Vector2Int MouseToGridPos()
	{
		Vector3 cursorPos = MouseToWorldPos();
		return new Vector2Int((int)Mathf.Round(cursorPos.x), (int)Mathf.Round(cursorPos.y));
	}

	public Vector3 MouseToWorldPos()
	{
		return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
	}
}
