using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Animator animator;
  public void SettingsPageOpen (Animator animator) => animator.SetBool("isSettingsPageOpen", true);
  public void SettingsPageClose (Animator animator) => animator.SetBool("isSettingsPageOpen", false);
}
