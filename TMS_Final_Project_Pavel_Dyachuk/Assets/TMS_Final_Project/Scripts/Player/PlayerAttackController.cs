using Platformer.Enemies;
using Platformer.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackArea;

    [SerializeField]
    private PlayerView _playerView;

    [SerializeField]
    private int _playerAttackDamage = 1;

    private void OnEnable()
    {
        _attackArea.GetComponent<PlayerAttackArea>().OnEnemyAttacked += DamageEnemy;
    }

    private void OnDisable()
    {
        _attackArea.GetComponent<PlayerAttackArea>().OnEnemyAttacked -= DamageEnemy;
    }

    private float _attackCooldown = 0.52f;
    private bool _isAttacking;
    public int Damage => _playerAttackDamage;
    public void PlayerAttack()
    {
        if (!_isAttacking)
        {
            _playerView.PlayAttackAnimation(OnAttack1AnimationFinished);
            _attackArea.SetActive(true);
            _isAttacking = true;
            StartCoroutine(DisableAttackCooldown(_attackCooldown));
            SoundManager.Sound_Manager.PlaySwordSwingSound();
        }
        else return;
    }

    private void DamageEnemy(Collider2D enemyCollider)
    {
        enemyCollider.GetComponent<Enemy>().EnemyTakeDamage(_playerAttackDamage);
    }

    private IEnumerator DisableAttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        _isAttacking = false;
        _attackArea.SetActive(false);
    }

    private void OnAttack1AnimationFinished()
    {
    }
}
