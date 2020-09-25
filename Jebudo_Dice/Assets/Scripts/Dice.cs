using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    AudioSource [] audioSource;
    Rigidbody dice;
    int count = 0;
    // Start is called before the first frame update
    public GameObject ColorPickedPrefab;
    private ColorPickerTriangle CP;
    private bool isPaint = false;
    private GameObject go;
    private Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        audioSource = GetComponents<AudioSource>();
        dice = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isPaint)
        {
            mat.color = CP.TheColor;
        }
    }

    public void EditColor()
    {
        if (isPaint)
        {
            StopPaint();
        }
        else
        {
            StartPaint();
        }
    }


    private void StartPaint()
    {
        go = (GameObject)Instantiate(ColorPickedPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
        go.transform.localScale = Vector3.one;
        go.transform.LookAt(Vector3.up*1000000);
        if(transform.position.x<0) go.transform.position += (Vector3.right * .2f);
        else go.transform.position -= (Vector3.right * .2f);
        go.transform.parent = transform;
        dice.isKinematic = true;
        CP = go.GetComponent<ColorPickerTriangle>();
        CP.SetNewColor(mat.color);
        isPaint = true;
    }

    private void StopPaint()
    {
        dice.isKinematic = false;
        Destroy(go);
        isPaint = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        dice = GetComponent<Rigidbody>();

        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            dice.AddForce(contact.normal * 10);
        }
        if (collision.gameObject.name=="Terrain" && collision.relativeVelocity.magnitude > .1f)
        {
            audioSource[count].Play();
            count = (count + (int)Random.Range(0, 4)) % 2;
        }
    }
}
