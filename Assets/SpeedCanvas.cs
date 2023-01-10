using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCanvas : MonoBehaviour
{

    public GameObject car;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the speed of the car
        
    }

    void FixedUpdate()
    {
        float speed = Mathf.Abs(car.GetComponent<CarController>().speed);
        GetComponent<TMPro.TextMeshProUGUI>().text = "Speed: " + speed.ToString("F2") + " km/h";
    }
}
