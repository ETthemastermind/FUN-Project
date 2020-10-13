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
                Instantiate(TestBalloon, SpawnPoint, Quaternion.identity);
            }
        }

    }
}
