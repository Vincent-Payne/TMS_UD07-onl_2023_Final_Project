using Platformer;
using Platformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCherryBarManager : MonoBehaviour
{
    private int _currentCherry, _maxCherry;
    [SerializeField]
    private GameObject _cherryPrefab;

    private List<PlayerCherry> cherries = new List<PlayerCherry>();

    private void OnEnable()
    {
        GameManager.OnCurrentCherryChanged += DrawCherries;
    }

    private void OnDisable()
    {
        GameManager.OnCurrentCherryChanged -= DrawCherries;
    }
    public int CurrentCherry
    {
        get { return _currentCherry; }
        set { _currentCherry = value; }
    }
    public int MaxCherry
    {
        get { return _maxCherry; }
        set { _maxCherry = value; }
    }

    public void DrawCherries()
    {
        //Debug.Log(_currentCherry);
        ClearCherryBar();
        for (int i = 0; i < _maxCherry; i++)
        {
            CreateEmptyCherry();
        }

        for (int i = 0; i < cherries.Count; i++)
        {
            int cherryStatusRemainder = (int)Mathf.Clamp(_currentCherry - (i), 0, 1);
            cherries[i].SetCherryImage((CherryStatus)cherryStatusRemainder);
        }
    }
    public void CreateEmptyCherry()
    {
        GameObject newCherry = Instantiate(_cherryPrefab);
        newCherry.transform.SetParent(transform);

        PlayerCherry cherrieComponent = newCherry.GetComponent<PlayerCherry>();
        cherrieComponent.SetCherryImage(CherryStatus.Empty);
        cherries.Add(cherrieComponent);
    }

    public void ClearCherryBar()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        cherries = new List<PlayerCherry>();
    }
}

