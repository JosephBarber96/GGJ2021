using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator PlayerAnimator;
    public Rigidbody2D Rb;

    [Range(1, 50)]
    public float VerticalSpeed;
    [Range(1, 50)]
    public float HorizontalSpeed;

    private int _animXBlendHash, _animYBlendHash;
    private HashSet<GameObject> _interactableObjects;
    private ActionManager _actionManager;

    private Vector2 m_currentBlend;
    private Vector2 m_targetBlend;

    void Awake()
    {
        _interactableObjects = new HashSet<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animXBlendHash = Animator.StringToHash("xMove");
        _animYBlendHash = Animator.StringToHash("yMove");
        _actionManager = ActionManager.Instance;

        m_currentBlend = Vector2.zero;
        m_targetBlend = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();

        if (_actionManager.IsEnabled(ActionManager.InputAction.Interact))
        {
            Interact();
        }
    }

    private void UpdateMovement()
    {
        var movingForward = _actionManager.IsEnabled(ActionManager.InputAction.MoveForward);
        var movingBackward = _actionManager.IsEnabled(ActionManager.InputAction.MoveBackward);
        var movingLeft = _actionManager.IsEnabled(ActionManager.InputAction.MoveLeft);
        var movingRight = _actionManager.IsEnabled(ActionManager.InputAction.MoveRight);
        bool move = (movingForward || movingBackward || movingLeft || movingRight);

        Vector2 moveDelta = Vector2.zero;
        if (movingForward)
        {
            moveDelta.y += VerticalSpeed;
            m_targetBlend.y = 1;
        }
        else if (movingBackward)
        {
            moveDelta.y += -VerticalSpeed;
            m_targetBlend.y = -1;
        }
        else
        {
            m_targetBlend.y = 0;
        }

        if (movingRight)
        {
            moveDelta.x += HorizontalSpeed;
            m_targetBlend.x = 1;
        }
        else if (movingLeft)
        {
            moveDelta.x += -HorizontalSpeed;
            m_targetBlend.x = -1;
        }
        else
        {
            m_targetBlend.x = 0;
        }

        if (move)
        {
            const float MAGIC_LERP_NUMBER = 16;
            m_currentBlend = Vector2.Lerp(m_currentBlend, m_targetBlend, Time.deltaTime * MAGIC_LERP_NUMBER);
        }

        moveDelta.Normalize();
        Vector2 newPos = Rb.position;
        newPos.x += moveDelta.x * HorizontalSpeed * Time.deltaTime;
        newPos.y += moveDelta.y * VerticalSpeed * Time.deltaTime;
        Rb.MovePosition(newPos);

        // Animator
        PlayerAnimator.SetFloat(_animXBlendHash, m_currentBlend.x);
        PlayerAnimator.SetFloat(_animYBlendHash, m_currentBlend.y);
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
