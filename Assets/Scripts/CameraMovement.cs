using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    float x;
    float z;
    public Vector3 pos;
    float height;
    float width;
    bool stop = false;
	// Use this for initialization
	void Start () {
        height = Screen.height;
        width = Screen.width;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(1))
            stop = true;
        else
            stop = false;


        //if (stop)
       // {
            x = Input.mousePosition.x - width / 2;
            z = Input.mousePosition.y - height / 2;

            pos = new Vector3(x / 1000, 0, z / 1000);

            transform.position += pos;
       // }
	}
}
