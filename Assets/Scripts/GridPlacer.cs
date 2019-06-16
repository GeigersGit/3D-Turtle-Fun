using UnityEngine;
using System.Collections;

public class GridPlacer : MonoBehaviour {
    bool hover = false;
    public Object SquarePrefab;
    public Transform SmokePrefab;

    private Renderer rend;

    public bool front = false;
    public bool back = false;
    public bool right = false;
    public bool left = false;
    Vector3 offset;
    Vector3 smokeoffset = new Vector3(0, 5, 0);
    


	// Use this for initialization
	void Start ()
    {
        SquarePrefab = GameObject.Find("SquareOriginal");
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    void OnMouseEnter()
    {

        hover = true;
        rend.enabled = true;
    }
    void OnMouseExit()
    {
        hover = false;
        rend.enabled = false;
    }


	// Update is called once per frame
	void Update ()
    {
        if (hover)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (front)
                    offset = new Vector3(0, 0, 10);
                              
                if (back)
                    offset = new Vector3(0, 0, -10);
                
                if (right)
                    offset = new Vector3(10, 0, 0);
                
                if (left)
                    offset = new Vector3(-10, 0, 0);
                
                Instantiate(SquarePrefab, transform.parent.position + offset, transform.parent.rotation);
                Instantiate(SmokePrefab, transform.parent.position + offset + smokeoffset, SmokePrefab.transform.rotation);
            }
        }
	}
}
