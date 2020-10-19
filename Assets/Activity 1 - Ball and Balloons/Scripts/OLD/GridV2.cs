using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridV2 : MonoBehaviour
{
    public float GridSizeX;
    public float GridSizeY;

    public float NumberOfCellsX;
    public float NumberOfCellsY;

    public float size;
    // Start is called before the first frame update
    void Start()
    {
        GridSizeX = GetComponent<Collider>().bounds.size.x;
        GridSizeY = GetComponent<Collider>().bounds.size.z;
        for (int y = -4; y < GridSizeY; y++)
        {
            for (int i = 0; i < NumberOfCellsX; i++)
            {

                float Destination = (GridSizeX / NumberOfCellsX * i);
                Debug.Log(Destination);
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = new Vector3(Destination - 4.5f, transform.position.y, transform.position.z + 0.7f + y);
            }
        }
        
        /*
        for (int i = 0; i < NumberOfCellsY; i++)
        {

            float Destination = (GridSizeY / NumberOfCellsY) * i;
            Debug.Log(Destination);
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = new Vector3(transform.position.x, transform.position.y, Destination - 6.5f);
        }
        */
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
