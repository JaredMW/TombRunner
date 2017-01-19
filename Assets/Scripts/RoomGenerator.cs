using UnityEngine;
using System.Collections;

public class RoomGenerator : MonoBehaviour {

    // FIELDS
    public bool triggered, discovered;

    // Room tracking data
    GameObject thisRoom;
    Vector3 thisRoomCoords;
    AdjacentDirections thisRoomDirections;

    // Scripts
    TriggerStartGame gameScript;
    
    public bool roomActive; // True if the player is currently in this room


    // PROPERTIES
    private Vector3 NorthCoordinates
    {
        get
        {
            Vector3 north = thisRoomCoords;
            north.z += 20;
            return north;
        }
    }

    private Vector3 EastCoordinates
    {
        get
        {
            Vector3 east = thisRoomCoords;
            east.x += 20;
            return east;
        }
    }

    private Vector3 SouthCoordinates
    {
        get
        {
            Vector3 south = thisRoomCoords;
            south.z -= 20;
            return south;
        }
    }

    private Vector3 WestCoordinates
    {
        get
        {
            Vector3 west = thisRoomCoords;
            west.x -= 20;
            return west;
        }
    }

    // METHODS
    // Use this for initialization
    void Start () {
        triggered = false;
        discovered = false;
        roomActive = false;

        gameScript = GameObject.Find("GameTrigger").GetComponent<TriggerStartGame>();

        thisRoom = this.transform.parent.gameObject;

        thisRoomCoords = thisRoom.transform.position;
        thisRoomDirections = gameScript.GetRoomDirections(thisRoom);
        thisRoom.name = "r" + gameScript.NumRooms;
        
        gameScript.totalRooms.Add(thisRoom);
        gameScript.totalRoomCoords.Add(thisRoomCoords);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// Select a room model to create out of the available rooms.
    /// Selects a different room every time, unless there are no other options
    /// left available.
    /// </summary>
    /// <param name="direction">
    /// The direction(s) that the room being selected must have. If only one
    /// direction is specified, then any room that contains this direction may
    /// be selected.
    /// </param>
    /// <param name="canBeHallway">
    /// Can the selected room be a hallway? (True by default.)
    /// </param>
    /// <returns>A randomly selected Room that has doors matching to those
    /// specified in <paramref name="direction"/>.</returns>
    private GameObject SelectRoom(AdjacentDirections directions, bool canBeHallway = true)
    {
        if (directions == (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.S | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.neswRooms.Count);

            if (gameScript.neswRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.neswRooms[i]))
                {
                    i = Random.Range(0, gameScript.neswRooms.Count);
                }
            }

            else if (gameScript.neswRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.neswRooms[i]))
                {
                    i = Random.Range(0, gameScript.neswRooms.Count);
                }
            }

            return (GameObject)gameScript.neswRooms[i];
        }


        else if (directions == (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.newRooms.Count);
            
            if (gameScript.newRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.newRooms[i]))
                {
                    i = Random.Range(0, gameScript.newRooms.Count);
                }
            }

            else if (gameScript.newRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.newRooms[i]))
                {
                    i = Random.Range(0, gameScript.newRooms.Count);
                }
            }

            return (GameObject)gameScript.newRooms[i];
        }


        else if (directions == (AdjacentDirections.N | AdjacentDirections.E | AdjacentDirections.S))
        {
            int i = Random.Range(0, gameScript.nesRooms.Count);
            
            if (gameScript.nesRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.nesRooms[i]))
                {
                    i = Random.Range(0, gameScript.nesRooms.Count);
                }
            }

            else if (gameScript.newRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.newRooms[i]))
                {
                    i = Random.Range(0, gameScript.newRooms.Count);
                }
            }

            return (GameObject)gameScript.nesRooms[i];
        }


        else if (directions == (AdjacentDirections.E | AdjacentDirections.S | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.eswRooms.Count);

            if (gameScript.eswRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.eswRooms[i]))
                {
                    i = Random.Range(0, gameScript.eswRooms.Count);
                }
            }

            else if (gameScript.eswRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.eswRooms[i]))
                {
                    i = Random.Range(0, gameScript.eswRooms.Count);
                }
            }

            return (GameObject)gameScript.eswRooms[i];
        }


        else if (directions == (AdjacentDirections.N | AdjacentDirections.S | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.nswRooms.Count);

            if (gameScript.nswRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.nswRooms[i]))
                {
                    i = Random.Range(0, gameScript.nswRooms.Count);
                }
            }

            else if (gameScript.nswRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.nswRooms[i]))
                {
                    i = Random.Range(0, gameScript.nswRooms.Count);
                }
            }

            return (GameObject)gameScript.nswRooms[i];
        }


        else if (directions == (AdjacentDirections.N | AdjacentDirections.E))
        {
            int i = Random.Range(0, gameScript.neRooms.Count);

            if (gameScript.neRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.neRooms[i]))
                {
                    i = Random.Range(0, gameScript.neRooms.Count);
                }
            }

            else if (gameScript.neRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.neRooms[i]))
                {
                    i = Random.Range(0, gameScript.neRooms.Count);
                }
            }

            return (GameObject)gameScript.neRooms[i];
        }


        else if (directions == (AdjacentDirections.N | AdjacentDirections.S))
        {
            int i = Random.Range(0, gameScript.nsRooms.Count);

            if (gameScript.nsRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.nsRooms[i]))
                {
                    i = Random.Range(0, gameScript.nsRooms.Count);
                }
            }

            else if (gameScript.nsRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.nsRooms[i]))
                {
                    i = Random.Range(0, gameScript.nsRooms.Count);
                }
            }

            return (GameObject)gameScript.nsRooms[i];
        }


        else if (directions == (AdjacentDirections.E | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.ewRooms.Count);

            if (gameScript.ewRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.ewRooms[i]))
                {
                    i = Random.Range(0, gameScript.ewRooms.Count);
                }
            }

            else if (gameScript.ewRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.ewRooms[i]))
                {
                    i = Random.Range(0, gameScript.ewRooms.Count);
                }
            }

            return (GameObject)gameScript.ewRooms[i];
        }


        else if (directions == (AdjacentDirections.S | AdjacentDirections.E))
        {
            int i = Random.Range(0, gameScript.seRooms.Count);

            if (gameScript.seRooms.Count > 1)
            {
                while (thisRoom.Equals((GameObject)gameScript.seRooms[i]))
                {
                    i = Random.Range(0, gameScript.seRooms.Count);
                }
            }

            return (GameObject)gameScript.seRooms[i];
        }


        else if (directions == (AdjacentDirections.S | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.swRooms.Count);

            if (gameScript.swRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.swRooms[i]))
                {
                    i = Random.Range(0, gameScript.swRooms.Count);
                }
            }

            else if (gameScript.swRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.swRooms[i]))
                {
                    i = Random.Range(0, gameScript.swRooms.Count);
                }
            }

            return (GameObject)gameScript.seRooms[i];
        }


        else if (directions == (AdjacentDirections.N | AdjacentDirections.W))
        {
            int i = Random.Range(0, gameScript.nwRooms.Count);

            if (gameScript.nwRooms.Count > 1 && canBeHallway)
            {
                while (thisRoom.Equals((GameObject)gameScript.nwRooms[i]))
                {
                    i = Random.Range(0, gameScript.nwRooms.Count);
                }
            }

            else if (gameScript.nwRooms.Count > 1)
            {
                while (gameScript.hallways.Contains((GameObject)gameScript.nwRooms[i]))
                {
                    i = Random.Range(0, gameScript.nwRooms.Count);
                }
            }

            return (GameObject)gameScript.nwRooms[i];
        }


        else if (directions == AdjacentDirections.N)
        {
            int i = Random.Range(0, gameScript.northRooms.Count);

            if (gameScript.northRooms.Count > 1)
            {
                if (canBeHallway)
                {
                    while (thisRoom.Equals((GameObject)gameScript.northRooms[i]) || gameScript.nRooms.Contains((GameObject)gameScript.northRooms[i]) && !triggered)
                    {
                        i = Random.Range(0, gameScript.northRooms.Count);
                    }
                }
                
                else
                {
                    while (gameScript.hallways.Contains((GameObject)gameScript.northRooms[i]) || ((gameScript.nRooms.Contains((GameObject)gameScript.northRooms[i]) && !triggered)))
                    {
                        i = Random.Range(0, gameScript.northRooms.Count);
                    }
                }
            }

            triggered = true;
            return (GameObject)gameScript.northRooms[i];
        }


        else if (directions == AdjacentDirections.E)
        {
            int i = Random.Range(0, gameScript.eastRooms.Count);

            if (gameScript.eastRooms.Count > 1)
            {
                if (canBeHallway)
                {
                    while (thisRoom.Equals((GameObject)gameScript.eastRooms[i]) || (gameScript.eRooms.Contains((GameObject)gameScript.eastRooms[i]) && !triggered))
                    {
                        i = Random.Range(0, gameScript.eastRooms.Count);
                    }
                }

                else
                {
                    while (gameScript.hallways.Contains((GameObject)gameScript.eastRooms[i]) || (gameScript.eRooms.Contains((GameObject)gameScript.eastRooms[i]) && !triggered))
                    {
                        i = Random.Range(0, gameScript.eastRooms.Count);
                    }
                }
            }

            triggered = true;
            return (GameObject)gameScript.eastRooms[i];
        }


        else if (directions == AdjacentDirections.S)
        {
            int i = Random.Range(0, gameScript.southRooms.Count);

            if (gameScript.southRooms.Count > 1)
            {
                if (canBeHallway)
                {
                    while (thisRoom.Equals((GameObject)gameScript.southRooms[i]) || (gameScript.sRooms.Contains((GameObject)gameScript.southRooms[i]) && !triggered))
                    {
                        i = Random.Range(0, gameScript.southRooms.Count);
                    }
                }

                else
                {
                    while (gameScript.hallways.Contains((GameObject)gameScript.southRooms[i]) || (gameScript.sRooms.Contains((GameObject)gameScript.southRooms[i]) && !triggered))
                    {
                        i = Random.Range(0, gameScript.southRooms.Count);
                    }
                }
            }

            triggered = true;
            return (GameObject)gameScript.southRooms[i];
        }


        else if (directions == AdjacentDirections.W)
        {
            int i = Random.Range(0, gameScript.westRooms.Count);

            if (gameScript.westRooms.Count > 1)
            {
                if (canBeHallway)
                {
                    while (thisRoom.Equals((GameObject)gameScript.westRooms[i]) || (gameScript.wRooms.Contains((GameObject)gameScript.westRooms[i]) && !triggered))
                    {
                        i = Random.Range(0, gameScript.westRooms.Count);
                    }
                }

                else
                {
                    while (gameScript.hallways.Contains((GameObject)gameScript.westRooms[i]) || (gameScript.wRooms.Contains((GameObject)gameScript.westRooms[i]) && !triggered))
                    {
                        i = Random.Range(0, gameScript.westRooms.Count);
                    }
                }
            }

            triggered = true;
            return (GameObject)gameScript.westRooms[i];
        }

        return null;
    }


    /// <summary>
    /// Randomly generate the rooms adjacent to this one, based upon the walls
    /// that have doors within the current room & the surrounding availability.
    /// </summary>
    public void GenerateRooms()
    {
        ArrayList directions = new ArrayList();
        directions.Add(AdjacentDirections.N);
        directions.Add(AdjacentDirections.E);
        directions.Add(AdjacentDirections.S);
        directions.Add(AdjacentDirections.W);

        for (int i = 0; i < directions.Count; i++)
        {
            AdjacentDirections thisDirection = (AdjacentDirections)directions[i];
            AdjacentDirections oppositeDirection = GetOppositeDirection(thisDirection);
            AdjacentDirections left = GetLeft(thisDirection);
            AdjacentDirections right = GetRight(thisDirection);
            Vector3 coordinates = GetDirectionCoords(thisDirection);

            if (gameScript.HasFlag(thisRoomDirections, thisDirection))
            {
                if (!gameScript.ContainsCoordinates(coordinates))
                {
                    ArrayList requiredDirections = GetRequiredDirections(coordinates);

                    ArrayList invalidDirections = GetInvalidDirections(coordinates);
                    bool canBeHallway = true;

                    if (invalidDirections.Contains(thisDirection) || requiredDirections.Contains(left) || requiredDirections.Contains(right))
                    {
                        canBeHallway = false;
                    }

                    GameObject newRoom = (GameObject)Instantiate(
                        SelectRoom(oppositeDirection, canBeHallway),
                        coordinates,
                        Quaternion.identity);
                    gameScript.NumRooms++;

                    // Change invalid normal walls to door walls
                    for (int c = 0; c < requiredDirections.Count; c++)
                    {
                        if (!gameScript.HasFlag(gameScript.GetRoomDirections(newRoom), (AdjacentDirections)requiredDirections[c]))
                        {
                            Vector3 location = newRoom.transform.position;
                            location.y = 3.75f;

                            Vector3 triggerLocation = newRoom.transform.position;
                            triggerLocation.y = 3.75f;

                            string directionName = gameScript.GetNameOfDirection((AdjacentDirections)requiredDirections[c]);

                            if ((AdjacentDirections)requiredDirections[c] == AdjacentDirections.N)
                            {
                                location.x -= 6;
                                location.z += 10;
                                triggerLocation.z += 7;
                            }

                            else if ((AdjacentDirections)requiredDirections[c] == AdjacentDirections.E)
                            {
                                location.x += 10;
                                location.z += 6;
                                triggerLocation.x += 7;
                            }

                            else if ((AdjacentDirections)requiredDirections[c] == AdjacentDirections.S)
                            {
                                location.x += 6;
                                location.z -= 10;
                                triggerLocation.z -= 7;
                            }

                            else if ((AdjacentDirections)requiredDirections[c] == AdjacentDirections.W)
                            {
                                location.x -= 10;
                                location.z -= 6;
                                triggerLocation.x -= 7;
                            }

                            GameObject doorWall = (GameObject)Instantiate(
                                gameScript.wallDoorPrefab,
                                location,
                                Quaternion.Euler(0, GetDirectionRotation((AdjacentDirections)requiredDirections[c]), 0));

                            doorWall.name = directionName + " Wall (1 Door)";
                            doorWall.transform.SetParent(newRoom.transform);

                            Destroy(newRoom.transform.Find(directionName + " Wall").gameObject);

                            GameObject trigger = (GameObject)Instantiate(
                                gameScript.roomTriggerPrefab,
                                triggerLocation,
                                Quaternion.Euler(0, GetDirectionRotation((AdjacentDirections)requiredDirections[c]), 0));

                            trigger.tag = ((AdjacentDirections)requiredDirections[c]).ToString();
                            trigger.name = directionName + " Trigger";
                            trigger.transform.SetParent(newRoom.transform);
                        }
                    }


                    // Change invalid door walls to normal walls
                    for (int c = 0; c < invalidDirections.Count; c++)
                    {
                        if (gameScript.HasFlag(gameScript.GetRoomDirections(newRoom), (AdjacentDirections)invalidDirections[c]))
                        {
                            Vector3 location = newRoom.transform.position;
                            location.y = 3.75f;
                            string directionName = gameScript.GetNameOfDirection((AdjacentDirections)invalidDirections[c]);

                            if ((AdjacentDirections)invalidDirections[c] == AdjacentDirections.N)
                            {
                                location.z += 10;
                            }

                            else if ((AdjacentDirections)invalidDirections[c] == AdjacentDirections.E)
                            {
                                location.x += 10;
                            }

                            else if ((AdjacentDirections)invalidDirections[c] == AdjacentDirections.S)
                            {
                                location.z -= 10;
                            }

                            else if ((AdjacentDirections)invalidDirections[c] == AdjacentDirections.W)
                            {
                                location.x -= 10;
                            }

                            GameObject wall = (GameObject)Instantiate(
                                gameScript.wallPrefab,
                                location,
                                Quaternion.Euler(0, GetDirectionRotation((AdjacentDirections)invalidDirections[c]), 0));

                            wall.name = directionName + " Wall";
                            wall.transform.SetParent(newRoom.transform);

                            Destroy(newRoom.transform.Find(directionName + " Wall (1 Door)").gameObject);
                            Destroy(newRoom.transform.Find(directionName + " Trigger").gameObject);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Get the North, South, East, or West adjacent room coordinates
    /// </summary>
    /// <param name="direction">Direction to get coordinates in</param>
    /// <returns>Vector3 coordinates</returns>
    public Vector3 GetDirectionCoords(AdjacentDirections direction)
    {
        if (direction == AdjacentDirections.N)
        {
            return NorthCoordinates;
        }

        else if (direction == AdjacentDirections.E)
        {
            return EastCoordinates;
        }

        else if (direction == AdjacentDirections.S)
        {
            return SouthCoordinates;
        }

        else if (direction == AdjacentDirections.W)
        {
            return WestCoordinates;
        }

        return default(Vector3);
    }


    /// <summary>
    /// Get the rotation angle needed for an object on a side of the room.
    /// </summary>
    /// <param name="direction">Direction angle being searched for</param>
    /// <returns>Float of an angle</returns>
    public float GetDirectionRotation(AdjacentDirections direction)
    {
        if (direction == AdjacentDirections.N)
        {
            return 0;
        }

        else if (direction == AdjacentDirections.E)
        {
            return 90;
        }

        else if (direction == AdjacentDirections.S)
        {
            return 180;
        }

        else if (direction == AdjacentDirections.W)
        {
            return 270;
        }

        return 0;
    }


    /// <summary>
    /// Get the direction opposite of this one
    /// </summary>
    /// <param name="direction">Normal direction</param>
    /// <returns>The opposite direction</returns>
    public AdjacentDirections GetOppositeDirection(AdjacentDirections direction)
    {
        ArrayList directions = new ArrayList();
        directions.Add(AdjacentDirections.N);
        directions.Add(AdjacentDirections.E);
        directions.Add(AdjacentDirections.S);
        directions.Add(AdjacentDirections.W);

        return (AdjacentDirections)directions[(directions.IndexOf(direction) + (directions.Count / 2)) % directions.Count];
    }

    /// <summary>
    /// Get the direction left of this one
    /// </summary>
    /// <param name="direction">Normal direction</param>
    /// <returns>The left direction</returns>
    public AdjacentDirections GetLeft(AdjacentDirections direction)
    {
        ArrayList directions = new ArrayList();
        directions.Add(AdjacentDirections.N);
        directions.Add(AdjacentDirections.E);
        directions.Add(AdjacentDirections.S);
        directions.Add(AdjacentDirections.W);

        return (AdjacentDirections)directions[(directions.IndexOf(direction) + (directions.Count - 1)) % directions.Count];
    }

    /// <summary>
    /// Get the direction right of this one
    /// </summary>
    /// <param name="direction">Normal direction</param>
    /// <returns>The right direction</returns>
    public AdjacentDirections GetRight(AdjacentDirections direction)
    {
        ArrayList directions = new ArrayList();
        directions.Add(AdjacentDirections.N);
        directions.Add(AdjacentDirections.E);
        directions.Add(AdjacentDirections.S);
        directions.Add(AdjacentDirections.W);

        return (AdjacentDirections)directions[(directions.IndexOf(direction) + 1) % directions.Count];
    }


    /// <summary>
    /// Hide this room if it is out of range
    /// </summary>
    public void HideRoom()
    {
        foreach (GameObject c in GameObject.Find(thisRoom.name).GetComponents<GameObject>())
        {
            c.SetActive(false);
        }
    }
    

    /// <summary>
    /// Notified by a RoomEntryTrigger to signal that a door should be created
    /// and closed
    /// </summary>
    /// <param name="enteredFrom">Direction entered from</param>
    public void RoomEntered(AdjacentDirections enteredFrom)
    {
        roomActive = true;

        if (enteredFrom == AdjacentDirections.N)
        {
            if (gameScript.GetRoomAt(NorthCoordinates).name != "Burial Chamber")
            {
                gameScript.GetRoomAt(NorthCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().CloseDoor(GetOppositeDirection(enteredFrom));
                gameScript.GetRoomAt(NorthCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().roomActive = false;
            }

            CloseDoor(enteredFrom);
        }

        else if (enteredFrom == AdjacentDirections.E)
        {
            gameScript.GetRoomAt(EastCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().CloseDoor(GetOppositeDirection(enteredFrom));
            CloseDoor(enteredFrom);

            gameScript.GetRoomAt(EastCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().roomActive = false;
        }

        else if (enteredFrom == AdjacentDirections.S)
        {
            gameScript.GetRoomAt(SouthCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().CloseDoor(GetOppositeDirection(enteredFrom));
            CloseDoor(enteredFrom);

            gameScript.GetRoomAt(SouthCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().roomActive = false;
        }

        else if (enteredFrom == AdjacentDirections.W)
        {
            gameScript.GetRoomAt(WestCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().CloseDoor(GetOppositeDirection(enteredFrom));
            CloseDoor(enteredFrom);

            gameScript.GetRoomAt(WestCoordinates).transform.Find("Room Generator").GetComponent<RoomGenerator>().roomActive = false;
        }

        // If entering the room for the first time
        if (!discovered)
        {
            discovered = true;
            gameScript.DiscoveredRooms += 1;

            GenerateRooms();
        }

        UpdateLights(enteredFrom);
    }

    /// <summary>
    /// Instantiate & close a door on this side of the room.
    /// </summary>
    /// <param name="side">Side of the room to create & close a door on (cannot
    /// be a conjunction of multiple directions).</param>
    /// <param name="isOpen">Whether the door being created is created open
    /// (true by default).</param>
    public void CloseDoor(AdjacentDirections side, bool isOpen = true)
    {
        Vector3 location = new Vector3(thisRoomCoords.x,
            thisRoomCoords.y + 8.4375f, thisRoomCoords.z);
        float rotation = GetDirectionRotation(side);
            
        if (side == AdjacentDirections.N)
        {
            location.z += 10.111f;
        }

        else if (side == AdjacentDirections.E)
        {
            location.x += 10.111f;
        }

        else if (side == AdjacentDirections.S)
        {
            location.z -= 10.111f;
        }

        else if (side == AdjacentDirections.W)
        {
            location.x -= 10.111f;
        }

        GameObject door = (GameObject)Instantiate(gameScript.doorPrefab,
            location, Quaternion.Euler(0, rotation, 0));
        door.transform.SetParent(thisRoom.transform);

        if (!isOpen)
        {
            door.GetComponent<MovingWall>().open = false;
        }

        else
        {
            door.GetComponent<MovingWall>().MoveDoor();
        }
    }

    /// <summary>
    /// Get the valid directions for a room to be placed at <paramref name="location"/>.
    /// </summary>
    /// <param name="location">Must not be the same as this room's coordinates.</param>
    /// <returns>ArrayList of AdjacentDirections</returns>
    private ArrayList GetRequiredDirections(Vector3 location)
    {
        Vector3 assist = new Vector3();
        ArrayList requiredDirections = new ArrayList();

        // north
        assist.Set(location.x, location.y, location.z + 20);
        if (gameScript.ContainsCoordinates(assist) && gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.S))
        {
            requiredDirections.Add(AdjacentDirections.N);
        }

        // east
        assist.Set(location.x + 20, location.y, location.z);
        if (gameScript.ContainsCoordinates(assist) && gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.W))
        {
            requiredDirections.Add(AdjacentDirections.E);
        }

        // south
        assist.Set(location.x, location.y, location.z - 20);
        if (gameScript.ContainsCoordinates(assist) && gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.N))
        {
            requiredDirections.Add(AdjacentDirections.S);
        }

        // west
        assist.Set(location.x - 20, location.y, location.z);
        if (gameScript.ContainsCoordinates(assist) && gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.E))
        {
            requiredDirections.Add(AdjacentDirections.W);
        }

        return requiredDirections;
    }

    /// <summary>
    /// Get the invalid directions for a room to be placed at <paramref name="location"/>.
    /// </summary>
    /// <param name="location">Must not be the same as this room's coordinates.</param>
    /// <returns>ArrayList of AdjacentDirections</returns>
    private ArrayList GetInvalidDirections(Vector3 location)
    {
        Vector3 assist = new Vector3();
        ArrayList invalidDirections = new ArrayList();

        // north
        assist.Set(location.x, location.y, location.z + 20);
        if (gameScript.ContainsCoordinates(assist) && !gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.S))
        {
            invalidDirections.Add(AdjacentDirections.N);
        }

        // east
        assist.Set(location.x + 20, location.y, location.z);
        if (gameScript.ContainsCoordinates(assist) && !gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.W))
        {
            invalidDirections.Add(AdjacentDirections.E);
        }

        // south
        assist.Set(location.x, location.y, location.z - 20);
        if (gameScript.ContainsCoordinates(assist) && !gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.N))
        {
            invalidDirections.Add(AdjacentDirections.S);
        }

        // west
        assist.Set(location.x - 20, location.y, location.z);
        if (gameScript.ContainsCoordinates(assist) && !gameScript.HasFlag(gameScript.GetRoomDirections(gameScript.GetRoomAt(assist)), AdjacentDirections.E))
        {
            invalidDirections.Add(AdjacentDirections.W);
        }

        return invalidDirections;
    }

    /// <summary>
    /// Shut off lights of rooms not tangent to the active room.
    /// </summary>
    /// <param name="enteredFrom">Direction entered from</param>
    private void UpdateLights(AdjacentDirections enteredFrom)
    {
        Vector3 enteredFromCoords = GetDirectionCoords(enteredFrom);
        Vector3 lightsOnCoords = GetDirectionCoords(GetOppositeDirection(enteredFrom));

        if (enteredFrom == AdjacentDirections.N)
        {
            enteredFromCoords.z += 20;
        }

        else if (enteredFrom == AdjacentDirections.E)
        {
            enteredFromCoords.x += 20;
        }

        else if (enteredFrom == AdjacentDirections.S)
        {
            enteredFromCoords.z -= 20;
        }

        else if (enteredFrom == AdjacentDirections.W)
        {
            enteredFromCoords.x -= 20;
        }
        
        // Shut off lights
        for (int i = -20; i <= 20; i += 20)
        {
            if (enteredFrom == AdjacentDirections.N || enteredFrom == AdjacentDirections.S)
            {
                Vector3 assist = new Vector3(enteredFromCoords.x + i, enteredFromCoords.y, enteredFromCoords.z);

                if (gameScript.ContainsCoordinates(assist))
                {
                    gameScript.GetRoomAt(assist).transform.Find("Room Light").GetComponent<Light>().gameObject.SetActive(false);
                }
            }
            
            else if (enteredFrom == AdjacentDirections.E || enteredFrom == AdjacentDirections.W)
            {
                Vector3 assist = new Vector3(enteredFromCoords.x, enteredFromCoords.y, enteredFromCoords.z + i);

                if (gameScript.ContainsCoordinates(assist))
                {
                    gameScript.GetRoomAt(assist).transform.Find("Room Light").GetComponent<Light>().gameObject.SetActive(false);
                }
            }
        }

        // Turn on lights
        for (int i = -20; i <= 20; i += 20)
        {
            if (enteredFrom == AdjacentDirections.N || enteredFrom == AdjacentDirections.S)
            {
                Vector3 assist = new Vector3(enteredFromCoords.x + i, lightsOnCoords.y, lightsOnCoords.z);

                if (gameScript.ContainsCoordinates(assist))
                {
                    gameScript.GetRoomAt(assist).transform.Find("Room Light").GetComponent<Light>().gameObject.SetActive(true);
                }
            }

            else if (enteredFrom == AdjacentDirections.E || enteredFrom == AdjacentDirections.W)
            {
                Vector3 assist = new Vector3(lightsOnCoords.x, lightsOnCoords.y, lightsOnCoords.z + i);

                if (gameScript.ContainsCoordinates(assist))
                {
                    gameScript.GetRoomAt(assist).transform.Find("Room Light").GetComponent<Light>().gameObject.SetActive(true);
                }
            }
        }
    }
}