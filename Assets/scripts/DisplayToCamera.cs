using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayToCamera : MonoBehaviour
{

    public Transform cam;
    private Transform playertransform;
    // Start is called before the first frame update

    void Start()
    {
        playertransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newAngle = new Vector3(cam.rotation.eulerAngles.x, cam.eulerAngles.y, cam.eulerAngles.z);
        playertransform.rotation = Quaternion.Euler(newAngle);
    }
}
