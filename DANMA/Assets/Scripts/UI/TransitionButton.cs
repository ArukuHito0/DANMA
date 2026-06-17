using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransitionButton : SelectableButton
{
    [SerializeField] private string transitionSceneName;

    protected override void OnClick()
    {
        SceneTransitionManager.Instance.OnLoadScendClicked(transitionSceneName);
    }
}
