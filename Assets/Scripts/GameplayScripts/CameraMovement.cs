using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    float moveSpeedMultiplier;
    [SerializeField]
    float zoomSpeedMultiplier;

    float mouseScrollInput;
    Camera mainCamera;

    Vector3 newPosition;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        newPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update () {

        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScrollInput != 0.0f)
        {
            float orthoSize = mainCamera.orthographicSize;
            orthoSize -= mouseScrollInput * zoomSpeedMultiplier * 0.25f;
            orthoSize = Mathf.Clamp(orthoSize, 4.0f, 34.0f);
            mainCamera.orthographicSize = orthoSize;
        }

        if (Input.mousePosition.x <= 25.0)
        {
            float scrollSpeedTmp = 0.5f;
            if (Input.mousePosition.x > 1.0)
            {
                scrollSpeedTmp = (float)((1.0 / Input.mousePosition.x) * moveSpeedMultiplier);
            }

            newPosition = new Vector3(mainCamera.transform.position.x - moveSpeedMultiplier * scrollSpeedTmp, mainCamera.transform.position.y, mainCamera.transform.position.z);
            mainCamera.transform.position = newPosition;
        }

        if (Input.mousePosition.x >= mainCamera.pixelWidth - 25.0) 
        {
            float scrollSpeedTmp = 0.5f;
            if (Input.mousePosition.x < mainCamera.pixelWidth - 1.0)
            {
                scrollSpeedTmp = (float)(1.0 / (mainCamera.pixelWidth - Input.mousePosition.x) * moveSpeedMultiplier);
            }

            newPosition = new Vector3(mainCamera.transform.position.x + moveSpeedMultiplier * scrollSpeedTmp, mainCamera.transform.position.y, mainCamera.transform.position.z);
            mainCamera.transform.position = newPosition;
        }

        if (Input.mousePosition.y <= 25.0)
        {
            float scrollSpeedTmp = 0.5f;
            if (Input.mousePosition.y > 1.0)
            {
                scrollSpeedTmp = (float)((1.0 / Input.mousePosition.y) * moveSpeedMultiplier);
            }

            newPosition = new Vector3(mainCamera.transform.position.x , mainCamera.transform.position.y - moveSpeedMultiplier * scrollSpeedTmp, mainCamera.transform.position.z);
            mainCamera.transform.position = newPosition;
        }

        if (Input.mousePosition.y >= mainCamera.pixelHeight - 25.0)
        {
            float scrollSpeedTmp = 0.5f;
            if (Input.mousePosition.y < mainCamera.pixelHeight - 1.0)
            {
                scrollSpeedTmp = (float)(1.0 / (mainCamera.pixelHeight - Input.mousePosition.y) * moveSpeedMultiplier);
            }

            newPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + moveSpeedMultiplier * scrollSpeedTmp, mainCamera.transform.position.z);
            mainCamera.transform.position = newPosition;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, -19.0f, 19.0f);
        newPosition.y = Mathf.Clamp(newPosition.y, -24.0f, 24.0f);
        mainCamera.transform.position = newPosition;

    }
}
