using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float timeSpeed;

    public Transform sun;
    public Transform moon;

    // Update is called once per frame
    void FixedUpdate()
    {
        sun.RotateAround(sun.transform.position, Vector3.right, timeSpeed * Time.deltaTime);
        moon.RotateAround(moon.transform.position, Vector3.right, timeSpeed * Time.deltaTime);
    }
}
