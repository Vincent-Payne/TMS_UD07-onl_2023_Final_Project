using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField]
    private Ammo _ammo;
    [SerializeField]
    private float _fireForce;
    [SerializeField]
    private float _fireRate;
    [SerializeField]
    private float _ammoLifetime;

    private float time = 0f;
    private Transform _firePosition;

    private void Start()
    {
        _firePosition = GetComponent<Transform>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= _fireRate)
        {
            time = 0f;
            Shoot();
        }

    }

    private void Shoot()
    {
        var firedAmmo = Instantiate(_ammo, _firePosition.position, Quaternion.identity);
        if (firedAmmo.TryGetComponent(out Rigidbody2D firedAmmoRigidbody))
        {
            firedAmmo.StartAmmoLifetime(_ammoLifetime);
            firedAmmoRigidbody.AddForce(Vector2.left * _fireForce, ForceMode2D.Impulse);
        }
    }


}
