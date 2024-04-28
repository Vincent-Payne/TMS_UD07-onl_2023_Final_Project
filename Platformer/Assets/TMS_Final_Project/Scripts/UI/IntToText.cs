using Platformer.Events;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class IntToText : MonoBehaviour
    {
        [SerializeField]
        private IntEventChannel _intEventChannel;


        private TextMeshProUGUI _text;


        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _intEventChannel.OnEventPublished += OnValueChanged;
        }

        private void OnDisable()
        {
            _intEventChannel.OnEventPublished -= OnValueChanged;
        }


        private void OnValueChanged(int value)
        {
            _text.text = value.ToString();
        }
    }
}