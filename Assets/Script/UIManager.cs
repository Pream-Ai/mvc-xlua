using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    private Dictionary<string,GameObject>_uiPanels=new Dictionary<string, GameObject>();
    private Transform _uiCanvasRoot;
    private void Awake()
    {
        instance = this;
        _uiCanvasRoot = GameObject.Find("Canvas")?.transform;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }
    /// <summary>
    /// 异步打开窗口
    /// </summary>
    public void OpenPanel(string addressableKey,Action<GameObject> callback)
    {
        if (_uiPanels.ContainsKey(addressableKey))
        {
            _uiPanels[addressableKey].SetActive(true);
            callback?.Invoke(_uiPanels[addressableKey]);
            return;
        }
        Addressables.InstantiateAsync(addressableKey, _uiCanvasRoot).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject panel=handle.Result;
                _uiPanels[addressableKey] = panel;
                callback?.Invoke(panel);
            }
            else
            {
                Debug.LogError($"[UImanager]加载失败:{addressableKey}");
            }
        };
    }
    /// <summary>
    /// 关闭释放窗口
    /// </summary>
    public void ClosePanel(string addressableKey)
    {
        if (_uiPanels.TryGetValue(addressableKey,out GameObject panel))
        {
            _uiPanels.Remove(addressableKey);
            Addressables.Release(panel);
        }
    }
}
//为什么要异步打开，这个库是什么意思，怎么用，为什么最后用out
