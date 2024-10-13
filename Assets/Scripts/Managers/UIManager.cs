using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Dictionary<Type, PopupBase> popupDict = new Dictionary<Type, PopupBase>();

    [SerializeField] private GameObject hudPrefab;
    [SerializeField] private GameObject characterChangePopupPrefab;
    [SerializeField] private GameObject nameChangePopupPrefab;
    [SerializeField] private GameObject checkActiveUsersPopupPrefab;
    [SerializeField] private GameObject npcDialogPopupPrefab;

    private Transform uiRoot;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (uiRoot == null)
            uiRoot = new GameObject("UIRoot").transform;
    }

    private GameObject PopupInstaniate<T>()
    {
        Type type = typeof(T);
        GameObject go = null;
        if (type == typeof(Hud))
        {
            go = Instantiate(hudPrefab, uiRoot);
        }
        else if (type == typeof(CharacterChangePopup))
        {
            go = Instantiate(characterChangePopupPrefab, uiRoot);
        }
        else if (type == typeof(NameChangePopup))
        {
            go = Instantiate(nameChangePopupPrefab, uiRoot);
        }
        else if (type == typeof(UsersPopup))
        {
            go = Instantiate(checkActiveUsersPopupPrefab, uiRoot);
        }
        else if (type == typeof(NpcDialogPopup))
        {
            go = Instantiate(npcDialogPopupPrefab, uiRoot);
        }

        if (go != null) go.name = type.Name;

        return go;
    }

    public T ShowPopup<T>() where T : PopupBase
    {
        Type type = typeof(T);

        if (popupDict.TryGetValue(type, out PopupBase ui) == false)
        {
            GameObject go = PopupInstaniate<T>();
            ui = go.GetComponent<T>();
            popupDict.Add(type, ui);
        }

        ui.Show();
        return ui as T;
    }

    public void ClosePopup(GameObject go)
    {
        go.GetComponent<PopupBase>()?.Close();
    }

    public void ClosePopup<T>()
    {
        if (popupDict.TryGetValue(typeof(T), out PopupBase ui))
        {
            ui.Close();
        }
    }

    public void CloseAllPopup(PopupBase popup = null)
    {
        foreach (var ui in popupDict)
        {
            if (popup != null && ui.Key == popup.GetType())
                continue;

            ui.Value.Close();
        }
    }

    public bool IsPopupOpened<T>()
    {
        if (popupDict.TryGetValue(typeof(T), out PopupBase ui))
        {
            return ui.IsOpen;
        }

        return false;
    }
}
