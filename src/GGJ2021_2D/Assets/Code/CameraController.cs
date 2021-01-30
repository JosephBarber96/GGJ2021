using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private enum eState
    {
        Off,
        On,
    }

    public float m_CameraSmoothScalar = 16f;

    private Camera m_camera;
    private Player m_player;
    private Vector2 _cameraVelocity = Vector2.zero;
    private eState m_state;

    private void Awake()
    {
        m_state = eState.Off;
    }

    private void OnEnable()
    {
        GameController.OnSceneLoad += OnSceneLoad;
    }

    private void OnDisable()
    {
        GameController.OnSceneLoad -= OnSceneLoad;
    }

    private void OnSceneLoad(GameController.eScenes scene)
    {
        if (scene == GameController.eScenes.Tutorial || 
            scene == GameController.eScenes.Game)
        {
            m_player = FindObjectOfType<Player>();
            m_camera = Camera.main;
            m_state = eState.On;
        }
    }

    private void LateUpdate()
    {
        if (m_state == eState.On)
        {
            float z = m_camera.transform.position.z;
            Vector3 thisPos = m_camera.transform.position;

            m_camera.transform.position = Vector3.Lerp(
                new Vector3(thisPos.x, thisPos.y, z),
                new Vector3(m_player.Position.x, m_player.Position.y, z),
                Time.deltaTime * m_CameraSmoothScalar
                );
        }
    }
}
