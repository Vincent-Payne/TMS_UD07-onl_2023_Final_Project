using Platformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarManager : MonoBehaviour
{
    private float _currentHealth, _maxHealth;
    [SerializeField]
    private GameObject _heartPrefab;

    private List<PlayerHealthHeart> hearts = new List<PlayerHealthHeart>();

    private void OnEnable()
    {
        PlayerController.OnCurrentHealthChanged += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerController.OnCurrentHealthChanged -= DrawHearts;
    }
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    public void DrawHearts()
    {
        ClearHealthBar();
        //Determine how much hearts to draw
        float maxHealthRemainder = _maxHealth % 2;
        int heartsToMake = (int)((_maxHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(_currentHealth - (i*2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(_heartPrefab);
        newHeart.transform.SetParent(transform);

        PlayerHealthHeart heartComponent = newHeart.GetComponent<PlayerHealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHealthBar()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<PlayerHealthHeart>();
    }

}
