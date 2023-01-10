using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public int checkpointNumber;
    public GameObject lastCheckpoint;

    // sound
    public AudioClip hitSound;
    public AudioClip checkpointSound;

    public float speed = 0f;
    public int currentLap = 0;
    private float rotation = 0f;
    private float rotationStep = 0.2f;
    private float maxRoation = 1.4f;
    private float maxSpeed = 25f;
    private float acceleration = 0.2f;
    private float deceleration = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is pressing the up arrow key
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (speed < maxSpeed)
            {
                speed += acceleration;
            }
        }
        // If the player is pressing the down arrow key
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (speed > -maxSpeed)
            {
                speed -= acceleration;
            }
        }
        // If the player is pressing the right arrow
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (speed > 0 && rotation < maxRoation)
            {
                rotation += rotationStep;
            } else if (speed < 0 && rotation > -maxRoation)
            {
                rotation -= rotationStep;
            }
        }
        // If the player is pressing the left arrow
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            if (speed > 0 && rotation > -maxRoation)
            {
                rotation -= rotationStep;
            } else if (speed < 0 && rotation < maxRoation)
            {
                rotation += rotationStep;
            }
        }
        // If the player is not pressing any arrow keys
        // If the speed is greater than 0
        if (speed > 0)
        {
            // Decrease the speed
            speed -= deceleration;
        }
        else if (speed < 0)
        {
            speed += deceleration;
        }
        if (rotation > 0)
        {
            // Decrease the rotation
            rotation -= 0.5f * rotationStep;
        }
        else if (rotation < 0)
        {
            // Increase the rotation
            rotation += 0.5f * rotationStep;
        }
        if (rotation >= -0.05f && rotation <= 0.05f)
        {
            rotation = 0f;
        }
        if (speed >= -0.05f && speed <= 0.05f)
        {
            speed = 0f;
        }
        // If the car falls reset to last checkpoint
        if (transform.position.y < -10)
        {
            TeleportBack();
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // Move the car
        transform.Translate(0, 0, speed * Time.deltaTime);
        // Rotate the car
        transform.Rotate(0, rotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pole")
        {
            // Teleport the car behind 5m from the pole
            TeleportBack();
        }
        else if (other.gameObject.tag == "Checkpoint")
        {
            if (other.gameObject.GetComponent<CheckpointTrigger>().checkpointNumber == 0)
            {
                Debug.Log("Start/Finish");
                AudioSource.PlayClipAtPoint(checkpointSound, transform.position);
                if (checkpointNumber != other.gameObject.GetComponent<CheckpointTrigger>().checkpointNumber)
                {
                    currentLap++;
                }
            }
            if (other.gameObject.GetComponent<CheckpointTrigger>().checkpointNumber != checkpointNumber)
            {
                checkpointNumber = other.gameObject.GetComponent<CheckpointTrigger>().checkpointNumber;
                lastCheckpoint = other.gameObject;
            }
        }
    }

    private void TeleportBack() {
        Debug.Log("Checkpoint missed");
        transform.position = lastCheckpoint.transform.position - lastCheckpoint.transform.forward * 10;
        transform.rotation = lastCheckpoint.transform.rotation;
        speed = 0f;
        rotation = 0f;
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }
}
