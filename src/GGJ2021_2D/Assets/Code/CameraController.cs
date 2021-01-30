using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera PlayerCamera;
    public Player m_Player;
    [Range(0, 1f)]
    public float CameraSmoothTime;

    public float m_CameraSmoothScalar = 16f;

    private Vector2 _cameraVelocity = Vector2.zero;

    private void Awake()
    {
        // fix this as soon as spawning infrastructure is in place to get a ref from the game controller
        m_Player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    var targetPosition = Vector2.SmoothDamp(PlayerCamera.transform.position, m_Player.transform.position,
    //        ref _cameraVelocity, CameraSmoothTime);
    //    PlayerCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y, PlayerCamera.transform.position.z);
    //}

    private void LateUpdate()
    {
        float z = PlayerCamera.transform.position.z;
        Vector3 thisPos = PlayerCamera.transform.position;

        PlayerCamera.transform.position = Vector3.Lerp(
            new Vector3(thisPos.x, thisPos.y, z),
            new Vector3(m_Player.Position.x, m_Player.Position.y, z),
            Time.deltaTime * m_CameraSmoothScalar
            );
    }
}
