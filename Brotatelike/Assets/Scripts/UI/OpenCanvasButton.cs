using UnityEngine;

public class OpenCanvasButton : SelectableButton
{
    [SerializeField] private Canvas targetCanvas;

    protected override void OnClick()
    {
        if(targetCanvas == null) return;

        targetCanvas.enabled = true;
    }
}
