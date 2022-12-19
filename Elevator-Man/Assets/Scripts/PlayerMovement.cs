using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private bool isFacingRight = true;
    public Animator animator;
    public GameObject bullet;
    public Transform shootPoint;
    public float shootSpeed;
    public float bulletSpeed;
    private bool canShoot = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movementInput * _speed;
        Flip();
        animator.SetFloat("Speed", Mathf.Abs(_movementInput.x+_movementInput.y));
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    private void OnFire(InputValue inputValue)
    {
        if(canShoot)
        {
            StartCoroutine(shootGun());
        }
    }

    private void Flip()
    {
        if (isFacingRight && _movementInput.x < 0f || !isFacingRight && _movementInput.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public IEnumerator shootGun()
    {
        canShoot = false;
        GameObject bulletCreated = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody2D bulletRigidbody =  bulletCreated.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(shootPoint.right * bulletSpeed);

        Destroy(bulletCreated, 5);
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }
}