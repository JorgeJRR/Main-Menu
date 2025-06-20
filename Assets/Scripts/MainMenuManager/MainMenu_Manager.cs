using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField]
    private RectTransform settingsPanel;

    private Vector2 closedOffsetMin = new Vector2(0, 1080);
    private Vector2 closedOffsetMax = new Vector2(0, 1080);
    private Vector2 openOffsetMin = new Vector2(0, 0);
    private Vector2 openOffsetMax = new Vector2(0, 0);

    private void Start()
    {
        // Inicializa en estado cerrado
        settingsPanel.offsetMin = closedOffsetMin;
        settingsPanel.offsetMax = closedOffsetMax;
        settingsPanel.gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.gameObject.SetActive(true);

        settingsPanel.DOOffsetMin(openOffsetMin, 0.5f).SetEase(Ease.OutCubic);
        settingsPanel.DOOffsetMax(openOffsetMax, 0.5f).SetEase(Ease.OutCubic);
    }

    public void CloseSettings()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsPanel.DOOffsetMin(closedOffsetMin, 0.5f).SetEase(Ease.InCubic));
        sequence.Join(settingsPanel.DOOffsetMax(closedOffsetMax, 0.5f).SetEase(Ease.InCubic));
        sequence.OnComplete(() => settingsPanel.gameObject.SetActive(false));
    }
}