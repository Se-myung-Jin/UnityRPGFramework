﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 0;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _scene = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "UI_Root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GeneralManager.Resource.Instanciate($"UI/Scene/{name}");
        T scene = Util.GetOrAddComponent<T>(go);
        _scene = scene;

        

        go.transform.SetParent(Root.transform);

        return scene;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GeneralManager.Resource.Instanciate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0) return;

        UI_Popup popup = _popupStack.Pop();
        GeneralManager.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0) return;

        if (_popupStack.Peek() != popup) return;

        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
}