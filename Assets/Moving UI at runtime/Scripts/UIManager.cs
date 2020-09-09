using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UIManager : MonoBehaviour
{
    public bool CellsActive = false;

    public GameObject[] CellsInScene;
    public GameObject ChosenCell;

    public Vector2 CurrentMouseLocation;
    public float[] NewAnchor = new float[2];

    public float ScreenRes_W;
    public float ScreenRes_H;
    // Start is called before the first frame update
    void Start()
    {
        CellsInScene = GameObject.FindGameObjectsWithTag("Cell");
        for (int i = 0; i < CellsInScene.Length; i++)
        {
            CellsInScene[i].GetComponent<Button>().interactable = false;
            
        }
        ScreenRes_W = (Screen.width);
        ScreenRes_H = (Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (ChosenCell != null)
        {
            CurrentMouseLocation = Input.mousePosition;
            ChosenCell.transform.position = new Vector3(CurrentMouseLocation.x, CurrentMouseLocation.y, 0);
        }


        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ScaleCellUp();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ScaleCellDown();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCellPosition();
        }
    }

    public void ActivateCells()
    {
        if (CellsActive == false)
        {
            CellsActive = true;
            Debug.Log("Cell moving mode enabled");
            for (int i = 0; i < CellsInScene.Length; i++)
            {
                
                CellsInScene[i].GetComponent<Button>().interactable = true;
                for (int j = 0; j < CellsInScene[i].transform.childCount; j++)
                {
                    GameObject CurrentChild = CellsInScene[i].transform.GetChild(j).gameObject;
                    if (CurrentChild.GetComponent<Button>() != null)
                    {
                        CellsInScene[i].transform.GetChild(j).GetComponent<Button>().enabled = false;

                    }
                    
                }

            }

        }

        else
        {
            CellsActive = false;
            Debug.Log("Cell moving mode disabled");
            for (int i = 0; i < CellsInScene.Length; i++)
            {
                CellsInScene[i].GetComponent<Button>().interactable = false;
                for (int j = 0; j < CellsInScene[i].transform.childCount; j++)
                {
                    GameObject CurrentChild = CellsInScene[i].transform.GetChild(j).gameObject;
                    if (CurrentChild.GetComponent<Button>() != null)
                    {
                        CellsInScene[i].transform.GetChild(j).GetComponent<Button>().enabled = true;
                    }
                    
                }
            }

            
        }
    }

    

    public void ActivateCell(GameObject Cell)
    {
        if (ChosenCell == null)
        {
            Debug.Log("Pick up cell");
            ChosenCell = Cell;
        }

        else
        {
            Debug.Log("Put down cell");
            Vector3 CellLocation = ChosenCell.transform.position;
            CalculateAnchor();
            ChosenCell.transform.position = CellLocation;
            ChosenCell = null;
            
        }
        /*
        Debug.Log(Cell.name + "Cell Activated");
        ChosenCell = Cell;
        //Button.GetComponent<RuntimeButtonMove>().enabled = true;
        */
    }

    public void ScaleCellUp()
    {
        if (ChosenCell != null)
        {
            float NewY = ChosenCell.transform.localScale.y + 0.1f;
            float NewX = ChosenCell.transform.localScale.x + 0.1f;
            ChosenCell.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);
        }
        
    }

    public void ScaleCellDown()
    {
        float NewY = ChosenCell.transform.localScale.y - 0.1f;
        float NewX = ChosenCell.transform.localScale.x - 0.1f;
        ChosenCell.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);

    }

    public void SaveCellPosition()
    {
        Debug.Log("Saved Cell Position");
        string FileName = "/TestSave";
        string FilePath = Application.streamingAssetsPath + FileName + ".txt";

        StreamWriter SW = new StreamWriter(FilePath, false);

        for (int i = 0; i < CellsInScene.Length; i++)
        {
            
            SW.WriteLine(CellsInScene[i].name + "|" + CellsInScene[i].transform.position + "|" + CellsInScene[i].transform.localScale);
        }
            
        
        SW.Close();
    }

    public void LoadCellPosition()
    {
       
    }

    public void CalculateAnchor()
    {
        Debug.Log("Calculating Anchor Point");
        float ScreenRes_W_OneThird = ScreenRes_W / 3;
        float ScreenRes_W_TwoThird = ScreenRes_W_OneThird * 2;

        //Debug.Log(ScreenRes_W_OneThird);
        //Debug.Log(ScreenRes_W_TwoThird);
        //Debug.Log(ScreenRes_W);


        // work out which third of the screen the mouse is on (width)
        if (CurrentMouseLocation.x >= 0 && CurrentMouseLocation.x <= ScreenRes_W_OneThird) //left third
        {
            Debug.Log("Left");
            NewAnchor[0] = 0f;
        }

        else if (CurrentMouseLocation.x >= ScreenRes_W_OneThird && CurrentMouseLocation.x <= ScreenRes_W_TwoThird) // middle third
        {
            Debug.Log("Middle");
            NewAnchor[0] = 0.5f;
        }

        else if (CurrentMouseLocation.x >= ScreenRes_W_TwoThird && CurrentMouseLocation.x <= ScreenRes_W) // top third
        {
            Debug.Log("Top");
            NewAnchor[0] = 1f;
        }

        //work out which third of the screen the mouse is on (height)
        float ScreenRes_H_OneThird = ScreenRes_H / 3;
        float ScreenRes_H_TwoThird = ScreenRes_H_OneThird * 2;

        if (CurrentMouseLocation.y >= 0 && CurrentMouseLocation.y <= ScreenRes_H_OneThird) //left third
        {
            Debug.Log("Bottom");
            NewAnchor[1] = 0f;
        }

        else if (CurrentMouseLocation.y >= ScreenRes_H_OneThird && CurrentMouseLocation.y <= ScreenRes_H_TwoThird) // middle third
        {
            Debug.Log("Middle");
            NewAnchor[1] = 0.5f;
        }

        else if (CurrentMouseLocation.y >= ScreenRes_H_TwoThird && CurrentMouseLocation.y <= ScreenRes_H) // top third
        {
            Debug.Log("Top");
            NewAnchor[1] = 1f;
        }

        ChosenCell.GetComponent<RectTransform>().anchorMax = new Vector2(NewAnchor[0], NewAnchor[1]);
        ChosenCell.GetComponent<RectTransform>().anchorMin = new Vector2(NewAnchor[0], NewAnchor[1]);
        //this.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(Anchor_TopRight[0], Anchor_TopRight[1]);
        //this.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(Anchor_TopRight[0], Anchor_TopRight[1]);
    }

}
