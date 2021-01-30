using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera PlayerCamera;
    public GameObject Player;
    [Range(0, 1f)]
    public float CameraSmoothTime;

    private Vector2 _cameraVelocity = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        var targetPosition = Vector2.SmoothDamp(PlayerCamera.transform.position, Player.transform.position,
            ref _cameraVelocity, CameraSmoothTime);
        PlayerCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y, PlayerCamera.transform.position.z);
    }
}
