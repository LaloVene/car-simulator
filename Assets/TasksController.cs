using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TasksController : MonoBehaviour
{

    // Crete an array of 5 strings
    // The strings should be "task 1", "task 2", "task 3", "task 4", "task 5"
    private string[] taskList = new string[5];
    private int currentTask = 0;
    private float startTime;
    public GameObject car;
    //  array of AudioSource
    public AudioSource[] songs;
    private int currentSong = 0;

    // Create the CSV file
    string csvPath = @"C:\Users\laloh\Downloads\logs.csv";
    // StreamWriter writer = new StreamWriter(csvPath, false);
    

    // Start is called before the first frame update
    void Start()
    {
        // Set the array values to the strings
        taskList[0] = "Play/Pause music";
        taskList[1] = "Increase volume";
        taskList[2] = "Lower volume";
        taskList[3] = "Next song";
        taskList[4] = "Previuos song";
        GetComponent<TMPro.TextMeshProUGUI>().text = taskList[0];
        startTime = Time.time;
        songs[0].volume = 0.6f;
        songs[1].volume = 0.6f;
        songs[2].volume = 0.6f;
        songs[currentSong].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && currentTask == 0)
        {
            songs[currentSong].Pause();
            StartCoroutine(TaskList());
            StartCoroutine(PlayAgain());
        }
        if (Input.GetKey(KeyCode.Alpha2) && currentTask == 1)
        {
            songs[currentSong].volume += 0.4f;
            StartCoroutine(TaskList());
            StartCoroutine(ReturnVolume());
        }
        if (Input.GetKey(KeyCode.Alpha3) && currentTask == 2)
        {
            songs[currentSong].volume -= 0.4f;
            StartCoroutine(TaskList());
            StartCoroutine(ReturnVolume());
        }
        if (Input.GetKey(KeyCode.Alpha4) && currentTask == 3)
        {
            songs[currentSong].Stop();
            currentSong = (currentSong + 1) % songs.Length;
            songs[currentSong].Play();
            StartCoroutine(TaskList());
        }
        if (Input.GetKey(KeyCode.Alpha5) && currentTask == 4)
        {
            songs[currentSong].Stop();
            currentSong = currentSong - 1;
            if (currentSong < 0)
            {
                currentSong = songs.Length - 1;
            }
            songs[currentSong].Play();
            StartCoroutine(TaskList());
        }
    }

    IEnumerator TaskList()
    {
        // Print elapsed time
        int currentLap = car.GetComponent<CarController>().currentLap;
        float speed = car.GetComponent<CarController>().speed;
        string log = taskList[currentTask] + "," + (Time.time - startTime) + "," + speed + "," + currentLap;
        Debug.Log(log);
        try  
        {  
            using (StreamWriter writer = new StreamWriter(csvPath, append: true))  
            {  
                // append message to file
                writer.WriteLine(log);
            }
        }  
        catch(Exception exp)  
        {  
            Debug.Log(exp.Message);  
        } 
        currentTask = -1;
        GetComponent<TMPro.TextMeshProUGUI>().text = "Task completed";
        yield return new WaitForSeconds(5);
        currentTask = UnityEngine.Random.Range(0, 5);
        GetComponent<TMPro.TextMeshProUGUI>().text = taskList[currentTask];
        startTime = Time.time;
    }
    IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(2);
        songs[currentSong].Play();
    }
    IEnumerator ReturnVolume()
    {
        yield return new WaitForSeconds(4);
        songs[currentSong].volume = 0.6f;
    }

}
