using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelAnimation : MonoBehaviour
{
    [SerializeField] RectTransform yourScoreRectTransform;
    [SerializeField] RectTransform totalScoreRectTransform;
    [SerializeField] RectTransform panelRectTransform;

    void OnEnable()
    {
        yourScoreRectTransform.localScale = Vector3.zero;
        totalScoreRectTransform.localScale = Vector3.zero;
        panelRectTransform.localPosition = new Vector3(0, -700, 0);

        LeanTween.scale(yourScoreRectTransform, Vector3.one, 0.2f)
            .setEaseInOutBack()
            .setIgnoreTimeScale(true);

        LeanTween.scale(totalScoreRectTransform, Vector3.one, 0.2f)
            .setEaseInOutBack()
            .setDelay(0.3f)
            .setIgnoreTimeScale(true);

        LeanTween.move(panelRectTransform, new Vector3(0, -100, 0), 0.2f)
            .setEaseOutCubic()
            .setDelay(0.6f)
            .setIgnoreTimeScale(true);
    }
}