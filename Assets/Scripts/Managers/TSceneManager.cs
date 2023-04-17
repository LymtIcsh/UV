using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.InputSystem;

public class TSceneManager : TSingleton<TSceneManager>
{
    [SerializeField]
    private CanvasGroup cg;

    [SerializeField]
    private Image processImage;

    [SerializeField]
    private Image currentImage;
    public string currentSceneName;

    public Text enterText;

    public Text enterPercent;

    public List<TSceneEntity> scenes;

    private void Update()
    {
        if (Keyboard.current.tKey.wasReleasedThisFrame)
        {
            LoadScene(TSceneKey.World, () => { }, false);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ResetCanvas();
    }

    private void ResetCanvas()
    {
        enterPercent.text = "0.0%";
        enterText.enabled = false;
        cg.alpha = 0.0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    private void ShowCanvas()
    {
        enterPercent.text = "0.0%";
        enterText.enabled = false;
        cg.alpha = 1.0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private string GetNameByKey(TSceneKey key)
    {
        var s = scenes.Find(s => s.key == key);
        return s != null ? s.name : "Start";
    }

    private IEnumerator SwitchScene(TSceneKey key, Action OnFinish, bool autoEnter)
    {
        ShowCanvas();
        string name = GetNameByKey(key);
        Rect parentRect = processImage.rectTransform.rect;
        if (name == currentSceneName)
        {
            yield break;
        }
        else
        {
            currentSceneName = name;
            var load = SceneManager.LoadSceneAsync(name);
            load.allowSceneActivation = false;
            while (!load.isDone)
            {
                if (load.progress < 0.9f)
                {
                    enterPercent.text = $"{load.progress * 100.0f}";
                    currentImage.rectTransform.sizeDelta = new Vector2(parentRect.width * load.progress, parentRect.height);
                }
                else
                {
                    currentImage.rectTransform.sizeDelta = parentRect.size;
                }

                if (load.progress >= 0.9f)
                {
                    enterPercent.text = "100.0%";
                    if (autoEnter)
                    {
                        yield return new WaitForSeconds(1.0f);
                        load.allowSceneActivation = true;
                        yield return null;
                        OnFinish();
                        ResetCanvas();
                        yield break;
                    }
                    else
                    {
                        enterText.enabled = true;
                        if (Keyboard.current.spaceKey.isPressed)
                        {
                            load.allowSceneActivation = true;
                            yield return null;
                            OnFinish();
                            ResetCanvas();
                            yield break;
                        }

                    }
                }
                yield return null;
            }
        }
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="key">场景枚举</param>
    /// <param name="Enter">是否加载完成后自动进入</param>
    /// <param name="OnFinish">加载完成后回调</param>
    public void LoadScene(in TSceneKey key, in Action OnFinish, in bool autoEnter)
    {
        StartCoroutine(SwitchScene(key, OnFinish, autoEnter));
    }
}

[System.Serializable]
public class TSceneEntity
{
    [Tooltip("关卡名称")]
    public string name;

    [Tooltip("关卡枚举")]
    public TSceneKey key;

}

public enum TSceneKey
{
    /// <summary>
    /// 主菜单
    /// </summary>
    [Tooltip("主菜单")]
    Start,
    /// <summary>
    /// 主场景
    /// </summary>
    [Tooltip("主世界")]
    World,
}
