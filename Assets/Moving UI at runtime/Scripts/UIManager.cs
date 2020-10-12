using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class UIManager : MonoBehaviour
{
    public bool CellsActive = false; //reference to check if the cells functionality is active 

    public GameObject[] CellsInScene; //array of all the objects with the "Cell" tag
    public GameObject ChosenCell; //current chosen cell

    public Vector2 CurrentMouseLocation; //current mouse location
    public float[] NewAnchor = new float[2]; //float to put the new anchor values in 

    public float ScreenRes_W; //these were mainly to just check to see if the values for the screen resolution were correct
    public float ScreenRes_H;

    public string[] LoadedData; //string array for the data load from the .txt
    
    // Start is called before the first frame update
    void Start()
    {
        CellsInScene = GameObject.FindGameObjectsWithTag("Cell"); //finds all the objects with the "Cell" tag
        for (int i = 0; i < CellsInScene.Length; i++) //for each gameobject in the cell array
        {
            CellsInScene[i].transform.FindChild("Cell").gameObject.GetComponent<Button>().interactable = false;
            CellsInScene[i].transform.FindChild("FunctionButtons").gameObject.SetActive(false);
           
        }
        ScreenRes_W = (Screen.width); //find the width and height of the screen, mainly here for debug
        ScreenRes_H = (Screen.height);


        LoadCellPosition(); //load saved UI data
    }

    // Update is called once per frame
    void Update()
    {
        if (ChosenCell != null) //if the user has chosen a cell to move
        {
            CurrentMouseLocation = Input.mousePosition; //get the current mouse position
            ChosenCell.transform.position = new Vector3(CurrentMouseLocation.x, CurrentMouseLocation.y, 0); //have the cell follow the mouse
        }
    }
    
    public void ActivateCells() //show the cells to the user
    {
        if (CellsActive == false) //if the user is currently not the edit cell mode
        {
            CellsActive = true; //activate the cells active variable
            Debug.Log("Cell moving mode enabled"); //print to console the the cell moving mode is active
            for (int i = 0; i < CellsInScene.Length; i++) //for the length of the "cells in scene" array
            {
                GameObject CellBorder = CellsInScene[i].transform.FindChild("Cell").gameObject; //refernce some things to make them easier to code for
                GameObject FunctionButtons = CellBorder.GetComponent<Cell>().FunctionButtons;
                GameObject UIButtons = CellBorder.GetComponent<Cell>().UIButtons; 
                List<GameObject> UIbuttons_List = CellBorder.GetComponent<Cell>().ComponentsUnderCell;

                CellBorder.GetComponent<Button>().interactable = true; //activate the borders of the cell
                FunctionButtons.SetActive(true);
                foreach (GameObject uiButton in UIbuttons_List)
                {
                    uiButton.GetComponent<Button>().interactable = false;
                    uiButton.GetComponent<Image>().raycastTarget = false;
                    
                }
                if (CellBorder.GetComponent<Cell>().HiddenCell == true)
                {
                    UIButtons.SetActive(true);
                }


            }

        }

        else //if the cellsactive is true, it basically does the opposite
        {
            CellsActive = false; //set the cells active variable to false
            Debug.Log("Cell moving mode disabled"); //print to console that the cell moving mode is disabled
            for (int i = 0; i < CellsInScene.Length; i++) //for the length of the "cells in scene" array
            {
                GameObject CellBorder = CellsInScene[i].transform.FindChild("Cell").gameObject;
                GameObject FunctionButtons = CellBorder.GetComponent<Cell>().FunctionButtons;
                GameObject UIButtons = CellBorder.GetComponent<Cell>().UIButtons;
                List<GameObject> UIbuttons_List = CellBorder.GetComponent<Cell>().ComponentsUnderCell;

                CellBorder.GetComponent<Button>().interactable = false; //deactivate the border of the cell
                FunctionButtons.SetActive(false);
               
                foreach (GameObject uiButton in UIbuttons_List)
                {
                    uiButton.GetComponent<Button>().interactable = true;
                    uiButton.GetComponent<Image>().raycastTarget = true;
                    
                }

                if (CellBorder.GetComponent<Cell>().HiddenCell == true)
                {
                    UIButtons.SetActive(false);
                }


                

            }


        }
    }

    
    
    public void ActivateCell(GameObject Cell) //function that gets called when a specific cell is clicked on
    {
        if (ChosenCell == null) //if no cell is currently being held
        {
            Debug.Log("Pick up cell"); //print to console that a cell has been picked up
            ChosenCell = Cell.transform.parent.gameObject; //sets the chosen cell to this cell
        }

        else //if a cell is currently being held
        {
            Debug.Log("Put down cell"); //print to console that a cell has been put down
            Vector3 CellLocation = ChosenCell.transform.position; //gets the current location of the cell
            CalculateAnchor(); //run the script to calculate where the anchor should be
            ChosenCell.transform.position = CellLocation; //reset the location of the cell, if the anchor changes then its position moves
            ChosenCell = null; //set the chosen cell to null
            SaveCellPosition(); //run the save cell position script
            
        }

    }
    

    public void SaveCellPosition() //horrible function
    {
        Debug.Log("Saved Cell Position"); //print to console that the UI layout is being saved
        string FileName = "/TestSave"; //TEMP filename, this will need to be swapped out when the profile system is working as intended
        string FilePath = Application.streamingAssetsPath + FileName + ".txt"; //TEMP filepath, this will need to be swapped out when the profile system is working as intended

        StreamWriter SW = new StreamWriter(FilePath, false); //creates a new file at the filepath

        for (int i = 0; i < CellsInScene.Length; i++) //for the amount of Cells in the scene
        {
            
            SW.WriteLine(CellsInScene[i].name + "|" + CellsInScene[i].transform.position + "|" + CellsInScene[i].transform.localScale + "|" + CellsInScene[i].GetComponent<RectTransform>().anchorMax + "|" + CellsInScene[i].GetComponent<RectTransform>().anchorMin); //create a string holding the data, why im doing it like this i have absolutely no idea
        }
            
        
        SW.Close(); //close the Streamwriter
    }
//==================================================================working on this==============================================
    public void LoadCellPosition()  //a more so horrible function
    {
        string FileName = "/TestSave"; //TEMP filename, will be swapped out later
        string FilePath = Application.streamingAssetsPath + FileName + ".txt"; //TEMP filepath, will be swapped out later

        if (File.Exists(FilePath)) //if the file exists
        {
            Debug.Log("File Found"); //debug that the file was found

            StreamReader SR = new StreamReader(FilePath); //opens a new streamreader at the filepath

            //Debug.Log (SR.ReadToEnd());
            string contents = SR.ReadToEnd(); //full contents of the filepath
            LoadedData = contents.Split('\n'); //split the contents by line
            for (int i = 0; i < CellsInScene.Length; i++) //for the number of Cells in the scene
            {
                string[] DataLine = LoadedData[i].Split('|');//spit the i'th entry by |, (|) used to seperate values

                if (CellsInScene[i].name == DataLine[0]) //check to see if the Cell name matches the Cell name in column 0 of the dataline
                {
                    Debug.Log("Data found for " + CellsInScene[i].name); //prints to console that the data for a cell in the scene has been found

                    Vector2 Position = StringToVector2(DataLine[1]); //run the StringToVector2 function with Dataline[1](the saved position from the file) as input and return the value
                    Debug.Log("The position for this cell is: " + Position); //print the returned postion to the console
                    CellsInScene[i].transform.position = Position; //set the position of the cell

                    Vector2 Scale = StringToVector2(DataLine[2]); //run the StringToVector2 function with Dataline[2](the saved scale from the file) as input and return the value
                    Debug.Log("The scale for this cell is:" + Scale); //print the returned scale to the console
                    CellsInScene[i].transform.localScale = Scale; //set the scale of the cell

                    Vector2 CurrentPosition = CellsInScene[i].transform.position;
                    Vector2 AnchorMax = StringToVector2(DataLine[3]);
                    Debug.Log("The AnchorMax for this cell is: " + AnchorMax);
                    CellsInScene[i].GetComponent<RectTransform>().anchorMax = AnchorMax;
                    CellsInScene[i].GetComponent<RectTransform>().anchorMin = AnchorMax;
                    CellsInScene[i].transform.position = CurrentPosition;

                }
            }

            SR.Close();
        }

        
        
    }
    //==========================================================================================================================================

    public void CalculateAnchor() //function to calculate where the anchor should be set once the UI element has been moved. the screen is split into thirds up and down and the anchor is chosen depending which segment of the screen the cell falls in
    {
        Debug.Log("Calculating Anchor Point"); //prints to console that the anchor point is being calculated
        float ScreenRes_W_OneThird = ScreenRes_W / 3; //finds 1/3 of the screen resolution
        float ScreenRes_W_TwoThird = ScreenRes_W_OneThird * 2; //find 2/3 of the screen resolution

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

        ChosenCell.GetComponent<RectTransform>().anchorMax = new Vector2(NewAnchor[0], NewAnchor[1]); //sets the AnchorMax and AnchorMin
        ChosenCell.GetComponent<RectTransform>().anchorMin = new Vector2(NewAnchor[0], NewAnchor[1]);
        
    }

    public Vector2 StringToVector2(string Target) //function to transform the string input 
    {
        string NewTarget = Target.Trim('(',')'); //removes the brackets from the target
        string[] String_Pos = NewTarget.Split(','); //splits the newTarget up by , into an array of numbers as strings
        //Debug.Log(String_Pos[0]); 
        //Debug.Log(String_Pos[1]);
        Vector2 Output = new Vector2(float.Parse(String_Pos[0]), float.Parse(String_Pos[1])); //turns the numbers as strings into numbers as floats and saves them to this output variable
        return Output; //returns the output

    }

}
