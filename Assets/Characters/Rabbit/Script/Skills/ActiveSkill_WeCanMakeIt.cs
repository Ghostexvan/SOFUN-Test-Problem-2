using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_WeCanMakeIt : Skill_Base
{
    [SerializeField]
    private string skillName = "We Can Make IT";

    [SerializeField]
    private string description = "Heal all allies by 25% own HP and inflicts HASTE effect.";

    [SerializeField]
    private float healthPointMultiplier = 0.25f;

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    public ActiveSkill_WeCanMakeIt(){

    }

    public ActiveSkill_WeCanMakeIt(ActiveSkill_WeCanMakeIt activeSkill_WeCanMakeIt){
        this.skillName = activeSkill_WeCanMakeIt.skillName;
        this.description = activeSkill_WeCanMakeIt.description;
        this.healthPointMultiplier = activeSkill_WeCanMakeIt.healthPointMultiplier;
        this.effectList = activeSkill_WeCanMakeIt.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_WeCanMakeIt(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        List<CharacterActionController> alliesController = new List<CharacterActionController>();

        foreach (GameObject ally in allies){
            alliesController.Add(ally.GetComponent<CharacterActionController>());
        }

        return alliesController;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.ChangeCurrentHealthPoint(caster.GetCharacterData().healthPoint.Value * healthPointMultiplier);
            foreach(EffectObject_Base effect in effectList){
                    target.AddActiveEffect(effect.GetEffectInstance());
                }
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}
