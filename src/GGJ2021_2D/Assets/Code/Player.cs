using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerAnimator;
    public ActionManager ActionManager;

    [Range(1, 50)]
    public float VerticalSpeed;
    [Range(1, 50)]
    public float HorizontalSpeed;

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
        var movingForward = ActionManager.IsEnabled(ActionManager.InputAction.MoveForward);
        var movingBackward = ActionManager.IsEnabled(ActionManager.InputAction.MoveBackward);
        var movingLeft = ActionManager.IsEnabled(ActionManager.InputAction.MoveLeft);
        var movingRight = ActionManager.IsEnabled(ActionManager.InputAction.MoveRight);

        PlayerAnimator.SetBool(_movingForwardHash, movingForward);
        PlayerAnimator.SetBool(_movingBackwardHash, movingBackward);
        PlayerAnimator.SetBool(_movingLeftHash, movingLeft);
        PlayerAnimator.SetBool(_movingRightHash, movingRight);

        if (movingForward)
        {
            transform.Translate(new Vector2(0, VerticalSpeed * Time.deltaTime));
        }
        if (movingBackward)
        {
            transform.Translate(new Vector2(0, -VerticalSpeed * Time.deltaTime));
        }
        if (movingLeft)
        {
            transform.Translate(new Vector2(-HorizontalSpeed * Time.deltaTime, 0));
        }
        if (movingRight)
        {
            transform.Translate(new Vector2(HorizontalSpeed * Time.deltaTime, 0));
        }
    }
}
