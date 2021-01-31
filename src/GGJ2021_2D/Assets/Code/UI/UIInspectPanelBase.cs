using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIInspectPanelBase : MonoBehaviour
{
    public enum eState
    {
        OFF,
        AnimatingOn,
        Idle,
        AnimatingOff,
    }

    public const float TRANSITION_TIME = 0.25f;

    protected eState m_state;
    protected float m_stateTime;

    public void AnimateOff()
    {
        SetState(eState.AnimatingOff);
    }

    protected abstract void SetState(eState newState);
}
