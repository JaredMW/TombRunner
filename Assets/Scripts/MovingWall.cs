using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour {

    // Is the wall currently moving?
    public bool open = true;
    private bool moving = false;

    // Move speed
    public float speed;

    // The top/bottom y coordinates of the door
    public float door_Top;
    public float door_Bottom; // Must be <= door_Top

    // Use this for initialization
    void Start () {
        if (door_Bottom > door_Top)
        {
            door_Bottom = door_Top;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            if (open)
            {
                CloseDoor();
            }

            else
            {
                OpenDoor();
            }
        }

        else
        {
            if (open && transform.position.y != door_Top)
            {
                transform.position = new Vector3(transform.position.x, door_Top, transform.position.z);
            }

            else if (!open && transform.position.y != door_Bottom)
            {
                transform.position = new Vector3(transform.position.x, door_Bottom, transform.position.z);
            }
        }
    }

    // Close the Moving Wall (Move it to door_Bottom)
    void CloseDoor()
    {
        float change = speed * Time.deltaTime;
        transform.Translate(0f, -change, 0f);

        if (transform.position.y <= door_Bottom)
        {
            transform.position = new Vector3(transform.position.x, door_Bottom, transform.position.z);

            moving = false;
            open = false;
        }
    }

    // Open the Moving Wall (Move it to door_Top)
    void OpenDoor()
    {
        float change = speed * Time.deltaTime;
        transform.Translate(0f, change, 0f);

        // Door at its maximum high
        if (transform.position.y >= door_Top)
        {
            transform.position = new Vector3(transform.position.x, door_Top, transform.position.z);

            moving = false;
            open = true;
        }
    }

    /// <summary>
    /// Change the state of the door (open/closed) by moving it.
    /// </summary>
    public void MoveDoor()
    {
        if (!moving)
        {
            moving = true;
        }
    }
}
