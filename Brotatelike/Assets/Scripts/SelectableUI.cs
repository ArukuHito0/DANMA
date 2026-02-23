using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent pointerEnter;
    [SerializeField] private UnityEvent pointerExit;
    [SerializeField] private UnityEvent pointerClick;

    public void OnPointerEnter(PointerEventData eventData) { pointerEnter?.Invoke(); }

    public void OnPointerExit(PointerEventData eventData) { pointerExit?.Invoke(); }

    public void OnPointerClick(PointerEventData eventData) { pointerClick?.Invoke(); }
}
