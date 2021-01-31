using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    public GameController.eScenes m_sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            GameController.Instance.LoadScene(m_sceneToLoad);
        }
    }
}
