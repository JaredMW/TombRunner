using UnityEngine;
using System.Collections;

/// <summary>
/// Upon the room's entry, create & close the door that this trigger was placed
/// in front of, and set the RoomGenerator of the room associated with this
/// trigger as activated.
/// </summary>
public class RoomEntryTrigger : MonoBehaviour {

    // FIELDS
    // Scripts
    TriggerStartGame gameScript;
    RoomGenerator roomGenerator;

    // Associated room
    GameObject thisTrigger;


    // METHODS
	// Use this for initialization
	void Start () {
        gameScript = GameObject.Find("GameTrigger").GetComponent<TriggerStartGame>();
        
        thisTrigger = this.gameObject;

        roomGenerator = this.transform.parent.FindChild("Room Generator").GetComponent<RoomGenerator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// When trigger is entered by player, notify the RoomGenerator of this trigger's room
    /// to close this door, and to deactivate the room on the other side of the door.
    /// </summary>
    /// <param name="c">The Player</param>
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && !roomGenerator.roomActive) // When this trigger is activated
        {
            gameScript.Doors += 1;
            roomGenerator.RoomEntered(gameScript.GetRoomDirections(thisTrigger));
        }
    }
}
