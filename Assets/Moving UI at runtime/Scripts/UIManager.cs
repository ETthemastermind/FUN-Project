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
    // Start is called before the first frame update
    void Start()
    {
        CellsInScene = GameObject.FindGameObjectsWithTag("Cell");
        for (int i = 0; i < CellsInScene.Length; i++)
        {
            CellsInScene[i].GetComponent<Button>().interactable = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ChosenCell != null)
        {
            ChosenCell.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
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
            
            SW.WriteLine(CellsInScene[i].name + "|" + CellsInScene[i].transform.localPosition + "|" + CellsInScene[i].transform.localScale);
        }
            
        
        SW.Close();
    }

    public void LoadCellPosition()
    {
       
    }
}
