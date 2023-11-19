using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Sound_Manager;


    [Header("World")]

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _levelSound;

    [SerializeField]
    private AudioClip _pickGemSound;

    [SerializeField]
    private AudioClip _pickCherrySound;

    [SerializeField]
    private AudioClip _trampolineSound;


    [Header("Player")]

    [SerializeField]
    private AudioClip _playerJumpSound;

    [SerializeField]
    private AudioClip _playerHitByEnemySound;

    [SerializeField]
    private AudioClip _playerDeathSound;

    [SerializeField]
    private AudioClip _playerSwordSwingSound;

    [SerializeField]
    private AudioClip _playerUseCherrySound;


    [Header("Enemies")]

    [SerializeField]
    private AudioClip _enemyDeathSound;

    [SerializeField]
    private AudioClip[] _enemyBatDeathSound;

    [SerializeField]
    private AudioClip _enemyHitSound;

    [SerializeField]
    private AudioClip[] _enemyBatHitSound;

    private bool _soundEffectsSwitch = true;
    private bool _musicSwitch = true;

    private void Awake()
    {
        Sound_Manager = this;
    }

    public void PlayLevelSound()
    {
        
    }
    public void PlayPlayerJumpSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_playerJumpSound); }
    }
    public void PlayPlayerHitByEnemySound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_playerHitByEnemySound); }
    }
    public void PlayPickGemSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_pickGemSound); }
    }
    public void PlayPickCherrySound()
    {
        if(_soundEffectsSwitch) { _audioSource.PlayOneShot(_pickCherrySound);}
    }
    public void PlayPlayerUseCherrySound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_playerUseCherrySound); }
    }
    public void PlayPlayerDeathSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_playerDeathSound);}
    }
    public void PlayTrampolineSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_trampolineSound); }
    }
    public void PlayEnemyDeathSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_enemyDeathSound); }
    }
    public void PlayEnemyHitSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_enemyHitSound); }
    }
    public void PlayEnemyBatDeathSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_enemyBatDeathSound[Random.Range(0, 3)]); }
    }
    public void PlayEnemyBatHitSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_enemyBatHitSound[Random.Range(0, 2)]); }
    }
    public void PlaySwordSwingSound()
    {
        if (_soundEffectsSwitch) { _audioSource.PlayOneShot(_playerSwordSwingSound); }
    }

    public void TurnOffSoundEffects()
    {
        _soundEffectsSwitch = false;
    }
    public void TurnOnSoundEffects()
    {
        _soundEffectsSwitch = true;
    }
}
