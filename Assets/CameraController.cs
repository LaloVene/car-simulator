using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object

    // Start is called before the first frame update
    void Start () 
    {

    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set camera always behind the player
        transform.position = player.transform.position - player.transform.forward * 10 + Vector3.up * 5;
        transform.LookAt(player.transform.position + player.transform.forward * 30);
    }
}
