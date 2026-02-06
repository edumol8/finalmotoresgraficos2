using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField]
    private float _gravityMultiplier;

    private float _gravity;

    private Rigidbody _rigidbody;

    public float GravityMultiplier
    {
        get
        {
            return _gravityMultiplier;
        }
        set
        {
            _gravityMultiplier = value;
            _gravity = Physics.gravity.y * GravityMultiplier;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gravity = Physics.gravity.y * GravityMultiplier;
    }

    private void FixedUpdate()
    {
        float ySpeed = _rigidbody.linearVelocity.y + (_gravity * Time.deltaTime);
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, ySpeed, _rigidbody.linearVelocity.z);
    }
}
