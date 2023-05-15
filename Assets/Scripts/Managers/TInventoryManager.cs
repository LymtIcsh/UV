using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TInventoryManager : TSingleton<TInventoryManager>, IPointerMoveHandler, IPointerClickHandler
{
    [SerializeField]
    private CanvasGroup cg;

    [SerializeField]
    private Image _0;

    [SerializeField]
    private Image _1;

    [SerializeField]
    private Image _2;

    [SerializeField]
    private Image _3;

    [SerializeField]
    private Image _4;

    [SerializeField]
    private Image _5;

    private Color hide = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    private Color show = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private bool isShow = false;

    public Action<int> Choose;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ResetCanvas();
        TInputManager.Instance.Compass += this.OnCompass;
        Choose += (part) =>
        {
            Debug.Log(part);
        };
    }

    private void ResetCanvas()
    {
        cg.alpha = 0.0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        ResetColor();
    }

    private void ShowCanvas()
    {
        cg.alpha = 1.0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        ResetColor();
    }

    private void ResetColor()
    {
        _0.color = hide;
        _1.color = hide;
        _2.color = hide;
        _3.color = hide;
        _4.color = hide;
        _5.color = hide;
    }

    public void OnPointerMove(PointerEventData e)
    {
        ResetColor();
        int part = (int)e.position.GetAnlgeFromPoint(new Vector2(Screen.width / 2, Screen.height / 2)) / 60;
        if (part == 0)
        {
            _0.color = show;
        }
        else if (part == 1)
        {
            _1.color = show;
        }
        else if (part == 2)
        {
            _2.color = show;
        }
        else if (part == 3)
        {
            _3.color = show;
        }
        else if (part == 4)
        {
            _4.color = show;
        }
        else if (part == 5)
        {
            _5.color = show;
        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        int part = (int)e.position.GetAnlgeFromPoint(new Vector2(Screen.width / 2, Screen.height / 2)) / 60;
        isShow = false;
        ResetCanvas();
        Choose?.Invoke(part);
    }

    private void OnCompass(TInputManager.TInputKeyType type)
    {
        if (type == TInputManager.TInputKeyType.Down)
        {
            isShow = !isShow;
            if (isShow)
            {
                ShowCanvas();
            }
            else
            {
                ResetCanvas();
            }
        }
    }
}
