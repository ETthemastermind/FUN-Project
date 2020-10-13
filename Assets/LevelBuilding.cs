using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilding : MonoBehaviour
{
    public Camera _Camera;
    public Transform FoundGrid;
    public LayerMask layerMask;
    public float Distance = 50f;

    public GameObject TestBalloon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Button Pressed");
            PlaceObject();
        }
    }

    public void PlaceObject()
    {
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray,out Hit,layerMask))
        {
            if (Hit.transform.gameObject.tag == "GridCube")
            {
                FoundGrid = Hit.transform;
                Vector3 SpawnPoint = new Vector3(FoundGrid.position.x, 1f, FoundGrid.position.z);
                Instantiate(TestBalloon, SpawnPoint, Quaternion.identity);
            }
            
        }
        



    }
}
