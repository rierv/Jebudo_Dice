using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dice;

    void Start()
    {

        if (!Input.gyro.enabled)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        dice.transform.rotation = Input.gyro.attitude;
    }
}
