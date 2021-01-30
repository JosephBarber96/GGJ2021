using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionManager : MonoBehaviour
{
    #region Singleton
    private static ActionManager _instance;
    private static ActionManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }
    #endregion

    public enum InputAction
    {
        MoveForward,
        MoveBackward,
        MoveRight,
        MoveLeft,
        Interact,
    }

    public static Dictionary<InputAction, bool> ActionStates = Enum.GetValues(typeof(InputAction))
        .Cast<InputAction>()
        .ToDictionary(x => x, _ => false);

    public bool IsEnabled(InputAction action) => ActionStates[action];

    void Update()
    {
        ActionStates[InputAction.MoveForward] = Input.GetKey(KeyCode.W);
        ActionStates[InputAction.MoveBackward] = Input.GetKey(KeyCode.S);
        ActionStates[InputAction.MoveLeft] = Input.GetKey(KeyCode.A);
        ActionStates[InputAction.MoveRight] = Input.GetKey(KeyCode.D);
        ActionStates[InputAction.Interact] = Input.GetKeyDown(KeyCode.E);
    }
}
