using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public virtual void Init()
    {
        GeneralManager.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        GeneralManager.UI.ClosePopupUI(this);
    }
}
