using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Dictionary<string, GameObject> _uiPanels = new Dictionary<string, GameObject>();
    private Transform _uiCanvasRoot;

    private void Awake()
    {
        Instance = this;
        _uiCanvasRoot = GameObject.Find("Canvas")?.transform;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenPanel(string addressableKey, System.Action<GameObject> callback)
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
                _uiPanels[addressableKey] = handle.Result;
                callback?.Invoke(handle.Result);
            }
        };
    }

    public void ClosePanel(string addressableKey)
    {
        if (_uiPanels.TryGetValue(addressableKey, out GameObject panel))
        {
            _uiPanels.Remove(addressableKey);
            Addressables.ReleaseInstance(panel);
        }
    }
}