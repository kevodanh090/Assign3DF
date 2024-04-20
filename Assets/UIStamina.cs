using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIStamina : MonoBehaviour
{
    private Slider slider;
    // Varriable to scale bar size  depending on stat (Higher stat =  longer bar across screen)
    // Secondary bar behind may bar for polish effect (Yellow bar that shows how much  an action/damge take away form current stat)

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public virtual void SetStat()
    {

    }
    public virtual void SetMaxStat()
    {

    }
}
