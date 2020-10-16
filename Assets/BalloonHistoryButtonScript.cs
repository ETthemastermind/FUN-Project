using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonHistoryButtonScript : MonoBehaviour
{
    public GameObject Balloon;
    public BalloonProperties BP;
    public MapClick LevelDesignManager;

    public GameObject NameButton;
    public GameObject ColorDropdown;
    public GameObject ValueDropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAsPreview()
    {
        
        Debug.Log("Chaning Preview");
        LevelDesignManager.CurrentChosenColour = BP.Color;
        LevelDesignManager.CurrentChosenValue = BP.value;
        LevelDesignManager.ChangePreview();
    }

    public void ColorChanged()
    {
        string label = ColorDropdown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
        
    }

    public void ValueChanged()
    {

    }

    public void RemoveBalloonAndButton()
    {
        Destroy(Balloon);
        Destroy(gameObject);
    }
}
