using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MapClick : MonoBehaviour, IPointerClickHandler //https://forum.unity.com/threads/interactable-minimap-using-raw-image-render-texture-solved.525486/
{
    // Start is called before the first frame update
    public Camera LevelBuildCamera;
    private Transform GridFound;
    public GameObject TestBalloon;
    public GameObject BalloonHistoryButton_Prefab; //reference to the button that will get added to the scroll view button history box
    public GameObject BalloonHistory_Content;

    

    private Texture[][] TextureArray = new Texture[3][];
    public Texture[] OnePointBallonTex;
    public Texture[] TwoPointBallonTex;
    public Texture[] ThreePointBallonTex;

    public string CurrentChosenColour;
    public int CurrentChosenValue = 1;
    public Texture RequestedTexture;

    public GameObject TemplateBalloon;

    public GameObject Test;

    public List<BalloonProperties> BHistory;
    public BalloonProperties LastBalloon;

    public void Start()
    {
        TextureArray[0] = OnePointBallonTex;
        TextureArray[1] = TwoPointBallonTex;
        TextureArray[2] = ThreePointBallonTex;

  
        Material BalloonMat = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
        string BalloonTexName = BalloonMat.mainTexture.name;
        Debug.Log(BalloonTexName);

        if (BalloonTexName.Contains("Red"))
        {
            CurrentChosenColour = "Red";
            ChangePreview();
        }
        else if (BalloonTexName.Contains("Yellow"))
        {
            CurrentChosenColour = "Yellow";
            ChangePreview();
        }
        else if (BalloonTexName.Contains("Green"))
        {
            CurrentChosenColour = "Green";
            ChangePreview();
        }
        else if (BalloonTexName.Contains("Purple"))
        {
            CurrentChosenColour = "Purple";
            ChangePreview();
        }
        else if (BalloonTexName.Contains("Blue"))
        {
            CurrentChosenColour = "Blue";
            ChangePreview();
        }
        else
        {
            Debug.Log("No Color found");
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Clicked");
        Vector2 localCursor = new Vector2(0, 0);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform, eventData.pressPosition, eventData.pressEventCamera, out localCursor))
        {
            Texture tex = GetComponent<RawImage>().texture;
            Rect r = GetComponent<RawImage>().rectTransform.rect;

            float coordX = Mathf.Clamp(0, (((localCursor.x - r.x) * tex.width) / r.width), tex.width);
            float coordY = Mathf.Clamp(0, (((localCursor.y - r.y) * tex.height) / r.height), tex.height);

            float recalcX = coordX / tex.width;
            float recalcY = coordY / tex.height;

            localCursor = new Vector2(recalcX, recalcY);
            CastMiniMapToWorld(localCursor);

        }
    }

    private void CastMiniMapToWorld(Vector2 localCursor)
    {
        Ray LevelBuildRay = LevelBuildCamera.ScreenPointToRay(new Vector2(localCursor.x * LevelBuildCamera.pixelWidth, localCursor.y * LevelBuildCamera.pixelHeight));
        RaycastHit LevelBuildCamHit;
        if (Physics.Raycast(LevelBuildRay, out LevelBuildCamHit, Mathf.Infinity))
        {
            //Debug.Log(LevelBuildCamHit.collider.gameObject + "found via render texture");
            if (LevelBuildCamHit.transform.tag == "GridCube")
            {
                GridFound = LevelBuildCamHit.transform;
                Vector3 SpawnPoint = new Vector3(GridFound.position.x, 1f, GridFound.position.z);
                GameObject SpawnedBalloon = Instantiate(TestBalloon, SpawnPoint, Quaternion.identity);
                Material BalloonMat = SpawnedBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                BalloonMat.SetTexture("_MainTex", RequestedTexture);
                SpawnedBalloon.GetComponent<ConfigedBalloon>().BalloonValue = CurrentChosenValue;
                GameObject SpawnedBalloonButton = Instantiate(BalloonHistoryButton_Prefab) as GameObject;

                LastBalloon.value = CurrentChosenValue;
                LastBalloon.Color = CurrentChosenColour;
                LastBalloon.GridLoc = GridFound.gameObject.GetComponent<GridAttributes>().GridCoords;
                BHistory.Add(LastBalloon);

                SpawnedBalloonButton.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SpawnedBalloon.name; //change the text on the button
                TMP_Dropdown dropdownLabel = SpawnedBalloonButton.transform.GetChild(1).GetComponent<TMP_Dropdown>(); //reference to the color dropdown box
                switch (CurrentChosenColour) //switch case to change the value/label of the dropdown
                {
                    case ("Red"):
                        dropdownLabel.value = 0;
                        break;
                    case ("Yellow"):
                        dropdownLabel.value = 1;
                        break;
                    case ("Green"):
                        dropdownLabel.value = 2;
                        break;
                    case ("Purple"):
                        dropdownLabel.value = 3;
                        break;
                    case ("Blue"):
                        dropdownLabel.value = 4;
                        break;

                }
                TMP_Dropdown dropdownLabel2 = SpawnedBalloonButton.transform.GetChild(2).GetComponent<TMP_Dropdown>();
                dropdownLabel2.value = CurrentChosenValue - 1;
                SpawnedBalloonButton.transform.parent = BalloonHistory_Content.transform;
                BalloonHistoryButtonScript BHBS = SpawnedBalloonButton.GetComponent<BalloonHistoryButtonScript>();
                BHBS.Balloon = SpawnedBalloon;
                BHBS.LevelDesignManager = this;
                BHBS.BP = LastBalloon;
                
                
                
            }
        }

    }

    public void SetBalloonColor(GameObject ButtonInput)
    {
        Material BalloonMat = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
        
        switch (ButtonInput.name)
        {
            case ("Red"):
                Debug.Log("Balloon Set to Red");
                CurrentChosenColour = "Red";
                ChangePreview();
                break;

            case ("Yellow"):
                Debug.Log("Balloon Set to Yellow");
                CurrentChosenColour = "Yellow";
                ChangePreview();
                break;

            case ("Green"):
                Debug.Log("Balloon Set to Green");
                CurrentChosenColour = "Green";
                ChangePreview();
                break;

            case ("Purple"):
                Debug.Log("Balloon Set to Purple");
                CurrentChosenColour = "Purple";
                ChangePreview();
                break;

            case ("Blue"):
                Debug.Log("Balloon Set to Blue");
                CurrentChosenColour = "Blue";
                ChangePreview();
                break;

            default:
                break;
        }
    }

    public void ChangeValue(int value)
    {
        CurrentChosenValue = value;
        ChangePreview();
    }

    public void ChangePreview()
    {
        int TrueValue = CurrentChosenValue - 1;
        for (int i = 0; i < TextureArray[TrueValue].Length; i++)
        {
            switch (CurrentChosenColour)
            {
                case "Red":
                    RequestedTexture = TextureArray[TrueValue][0];
                    Material BalloonMat = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat.SetTexture("_MainTex", RequestedTexture);
                    break;

                case "Yellow":
                    RequestedTexture = TextureArray[TrueValue][1];
                    Material BalloonMat2 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat2.SetTexture("_MainTex", RequestedTexture);
                    break;

                case "Green":
                    RequestedTexture = TextureArray[TrueValue][2];
                    Material BalloonMat3 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat3.SetTexture("_MainTex", RequestedTexture);
                    break;

                case "Purple":
                    RequestedTexture = TextureArray[TrueValue][3];
                    Material BalloonMat4 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat4.SetTexture("_MainTex", RequestedTexture);
                    break;

                case "Blue":
                    RequestedTexture = TextureArray[TrueValue][4];
                    Material BalloonMat5 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat5.SetTexture("_MainTex", RequestedTexture);
                    break;

                default:
                    break;

            }
        }
    }
}
