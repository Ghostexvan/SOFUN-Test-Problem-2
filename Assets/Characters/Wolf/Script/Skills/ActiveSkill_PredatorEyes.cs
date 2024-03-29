using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_PredatorEyes : Skill_Base
{
    [SerializeField]
    private string skillName = "Predator Eyes";

    [SerializeField]
    private string description = "Deals 150% PD to an enemy with lowest HP and heals by 50% damage.";

    [SerializeField]
    private float physicalDamageMultiplier = 1.5f;

    [SerializeField]
    private float healingMultiplier = 0.5f;

    public ActiveSkill_PredatorEyes()
    {

    }

    public ActiveSkill_PredatorEyes(ActiveSkill_PredatorEyes activeSkill_PredatorEyes)
    {
        this.skillName = activeSkill_PredatorEyes.skillName;
        this.description = activeSkill_PredatorEyes.description;
        this.physicalDamageMultiplier = activeSkill_PredatorEyes.physicalDamageMultiplier;
        this.healingMultiplier = activeSkill_PredatorEyes.healingMultiplier;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_PredatorEyes(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        GameObject target = enemies[0];

        foreach (GameObject enemy in enemies)
        {
            CharacterActionController controller = enemy.GetComponent<CharacterActionController>();
            CharacterActionController targetController = target.GetComponent<CharacterActionController>();

            if (targetController.GetCharacterData().healthPoint.Value > controller.GetCharacterData().healthPoint.Value)
            {
                target = enemy;
            }
        }

        return new List<CharacterActionController>(){
            target.GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach (CharacterActionController target in targetList)
        {
            if (target.DealPhysicalDamage(caster.GetCharacterData().physicalDamage.Value * physicalDamageMultiplier, caster.GetCharacterData().moveSpeed.Value))
            {
                caster.ChangeCurrentHealthPoint(
                    (
                        caster.GetCharacterData().physicalDamage.Value * physicalDamageMultiplier - target.GetCharacterData().physicalResistance.Value
                    ) * healingMultiplier
                );
            };
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}
