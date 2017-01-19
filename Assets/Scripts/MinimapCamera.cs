using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour
{
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("RigidBodyFPSController");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 15,
            transform.position.y, player.transform.position.z - 15);
    }
}
