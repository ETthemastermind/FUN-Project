using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapClick : MonoBehaviour, IPointerClickHandler //https://forum.unity.com/threads/interactable-minimap-using-raw-image-render-texture-solved.525486/
{
    // Start is called before the first frame update
    public Camera LevelBuildCamera;
    public Transform GridFound;
    public GameObject TestBalloon;
    public GameObject BalloonHistoryButton_Prefab; //reference to the button that will get added to the scroll view button history box
    public GameObject BalloonHistory_Content;

    public List<BalloonProperties> BP;


    public Texture[][] TextureArray = new Texture[3][];
    public Texture[] OnePointBallonTex;
    public Texture[] TwoPointBallonTex;
    public Texture[] ThreePointBallonTex;

    public string CurrentChosenColour;
    public int CurrentChosenValue = 1;
    public GameObject TemplateBalloon;
    


    public void Start()
    {
        TextureArray[0] = OnePointBallonTex;
        TextureArray[1] = TwoPointBallonTex;
        TextureArray[2] = ThreePointBallonTex;

        Time.timeScale = 0;
        Material BalloonMat = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
        string BalloonTexName = BalloonMat.mainTexture.name;
        Debug.Log(BalloonTexName);

        if (BalloonTexName.Contains("Red"))
        {
            CurrentChosenColour = "Red";
        }
        else if (BalloonTexName.Contains("Yellow"))
        {
            CurrentChosenColour = "Yellow";
        }
        else if (BalloonTexName.Contains("Green"))
        {
            CurrentChosenColour = "Green";
        }
        else if (BalloonTexName.Contains("Purple"))
        {
            CurrentChosenColour = "Purple";
        }
        else if (BalloonTexName.Contains("Blue"))
        {
            CurrentChosenColour = "Blue"; 
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
                GameObject SpawnedBalloonButton = Instantiate(BalloonHistoryButton_Prefab) as GameObject;
                SpawnedBalloonButton.transform.parent = BalloonHistory_Content.transform;
                SpawnedBalloonButton.transform.GetChild(0).GetComponent<Text>().text = SpawnedBalloon.name;
                
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
                    Texture RequestedTexture = TextureArray[TrueValue][0];
                    Material BalloonMat = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat.SetTexture("_MainTex", RequestedTexture);
                    break;

                case "Yellow":
                    Texture RequestedTexture2 = TextureArray[TrueValue][1];
                    Material BalloonMat2 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat2.SetTexture("_MainTex", RequestedTexture2);
                    break;

                case "Green":
                    Texture RequestedTexture3 = TextureArray[TrueValue][2];
                    Material BalloonMat3 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat3.SetTexture("_MainTex", RequestedTexture3);
                    break;

                case "Purple":
                    Texture RequestedTexture4 = TextureArray[TrueValue][3];
                    Material BalloonMat4 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat4.SetTexture("_MainTex", RequestedTexture4);
                    break;

                case "Blue":
                    Texture RequestedTexture5 = TextureArray[TrueValue][4];
                    Material BalloonMat5 = TemplateBalloon.GetComponent<Renderer>().material; //gets the material on the balloon
                    BalloonMat5.SetTexture("_MainTex", RequestedTexture5);
                    break;

                default:
                    break;

            }
        }
    }
}
