using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerCurrentPosition;

    private int minXPosition;
    private int maxXPosition;

    private Vector3 defaultCameraPositiom;

    void Start()
    {
        minXPosition = -9;
        maxXPosition = -7;

        defaultCameraPositiom = transform.position;
    }

    void LateUpdate()
    {
        playerCurrentPosition.x = player.transform.position.x;
        playerCurrentPosition.y = player.transform.position.y;
        transform.position = new Vector3(playerCurrentPosition.x, playerCurrentPosition.y, defaultCameraPositiom.z);
    }
}