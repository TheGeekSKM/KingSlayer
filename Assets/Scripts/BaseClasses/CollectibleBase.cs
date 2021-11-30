using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleBase : MonoBehaviour
{
    protected abstract void Collect(PlayerInventory player);
    [SerializeField] float _movementSpeed = 1f;
    protected float MovementSpeed => _movementSpeed;

    [SerializeField] float _moveSpeed = .15f;
    protected float MoveSpeed => _moveSpeed;

    [SerializeField] float zValueToDespawn = -30f;

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.z <= zValueToDespawn)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);

        // Vector3 moveOffset = new Vector3(0, 0, (-1 * _moveSpeed));
        // rb.MovePosition(_rb.position + moveOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory player = other.gameObject.GetComponent<PlayerInventory>();
        if (player != null)
        {
            Collect(player);
            Feedback();

            Destroy(gameObject);
        }
    }

    private void Feedback()
    {
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }

        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }
}
