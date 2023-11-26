using Platformer.Player;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool _touchedDoor;
    private void Update()
    {
        if ((_touchedDoor) && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _touchedDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _touchedDoor = false;
    }
}
