using UnityEngine;
using System.Collections;

public class TurretRotate : MonoBehaviour {
    public float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        angle = Mathf.Sin(Time.time);
        transform.RotateAround(transform.parent.position, transform.up, angle);
    }
}
