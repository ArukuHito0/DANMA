using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SelectableButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip enterSe;
    [SerializeField] private AudioClip clickSe;

    [SerializeField] private bool muteEnterSE;
    [SerializeField] private bool muteClickSE;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!muteEnterSE)
            SoundUtil.PlaySe(enterSe.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!muteClickSE)
            SoundUtil.PlaySe(clickSe.name);
        OnClick();
    }

    protected abstract void OnClick();
}
