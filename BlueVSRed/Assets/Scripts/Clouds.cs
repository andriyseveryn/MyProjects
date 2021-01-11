using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(-1*Time.deltaTime*0.5f, 0, 0);
        if (transform.position.x <= -2.83)
        {
            transform.position = new Vector3(14.68f, transform.position.y, transform.position.z);
        }
    }
}
