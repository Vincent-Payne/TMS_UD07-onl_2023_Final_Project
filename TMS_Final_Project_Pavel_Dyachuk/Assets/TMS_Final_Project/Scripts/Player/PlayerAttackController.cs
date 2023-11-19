using Platformer.Player;
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
    private float _playerAttackDamage = 1f;

    private float _attackCooldown = 0.52f;
    private bool _isAttacking;

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
