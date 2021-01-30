using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerAnimator;
    public ActionManager ActionManager;
    public Collider2D InteractTriggerCollider;

    [Range(1, 50)]
    public float VerticalSpeed;
    [Range(1, 50)]
    public float HorizontalSpeed;

    private int _movingForwardHash;
    private int _movingBackwardHash;
    private int _movingLeftHash;
    private int _movingRightHash;
    public HashSet<GameObject> _interactableObjects;

    void Awake()
    {
        _interactableObjects = new HashSet<GameObject>();
    }

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

        if (ActionManager.IsEnabled(ActionManager.InputAction.Interact))
        {
            Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        _interactableObjects.Add(collider.gameObject);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        _interactableObjects.Remove(collider.gameObject);
    }

    private void Interact()
    {
        var closestInteractable = _interactableObjects
            .OrderBy(x => (x.transform.position - transform.position).sqrMagnitude)
            .Select(x => x.GetComponent<IInteractable>())
            .FirstOrDefault(x => x != null);

        closestInteractable?.Interact(this);
    }
}
