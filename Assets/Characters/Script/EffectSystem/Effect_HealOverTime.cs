using System;
using UnityEngine;

[Serializable]
public class Effect_HealOverTime : Effect_Base {
    [SerializeField]
    private int duration = 10;

    [SerializeField]
    private float healAmount = 5f;

    [SerializeField]
    private int turnPassed = 0;

    public Effect_HealOverTime() {

    }

    public Effect_HealOverTime(Effect_HealOverTime hot){
        duration = hot.duration;
        healAmount = hot.healAmount;
    }

    public override Effect_Base GetEffectInstance(){
        return new Effect_HealOverTime(this);
    }

    public override bool ProcessEffect(CharacterActionController to)
    {
        Debug.Log("Healing", to.gameObject);
        to.ChangeCurrentHealthPoint(healAmount);
        turnPassed += 1;
        return turnPassed >= duration;
    }
}
