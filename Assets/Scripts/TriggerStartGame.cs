using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Adjacent direction flags for North, East, South, and West.
/// </summary>
public enum AdjacentDirections
{
    N = 1, E = 2, S = 4, W = 8
}

public enum DirectionRotations
{
    N = 0, E = 90, S = 180, W = 270
}

public class TriggerStartGame : MonoBehaviour {

    // FIELDS
    private bool trigger0 = false, trigger1 = false, trigger2 = false,
        trigger3 = false, goal0, goal1, goal2, goal3, win = false;
    private string score0, score1, score2, score3;
    private Camera minimap;
    private MovingWall chamberWallScript;
    private GameObject startingHallway;
    private float timer;

    // Room tracking
    public GameObject Room_0, Room_1, Room_2, Room_3, Room_4, Room_5, Room_6,
        Room_7, Room_8, Room_9, Room_10, Room_11, Room_12, Room_13, Room_14,
        Room_15, Room_16, Room_17, doorPrefab, wallPrefab, wallDoorPrefab,
        roomTriggerPrefab;
    private GameObject currentRoom;

    // Rooms will be initiated at Start().
    public ArrayList rooms, hallways, oneRooms, twoRooms, threeRooms, fourRooms, // Complete list of available rooms
        northRooms, eastRooms, southRooms, westRooms, neRooms, seRooms, swRooms, nwRooms, nsRooms, ewRooms,
        newRooms, nesRooms, eswRooms, nswRooms, neswRooms, nRooms, eRooms, sRooms, wRooms, // Lists of GameObject rooms with a door on the corresponding side(s)
        adjacentRooms, activeRooms, totalRooms, totalRoomCoords/*, activeRoomCoords*/; // List all the AdjacentDirections that correspond to their room number's adjacent rooms

    // Number of rooms that have been created
    private int numRooms, doors;

    // Rooms entered
    private int discoveredRooms;
    public Text scoreText;

    
    // PROPERTIES
    /// <summary>
    /// Get/set the number of rooms that have been created (in total).
    /// Do not change this value unless a new room has been created.
    /// </summary>
    public int NumRooms
    {
        get { return numRooms; }
        set { numRooms = value; }
    }

    /// <summary>
    /// Get/set the number of rooms that have been entered by the player
    /// </summary>
    public int DiscoveredRooms
    {
        get { return discoveredRooms; }
        set { discoveredRooms = value; }
    }

    /// <summary>
    /// Get/set the number of doors that have been entered by the player
    /// </summary>
    public int Doors
    {
        get { return doors; }
        set { doors = value; }
    }


    // METHODS
    // Use this for initialization
    void Start ()
    {
        minimap = Camera.allCameras[1];
        minimap.gameObject.SetActive(false);

        chamberWallScript = GameObject.Find("Sealing Door").GetComponent<MovingWall>();
        startingHallway = GameObject.Find("Starting Hallway");

        totalRooms = new ArrayList();
        totalRoomCoords = new ArrayList();

        totalRooms.Add(GameObject.Find("Burial Chamber"));
        totalRoomCoords.Add(((GameObject)totalRooms[0]).transform.position);

        numRooms = 0;
        doors = 0;
        discoveredRooms = 0;
        scoreText.text = discoveredRooms.ToString();

        hallways = new ArrayList();
        oneRooms = new ArrayList();
        twoRooms = new ArrayList();
        threeRooms = new ArrayList();
        fourRooms = new ArrayList();

        rooms = new ArrayList();
        rooms.Add(Room_0);
        rooms.Add(Room_1);
        rooms.Add(Room_2);
        rooms.Add(Room_3);
        rooms.Add(Room_4);
        rooms.Add(Room_5);
        rooms.Add(Room_6);
        rooms.Add(Room_7);
        rooms.Add(Room_8);
        rooms.Add(Room_9);
        rooms.Add(Room_10);
        rooms.Add(Room_11);
        rooms.Add(Room_12);
        rooms.Add(Room_13);
        rooms.Add(Room_14);
        rooms.Add(Room_15);
        rooms.Add(Room_16);
        rooms.Add(Room_17);


        // Assign directions associated with each room type, based on their tag
        adjacentRooms = new ArrayList();
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i] != null)
            {
                adjacentRooms.Add(GetRoomDirections((GameObject)rooms[i]));
            }
        }


        // Assign the North, East, South, and West rooms
        northRooms = new ArrayList();
        eastRooms = new ArrayList();
        southRooms = new ArrayList();
        westRooms = new ArrayList();
        nRooms = new ArrayList();
        eRooms = new ArrayList();
        sRooms = new ArrayList();
        wRooms = new ArrayList();
        neRooms = new ArrayList();
        nsRooms = new ArrayList();
        ewRooms = new ArrayList();
        seRooms = new ArrayList();
        swRooms = new ArrayList();
        nwRooms = new ArrayList();
        newRooms = new ArrayList();
        nesRooms = new ArrayList();
        eswRooms = new ArrayList();
        nswRooms = new ArrayList();
        neswRooms = new ArrayList();

        for (int i = 0; i < rooms.Count; i++)
        {
            AdjacentDirections a = (AdjacentDirections)adjacentRooms[i];

            // Classify rooms that are hallways
            if (((GameObject)rooms[i]).GetComponent<HallwayRoom>() != null)
            {
                hallways.Add((GameObject)rooms[i]);
            }

            // Rooms that contain this direction door
            if (HasFlag(a, AdjacentDirections.N))
                northRooms.Add((GameObject)rooms[i]);

            if (HasFlag(a, AdjacentDirections.E))
                eastRooms.Add((GameObject)rooms[i]);

            if (HasFlag(a, AdjacentDirections.S))
                southRooms.Add((GameObject)rooms[i]);

            if (HasFlag(a, AdjacentDirections.W))
                westRooms.Add((GameObject)rooms[i]);


            // Four directions
            if (a == (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.S | AdjacentDirections.W))
            {
                neswRooms.Add((GameObject)rooms[i]);
                fourRooms.Add((GameObject)rooms[i]);
            }


            // Three directions
            else if (a == (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.W))
            {
                newRooms.Add((GameObject)rooms[i]);
                threeRooms.Add((GameObject)rooms[i]);
            }

            else if (a == (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.S))
            {
                nesRooms.Add((GameObject)rooms[i]);
                threeRooms.Add((GameObject)rooms[i]);
            }

            else if (HasFlag(a, (AdjacentDirections.E | AdjacentDirections.S | AdjacentDirections.W)))
            {
                eswRooms.Add((GameObject)rooms[i]);
                threeRooms.Add((GameObject)rooms[i]);
            }

            else if (HasFlag(a, (AdjacentDirections.N | AdjacentDirections.S | AdjacentDirections.W)))
            {
                nswRooms.Add((GameObject)rooms[i]);
                threeRooms.Add((GameObject)rooms[i]);
            }


            // Two directions
            else if (a == (AdjacentDirections.N | AdjacentDirections.E))
            {
                neRooms.Add((GameObject)rooms[i]);
                twoRooms.Add((GameObject)rooms[i]);
            }

            else if (a == (AdjacentDirections.N | AdjacentDirections.S))
            {
                nsRooms.Add((GameObject)rooms[i]);
                twoRooms.Add((GameObject)rooms[i]);
            }

            else if (a == (AdjacentDirections.E | AdjacentDirections.W))
            {
                ewRooms.Add((GameObject)rooms[i]);
                twoRooms.Add((GameObject)rooms[i]);
            }

            else if (a == (AdjacentDirections.S | AdjacentDirections.E))
            {
                seRooms.Add((GameObject)rooms[i]);
                twoRooms.Add((GameObject)rooms[i]);
            }

            else if (a == (AdjacentDirections.S | AdjacentDirections.W))
            {
                swRooms.Add((GameObject)rooms[i]);
                twoRooms.Add((GameObject)rooms[i]);
            }

            else if (a == (AdjacentDirections.N | AdjacentDirections.W))
            {
                nwRooms.Add((GameObject)rooms[i]);
                twoRooms.Add((GameObject)rooms[i]);
            }


            // Single Directions
            else if (a == AdjacentDirections.N)
            {
                nRooms.Add((GameObject)rooms[i]);
                oneRooms.Add((GameObject)rooms[i]);
            }

            else if (a == AdjacentDirections.E)
            {
                eRooms.Add((GameObject)rooms[i]);
                oneRooms.Add((GameObject)rooms[i]);
            }

            else if (a == AdjacentDirections.S)
            {
                sRooms.Add((GameObject)rooms[i]);
                oneRooms.Add((GameObject)rooms[i]);
            }

            else if (a == AdjacentDirections.W)
            {
                wRooms.Add((GameObject)rooms[i]);
                oneRooms.Add((GameObject)rooms[i]);
            }
        }

        score0 = "";
        score1 = "";
        score2 = "";
        score3 = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            Application.LoadLevel("MainMenu");
        }

        if (trigger3)
        {
            scoreText.text =
                "Explored Rooms: " + DiscoveredRooms + score0
                + "\nDiscovered Rooms: " + NumRooms + score1
                + "\nDoors Closed: " + Doors + score2
                + "\nTime: " + (Mathf.RoundToInt(Time.time - timer))
                    + " seconds" + score3;
        }

        else
        {
            scoreText.text =
                "Explored Rooms: " + DiscoveredRooms + " (Goal: 100 rooms)"
                + "\nDiscovered Rooms: " + NumRooms + " (Goal: 150 rooms)"
                + "\nDoors Closed: " + Doors + " (Goal: 10 more than explored rooms)"
                + "\nTime: (Goal: 200 seconds)"
                + "\n[Press esc to restart]"
                + "\n[Press shift to sprint]";
        }

        // Event #3 (Replace the hallway)
        if (Time.time >= timer && trigger0 && !trigger1)
        {
            int i = Random.Range(0, northRooms.Count - 2);
            GameObject roomType = (GameObject)northRooms[i];
            Vector3 v = GameObject.Find("Burial Chamber").transform.position;
            v.z -= 20;

            GameObject room = (GameObject)Instantiate(roomType,
                v,
                Quaternion.identity);

            GameObject.Find("Monster").GetComponent<MonsterScript>().WakeMonster();

            numRooms++;

            timer += 2f;
            trigger1 = true;
        }

        // Event #4 (Open the door)
	    if (Time.time >= timer && trigger0 && trigger1 && !trigger2)
        {
            timer += 5.6f;
            trigger2 = true;
            chamberWallScript.MoveDoor();
        }

        // Event #5 (Activate the minimap)
        if (Time.time >= timer && trigger0 && trigger1 && trigger2 && !trigger3)
        {
            timer = Time.time;
            trigger3 = true;
            minimap.gameObject.SetActive(true);
        }

        if (!goal0 && DiscoveredRooms >= 100)
        {
            goal0 = true;
            score0 = " (Goal completed)";
        }

        if (!goal1 && NumRooms >= 150)
        {
            goal1 = true;
            score1 = " (Goal completed)";
        }

        if (!goal2 && Doors >= DiscoveredRooms + 10)
        {
            goal2 = true;
            score2 = " (Goal completed)";
        }

        if (!goal3 && trigger3 && (Time.time - timer) >= 200)
        {
            goal3 = true;
            score3 = " (Goal completed)";
        }

        if (!win && goal0 && goal1 && goal2 && goal3)
        {
            win = true;
            scoreText.color = new Color(34, 119, 44);
            score0 = "";
            score1 = "";
            score2 = "";
            score3 = "\n(Congrats! All goals met!)";
        }
	}

    // Enact the game-starting trigger
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && trigger0 == false)
        {
            // Event #1 (Close the door; enact timer)
            trigger0 = true;
            timer = Time.time + 5.6f;
            chamberWallScript.MoveDoor();

            // Event #2 (Destroy the hallway)
            Destroy(startingHallway, 5.6f);
        }
    }

    // Get the adjacen directions for this room to analyze, and use for placing
    // more rooms
    public AdjacentDirections GetRoomDirections(GameObject room)
    {
        if (room.tag == "NESW")
        {
            return (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.S | AdjacentDirections.W);
        }

        else if (room.tag == "NES")
        {
            return (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.S);
        }

        else if (room.tag == "NEW")
        {
            return (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.W);
        }

        else if (room.tag == "NSW")
        {
            return (AdjacentDirections.N | AdjacentDirections.S | AdjacentDirections.W);
        }

        else if (room.tag == "ESW")
        {
            return (AdjacentDirections.E | AdjacentDirections.S | AdjacentDirections.W);
        }

        else if (room.tag == "NE")
        {
            return (AdjacentDirections.N | AdjacentDirections.E);
        }

        else if (room.tag == "SE")
        {
            return (AdjacentDirections.S | AdjacentDirections.E);
        }

        else if (room.tag == "SW")
        {
            return (AdjacentDirections.S | AdjacentDirections.W);
        }

        else if (room.tag == "NW")
        {
            return (AdjacentDirections.N | AdjacentDirections.W);
        }

        else if (room.tag == "NS")
        {
            return (AdjacentDirections.N | AdjacentDirections.S);
        }

        else if (room.tag == "EW")
        {
            return (AdjacentDirections.E | AdjacentDirections.W);
        }

        else if (room.tag == "N")
        {
            return AdjacentDirections.N;
        }

        else if (room.tag == "E")
        {
            return AdjacentDirections.E;
        }

        else if (room.tag == "S")
        {
            return AdjacentDirections.S;
        }

        return AdjacentDirections.W;
    }

    /// <summary>
    /// Get the number of doors that this room conatins
    /// </summary>
    /// <param name="room">Door to analyze</param>
    /// <returns>Integer number of doors</returns>
    public int GetNumDoors(GameObject room)
    {
        if (fourRooms.Contains(room))
        {
            return 4;
        }

        else if (threeRooms.Contains(room))
        {
            return 3;
        }

        else if (twoRooms.Contains(room))
        {
            return 2;
        }

        else if (oneRooms.Contains(room))
        {
            return 1;
        }

        return 0;
    }


    /// <summary>
    /// Return true if AdjacentDirection b is contained within AdjacentDirection a
    /// </summary>
    /// <param name="a">AdjacentDirections being analyzed</param>
    /// <param name="b">AdjacentDirection to check if included within AdjacentDirections a</param>
    /// <returns></returns>
    public bool HasFlag(AdjacentDirections a, AdjacentDirections b)
    {
        return (a & b) == b;
    }


    /// <summary>
    /// Get the room type at these coordinates, if it exists. Otherwise, return
    /// null.
    /// </summary>
    /// <param name="coordinates">Coordinates to search</param>
    /// <returns>Room type at this Transform</returns>
    public GameObject GetRoomAt(Vector3 coordinates)
    {
        if (ContainsCoordinates(coordinates))
        {
            return (GameObject)totalRooms[totalRoomCoords.IndexOf(coordinates)];
        }

        return null;
    }

    /// <summary>
    /// Is this set of coordinates already being used?
    /// </summary>
    /// <param name="coordinates">Coordinates to check for</param>
    /// <returns>True if the coordinates already exist</returns>
    public bool ContainsCoordinates(Vector3 coordinates)
    {
        return (totalRoomCoords.Contains(coordinates));
    }

    /// <summary>
    /// Get the name of this AdjacentDirections direction.
    /// </summary>
    /// <param name="direction">
    /// Direction to get the string representation of.
    /// </param>
    /// <returns>String representation of a direction</returns>
    public string GetNameOfDirection(AdjacentDirections direction)
    {
        if (direction == AdjacentDirections.N)
        {
            return "North";
        }

        else if (direction == AdjacentDirections.E)
        {
            return "East";
        }

        else if (direction == AdjacentDirections.S)
        {
            return "South";
        }

        else if (direction == AdjacentDirections.W)
        {
            return "West";
        }

        return "";
    }
}
