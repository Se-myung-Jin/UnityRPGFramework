using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;
    Vector3 _destPos;

    PlayerState _state = PlayerState.Idle;
     
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    Texture2D _attackIcon;
    Texture2D _handIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        _attackIcon = GeneralManager.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = GeneralManager.Resource.Load<Texture2D>("Textures/Cursor/Hand");

        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        GeneralManager.Input.MouseAction -= OnMouseClicked;
        GeneralManager.Input.MouseAction += OnMouseClicked;
    }

    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent agent = gameObject.GetOrAddComponent<NavMeshAgent>(); ;

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            agent.Move(dir.normalized * moveDist);
            //transform.position += dir.normalized * moveDist;

            if (Physics.Raycast(transform.position, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    void Update()
    {
        UpdateMouseCursor();

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);    

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Click!");
            }
            else
            {
                Debug.Log("Ground Click");
            }
        }
    }
}
