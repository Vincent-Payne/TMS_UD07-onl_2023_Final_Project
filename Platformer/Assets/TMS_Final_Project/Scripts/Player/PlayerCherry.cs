using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCherry : MonoBehaviour
{
    [SerializeField]
    private Sprite fullCherry, emptyCherry;
    private Image cherryImage;

    private void Awake()
    {
        cherryImage = GetComponent<Image>();
    }


    public void SetCherryImage(CherryStatus status)
    {
        switch (status)
        {
            case CherryStatus.Empty:
                cherryImage.sprite = emptyCherry;
                break;
            case CherryStatus.Full:
                cherryImage.sprite = fullCherry;
                break;
        }
    }
}
public enum CherryStatus
{
    Empty = 0,
    Full = 1
}
