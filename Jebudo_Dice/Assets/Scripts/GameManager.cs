﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dice;
    List<GameObject> diceList = new List<GameObject>();
    List<Rigidbody> rbList = new List<Rigidbody>();
    bool selected = false;
    Dice selectedDiceforPainting;
    float time=0;
    bool editing = false;
    public GameObject light;
    List<Dice> paintingDices = new List<Dice>();
    void Start()
    {
        
        if (!Input.gyro.enabled)
        {
            Input.gyro.enabled = true;
        }
        for (int i = 0; i < 5; i++)
        {
            SpawnDice();
        }
    }

    void Update()
    {
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))&&!selected)
        {
            Ray ray;
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == 0) ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            else ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && notAButton(hit.point))
                {

                    GameObject touchedObject = hit.transform.gameObject;
                    if (touchedObject.name == "Terrain" && paintingDices.Count==0)
                    {

                        foreach (Rigidbody dice in rbList)
                        {
                            dice.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(4, 5), Random.Range(-1.5f, 1.5f)) * 500);
                            dice.AddTorque((transform.up * Random.Range(-2, 2) + transform.forward * Random.Range(-2, 2) + transform.right * Random.Range(-2, 2)) * 500);
                        }
                    }
                    else if (diceList.Contains(touchedObject))
                    {
                        selected = true;
                        selectedDiceforPainting = touchedObject.GetComponent<Dice>();
                        
                    }
                }
            }
        }
        else if ((Input.touchCount == 1 && !(Input.GetTouch(0).phase == TouchPhase.Ended) || !Input.GetMouseButtonUp(0)) && selected)
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                selectedDiceforPainting.EditColor();
                if (paintingDices.Contains(selectedDiceforPainting)) paintingDices.Remove(selectedDiceforPainting);
                else paintingDices.Add(selectedDiceforPainting);
                time = 0;
                editing = true;
            }
        }
        else if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) && selected)
        {
            time = 0;
            selected = false;
            if (!editing)
            {
                Rigidbody tmprb = selectedDiceforPainting.gameObject.GetComponent<Rigidbody>();
                tmprb.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(4, 5), Random.Range(-1.5f, 1.5f)) * 500);
                tmprb.AddTorque((transform.up * Random.Range(-2, 2) + transform.forward * Random.Range(-2, 2) + transform.right * Random.Range(-2, 2)) * 500);
            }
            editing = false;
        }
        if (Input.gyro.userAcceleration.magnitude > .001f)
        {

            light.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        }
        light.transform.eulerAngles = Vector3.Lerp(light.transform.eulerAngles, new Vector3(90,0,0), .2f*Time.deltaTime);
    }
    public void SpawnDice()
    {
        if (diceList.Count < 5)
        {
            GameObject newDice = Instantiate(dice);
            newDice.transform.position = new Vector3(Random.Range(-1, 1), Random.Range(1, 5), Random.Range(-1.5f, 1.5f));
            diceList.Add(newDice);
            rbList.Add(newDice.GetComponent<Rigidbody>());
        }
    }
    public void DestroyDice()
    {
        if (diceList.Count > 0)
        {
            GameObject oldDice = diceList.ToArray()[0];
            diceList.Remove(oldDice);
            rbList.Remove(oldDice.GetComponent<Rigidbody>());
            Destroy(oldDice);
        }
    }
    bool notAButton(Vector3 point)
    {
        Debug.Log(point);
        return (point.z>-2.5f);
    }
}
