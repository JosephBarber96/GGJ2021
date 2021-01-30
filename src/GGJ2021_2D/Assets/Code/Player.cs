using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerAnimator;
    public ActionManager ActionManager;
    private int _movingForwardHash;
    private int _movingBackwardHash;
    private int _movingLeftHash;
    private int _movingRightHash;


    // Start is called before the first frame update
    void Start()
    {
        _movingForwardHash = Animator.StringToHash("MovingForward");
        _movingBackwardHash = Animator.StringToHash("MovingBackward");
        _movingLeftHash = Animator.StringToHash("MovingLeft");
        _movingRightHash = Animator.StringToHash("MovingRight");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimator.SetBool(_movingForwardHash, ActionManager.IsEnabled(ActionManager.InputAction.MoveForward));
        PlayerAnimator.SetBool(_movingBackwardHash, ActionManager.IsEnabled(ActionManager.InputAction.MoveBackward));
        PlayerAnimator.SetBool(_movingLeftHash, ActionManager.IsEnabled(ActionManager.InputAction.MoveLeft));
        PlayerAnimator.SetBool(_movingRightHash, ActionManager.IsEnabled(ActionManager.InputAction.MoveRight));
    }
}
