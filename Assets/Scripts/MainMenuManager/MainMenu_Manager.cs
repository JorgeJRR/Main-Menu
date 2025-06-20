using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.EditorTools;
using TMPro;
using UnityEngine.InputSystem;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField]
    private RectTransform settingsPanel;
    [SerializeField]
    private CanvasGroup mainPanel;
    [SerializeField]
    private RectTransform gameTitle;
    [SerializeField]
    private TextMeshProUGUI pressAnyButton;

    private InputAction anyKeyAction;

    private Tween blinkTween;

    private Vector2 closedOffsetMin = new Vector2(0, 1080);
    private Vector2 closedOffsetMax = new Vector2(0, 1080);

    private Vector2 openOffsetMin = new Vector2(0, 0);
    private Vector2 openOffsetMax = new Vector2(0, 0);

    private Vector2 titleInitOffsetMin = new Vector2(0, 430);
    private Vector2 titleInitOffsetMax = new Vector2(0, 430);

    private Vector2 titleFinalOffsetMin = new Vector2(0, 0);
    private Vector2 titleFinalOffsetMax = new Vector2(0, 0);

    [SerializeField]
    private float mainPanelFadeTime;
    [SerializeField]
    private float mainPanelStartAlpha;
    [SerializeField]
    private float mainPanelFinalAlpha;

    private void Awake()
    {
        // Detecta cualquier tecla presionada
        anyKeyAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/anyKey");
        anyKeyAction.performed += ctx => StopBlinkingText();
    }

    private void OnEnable()
    {
        anyKeyAction.Enable();
    }

    private void OnDisable()
    {
        anyKeyAction.Disable();
    }

    private void Start()
    {
        gameTitle.offsetMin = titleInitOffsetMin;
        gameTitle.offsetMax = titleInitOffsetMax;

        settingsPanel.offsetMin = closedOffsetMin;
        settingsPanel.offsetMax = closedOffsetMax;
        settingsPanel.gameObject.SetActive(false);

        mainPanel.alpha = mainPanelStartAlpha;
        mainPanel.interactable = false;
        mainPanel.blocksRaycasts = false;

        StartBlinkingText();
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

    public void InitMainPanel()
    {
        gameTitle.DOOffsetMin(titleFinalOffsetMin, 0.5f).SetEase(Ease.OutCubic);
        gameTitle.DOOffsetMax(titleFinalOffsetMax, 0.5f).SetEase(Ease.OutCubic);

        mainPanel.DOFade(mainPanelFinalAlpha, mainPanelFadeTime).SetDelay(0.5f).SetEase(Ease.InOutQuad)
                  .OnStart(() => {
                      mainPanel.interactable = true;
                      mainPanel.blocksRaycasts = true;
                  });
    }

    public void StartBlinkingText()
    {
        blinkTween = pressAnyButton.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public void StopBlinkingText()
    {
        if (blinkTween != null && blinkTween.IsActive())
        {
            blinkTween.Kill();
            Color c = pressAnyButton.color;
            c.a = 0f;
            pressAnyButton.color = c;
        }
        anyKeyAction.Disable();
        InitMainPanel();
    }
}