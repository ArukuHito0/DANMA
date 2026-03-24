using UnityEngine;

public class QuitButton : SelectableButton
{
    protected override void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
