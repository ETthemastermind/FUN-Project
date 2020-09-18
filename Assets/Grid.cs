using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour //https://unity3d.college/2017/10/08/simple-unity3d-snap-grid-system/
{
    public int size = 1;
    public float GridSize_X;
    public float GridSize_Y;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3( (float)xCount * size, (float)yCount * size, (float)zCount * size);
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < GridSize_X; x ++)
        {
            for (float y = 0; y < GridSize_Y; y ++)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, y));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
