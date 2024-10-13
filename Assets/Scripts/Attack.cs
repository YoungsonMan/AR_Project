using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator animator;
    int hashAttackCount = Animator.StringToHash("AttackCount");
    void Start()
    {
        TryGetComponent(out animator);
        
    }

    
    void Update()
    {
        
    }

    public int AttackCount
    {
        get => animator.GetInteger(hashAttackCount);
        set => animator.SetInteger(hashAttackCount, value);
    }
}
