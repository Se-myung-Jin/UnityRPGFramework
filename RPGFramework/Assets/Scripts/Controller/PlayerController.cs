using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        GeneralManager.Input.KeyAction += OnKeyboard;
        GeneralManager.Input.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f)
            {
                _moveToDest = false;
            }
            else
            {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                transform.position += dir.normalized * moveDist;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            }
        }
    }
    
    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
            //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A))
            //transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.S))
            //transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D))
            //transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
        }
    }
}
