using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Inspectable : MonoBehaviour, IInteractable
{
    [Header("Inspectable")]
    [UnityEngine.Serialization.FormerlySerializedAs("m_inspectIcon")]
    public SpriteRenderer m_InteractionHintIcon;
    [UnityEngine.Serialization.FormerlySerializedAs("OnInteract")]
    public UnityEvent OnInteractEvent;

    public bool IsInspectable { get; private set; }

    private Vector2 m_inspectSpriteStartPos;
    private Vector2 m_inspectSpriteBouncePos;

    private void Start()
    {
        m_inspectSpriteStartPos = m_InteractionHintIcon.transform.position;
        m_inspectSpriteBouncePos = m_inspectSpriteStartPos + Vector2.up * 0.25f;
        Utils.SetAlpha(m_InteractionHintIcon, 0);
        IsInspectable = true;
    }

    private void Update()
    {
        Update_InspectIcon();
    }

    private void OnDisable()
    {
        HideInspectIcon();
        OnPlayerExit();
    }


    //-----------------------
    // Player interact  

    public void Interact(Player player)
    {
        if (!IsInspectable) { return; }

        if (OnInteractEvent != null)
        {
            OnInteractEvent.Invoke();
        }

        OnInteract(player);
    }

    protected abstract void OnInteract(Player player);



    //-----------------------
    // Public calls

    public void HideObject()
    {
        this.IsInspectable = false;

        // Dirty hack
        SpriteRenderer[] sprs = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprs.Length; i++)
        {
            sprs[i].enabled = false;
        }
    }


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
            Utils.SetAlpha(m_InteractionHintIcon, normalized);

            // Bounce position
            float easing = Easing.Back.Out(normalized);
            Vector2 pos = Vector2.Lerp(m_inspectSpriteStartPos, m_inspectSpriteBouncePos, easing);
            m_InteractionHintIcon.transform.position = pos;

            if (normalized > 1)
                m_iconState = eInspectIconState.On;
        }
        // Lerp off
        else if (m_iconState == eInspectIconState.LerpOff)
        {
            m_iconT += Time.deltaTime;
            float normalized = m_iconT / ICON_LERP_TIME;

            // Lerp alpha
            Utils.SetAlpha(m_InteractionHintIcon, 1 - normalized);

            // Bounce position
            Vector2 pos = Vector2.Lerp(m_inspectSpriteBouncePos, m_inspectSpriteStartPos, normalized);
            m_InteractionHintIcon.transform.position = pos;

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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null && IsInspectable)
        {
            ShowInspectIcon();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            HideInspectIcon();
            OnPlayerExit();
        }
    }

    protected abstract void OnPlayerExit();
}
