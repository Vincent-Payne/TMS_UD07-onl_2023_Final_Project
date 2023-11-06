using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Sound_Manager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _levelSound;
    [SerializeField]
    private AudioClip _playerJumpSound;
    [SerializeField]
    private AudioClip _playerHitByEnemySound;
    [SerializeField]
    private AudioClip _pickGemSound;
    [SerializeField]
    private AudioClip _pickCherrySound;
    [SerializeField]
    private AudioClip _playerDeathSound;
    [SerializeField]
    private AudioClip _trampolineSound;
    [SerializeField]
    private AudioClip _enemyDeathSound;

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
    public void TurnOffSoundEffects()
    {
        _soundEffectsSwitch = false;
    }
    public void TurnOnSoundEffects()
    {
        _soundEffectsSwitch = true;
    }
}
