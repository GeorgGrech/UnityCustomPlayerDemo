using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
   public Animation damageAnimation;
   public void PlayDamageAnimation()
    {
        Debug.Log("Arrived in DamageAnimation");
        damageAnimation.Play();
    }
}
