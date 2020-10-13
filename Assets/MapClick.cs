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

    public Texture[] OnePointBallonTex;
    public Texture[] TwoPointBallonTex;
    public Texture[] ThreePointBallonTex;

    public string CurrentChosenColour;
    public GameObject TemplateBalloon;



    public void Start()
    {
        Time.timeScale = 0;
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
                BalloonMat.SetTexture("_MainTex", OnePointBallonTex[0]);
                break;

            case ("Yellow"):
                Debug.Log("Balloon Set to Yellow");
                CurrentChosenColour = "Yellow";
                BalloonMat.SetTexture("_MainTex", OnePointBallonTex[1]);
                break;

            case ("Green"):
                Debug.Log("Balloon Set to Green");
                CurrentChosenColour = "Green";
                BalloonMat.SetTexture("_MainTex", OnePointBallonTex[2]);
                break;

            case ("Purple"):
                Debug.Log("Balloon Set to Purple");
                CurrentChosenColour = "Purple";
                BalloonMat.SetTexture("_MainTex", OnePointBallonTex[3]);
                break;

            case ("Blue"):
                Debug.Log("Balloon Set to Blue");
                CurrentChosenColour = "Blue";
                BalloonMat.SetTexture("_MainTex", OnePointBallonTex[4]);
                break;

            default:
                break;
        }
    }
}
