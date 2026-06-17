using UnityEngine;
using UnityEngine.Events;

public class ClickableButton : SelectableButton
{
    [SerializeField] private UnityEvent onClick;
    public UnityEvent _OnClick => onClick;

    protected override void OnClick()
    {
        onClick?.Invoke();
    }
}
