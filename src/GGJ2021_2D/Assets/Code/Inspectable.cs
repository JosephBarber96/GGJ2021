using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inspectable : MonoBehaviour, IInteractable
{   
    [Header("Inspectable")]
    public SpriteRenderer m_inspectIcon;

    private Vector2 m_inspectSpriteStartPos;
    private Vector2 m_inspectSpriteBouncePos;

    private void Start()
    {
        m_inspectSpriteStartPos = m_inspectIcon.transform.position;
        m_inspectSpriteBouncePos = m_inspectSpriteStartPos + Vector2.up * 0.25f;
        Utils.SetImageAlpha(m_inspectIcon, 0);
    }

    private void Update()
    {
        Update_InspectIcon();
    }


    //-----------------------
    // Player interact  

    public abstract void Interact(Player player);


    //-----------------------
    // Inspect icon

    private const float ICON_LERP_TIME = 0.25f;

    private enum eInspectIconState
    {
        Off,
        LerpOn,
        On,
        LerpOff,
    }

    private eInspectIconState m_iconState;
    private float m_iconT;

    private void Update_InspectIcon()
    {
        // Lerp on
        if (m_iconState == eInspectIconState.LerpOn)
        {
            m_iconT += Time.deltaTime;
            float normalized = m_iconT / ICON_LERP_TIME;

            // Lerp alpha
            Utils.SetImageAlpha(m_inspectIcon, normalized);

            // Bounce position
            float easing = Easing.Back.Out(normalized);
            Vector2 pos = Vector2.Lerp(m_inspectSpriteStartPos, m_inspectSpriteBouncePos, easing);
            m_inspectIcon.transform.position = pos;

            if (normalized > 1)
                m_iconState = eInspectIconState.On;
        }
        // Lerp off
        else if (m_iconState == eInspectIconState.LerpOff)
        {
            m_iconT += Time.deltaTime;
            float normalized = m_iconT / ICON_LERP_TIME;

            // Lerp alpha
            Utils.SetImageAlpha(m_inspectIcon, 1 - normalized);

            // Bounce position
            Vector2 pos = Vector2.Lerp(m_inspectSpriteBouncePos, m_inspectSpriteStartPos, normalized);
            m_inspectIcon.transform.position = pos;

            if (normalized > 1) 
                m_iconState = eInspectIconState.Off;
        }
    }

    private void ShowInspectIcon()
    {
        if (m_iconState != eInspectIconState.On && m_iconState != eInspectIconState.LerpOn)
        {
            m_iconState = eInspectIconState.LerpOn;
            m_iconT = 0f;
        }
    }


    private void HideInspectIcon()
    {
        m_iconState = eInspectIconState.LerpOff;
        m_iconT = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            ShowInspectIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            HideInspectIcon();
            if (UIController.Instance.CurrentInspectable == this)
            {
                UIController.Instance.HideInspect();
            }
        }
    }
}
