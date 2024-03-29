using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_AbundantCrops : Skill_Base
{
    [SerializeField]
    private string skillName = "Abundant Crops";

    [SerializeField]
    private string description = "Heals all allies by 150% MD.";

    [SerializeField]
    private float magicalDamageMultiplier = 1.5f;

    public ActiveSkill_AbundantCrops(){

    }

    public ActiveSkill_AbundantCrops(ActiveSkill_AbundantCrops activeSkill_AbundantCrops){
        this.skillName = activeSkill_AbundantCrops.skillName;
        this.description = activeSkill_AbundantCrops.description;
        this.magicalDamageMultiplier = activeSkill_AbundantCrops.magicalDamageMultiplier;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_AbundantCrops(this);
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
            target.ChangeCurrentHealthPoint(caster.GetCharacterData().magicalDamage.Value * magicalDamageMultiplier);
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}
