using UnityEngine;
using System.Collections;

public class WeaponPlacer : MonoBehaviour {
    bool hover = false;
    private Renderer rend;

    public Transform TurretPrefab;

    // Use this for initialization
    void Start () {
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
    void Update () {
        if (hover)
        {
            if(Input.GetMouseButtonDown(0))
				Instantiate(TurretPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
        }
	}
}
