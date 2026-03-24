using UnityEngine;
using UnityEngine.Events;

public class ClickableButton : SelectableButton
{
    [SerializeField] private UnityEvent onClick;

    protected override void OnClick()
    {
        onClick?.Invoke();
    }
}
