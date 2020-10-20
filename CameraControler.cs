using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    #region 参数
    private int cameraMovespeed = 3;
    private int limitWidth = 20;
    private int limitHeight = 20;
    private float zoomMin = 1;
    private float zoomMax = 20;

    private static Camera mainCamera;
    #endregion

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        MapControl();

    }

    private void MapControl()
    {
        Vector3 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 finalPos;

        if (offset.x > 0.95)
        {
            transform.Translate(Vector3.right * cameraMovespeed * Time.deltaTime, Space.World);
        }
        if (offset.x < 0.05)
        {
            transform.Translate(Vector3.left * cameraMovespeed * Time.deltaTime, Space.World);
        }
        if (offset.y > 0.95)
        {
            transform.Translate(Vector3.forward * cameraMovespeed * Time.deltaTime, Space.World);
        }
        if (offset.y < 0.05)
        {
            transform.Translate(Vector3.back * cameraMovespeed * Time.deltaTime, Space.World);
        }
        //Limit
        //限制x，z坐标
        finalPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        if (finalPos.x < -limitWidth)
        {
            finalPos.x = -limitWidth;
        }
        if (finalPos.x > limitWidth)
        {
            finalPos.x = limitWidth;
        }
        if (finalPos.z < -limitHeight)
        {
            finalPos.z = -limitHeight;
        }
        if (finalPos.z > limitHeight)
        {
            finalPos.z = limitHeight;
        }
        mainCamera.transform.position = finalPos;
      
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 100)//3d
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= zoomMax)//2d
                Camera.main.orthographicSize += 0.5F;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)//3d
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= zoomMin)//2d
                Camera.main.orthographicSize -= 0.5F;
        }
        
    }
}
