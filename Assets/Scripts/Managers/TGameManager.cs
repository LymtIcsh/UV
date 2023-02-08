using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGameManager : TSingleton<TGameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        InitSetting();
    }

    private void Start()
    {
        ListenEvents();
    }

    private void InitSetting()
    {
        Application.targetFrameRate = 144;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ListenEvents()
    {
        TInputManager.Instance.Slow += OnSlow;
    }

    private void OnSlow(TInputManager.TInputKeyType type)
    {
        Time.timeScale = type == TInputManager.TInputKeyType.Down ? 0.5f : 1.0f;
    }

}
