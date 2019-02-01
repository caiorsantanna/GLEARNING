using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Lista_Eventos_Licao4_Atv1 : MonoBehaviour
{
    public GameObject canvas;

    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {        
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas.SetActive(false);
    }    


}
