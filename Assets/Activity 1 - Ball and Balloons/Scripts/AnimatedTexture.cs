using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{
    public Material Material; //reference to the material

    public float TextureSpeed; //public float to control the speed
    public float Offset; //public float to see the current offset in the inspector
    // Start is called before the first frame update
    void Start()
    {
        Material = gameObject.GetComponent<Renderer>().material; //get the current material of the object
    }

    // Update is called once per frame
    void Update()
    {
        Offset += Time.deltaTime * TextureSpeed / 10f; //calculate the current offset for the material
        

        Material.mainTextureOffset = new Vector2(Offset, 0); //set the texture offset
    }
}
