using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDash : MonoBehaviour
{
    public bool valueSwitch = false;
    public void Switcher() => valueSwitch = !valueSwitch;
}
