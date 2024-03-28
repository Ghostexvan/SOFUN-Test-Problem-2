using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_EncouragingDance : Skill_Base
{
    [SerializeField]
    private string skillName = "Encouraging Dance";

    [SerializeField]
    private string description = "Increases MR of an ally with the lowest HP by 25% of own MR, once per battle.";

    [SerializeField]
    private bool isUsed = false;

    [SerializeField]
    private float magicalResistanceMultiplier = 0.25f;

    public PassiveSkill_EncouragingDance(){

    }

    public PassiveSkill_EncouragingDance(PassiveSkill_EncouragingDance passiveSkill_EncouragingDance){
        this.skillName = passiveSkill_EncouragingDance.skillName;
        this.description = passiveSkill_EncouragingDance.description;
        this.isUsed = passiveSkill_EncouragingDance.isUsed;
        this.magicalResistanceMultiplier = passiveSkill_EncouragingDance.magicalResistanceMultiplier;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && !isUsed;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_EncouragingDance(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        GameObject target = allies[0];

        foreach (GameObject ally in allies){
            CharacterActionController controller = ally.GetComponent<CharacterActionController>();
            CharacterActionController targetController = target.GetComponent<CharacterActionController>();

            if (targetController.GetCharacterData().healthPoint.Value > controller.GetCharacterData().healthPoint.Value){
                target = ally;
            }
        }

        return new List<CharacterActionController>(){
            target.GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.GetCharacterData().magicalResistance.AddModifier(
                new StatModifier(
                    caster.GetCharacterData().magicalResistance.Value * magicalResistanceMultiplier,
                    StatModType.PercentAdd,
                    this
                )
            );
        }

        isUsed = true;
    }
}
