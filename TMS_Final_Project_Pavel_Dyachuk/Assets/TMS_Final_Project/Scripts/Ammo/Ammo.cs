using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Common;

public class Ammo: MonoBehaviour, IDamageSource
{
    [SerializeField]
    private int _damage;

    public int Damage => _damage;
    public void StartAmmoLifetime(float ammoLifetime)
    {
        StartCoroutine(DestroyAmmo(ammoLifetime));
    }
    private IEnumerator DestroyAmmo(float ammolifitime)
    {
        yield return new WaitForSeconds(ammolifitime);
        Destroy(this.gameObject);
    }
}
