using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ActiveSkill_TalentedStrategist : Skill_Base
{
    [SerializeField]
    private string skillName = "Talented Strategist";

    [SerializeField]
    private string description = "Deals 200% MD to the target.";

    [SerializeField]
    private float magicalDamageMultiplier = 2f;

    public ActiveSkill_TalentedStrategist(){

    }

    public ActiveSkill_TalentedStrategist(ActiveSkill_TalentedStrategist activeSkill_TalentedStrategist){
        this.skillName = activeSkill_TalentedStrategist.skillName;
        this.description = activeSkill_TalentedStrategist.description;
        this.magicalDamageMultiplier = activeSkill_TalentedStrategist.magicalDamageMultiplier;
    }
 
    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_TalentedStrategist(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = new List<GameObject>(
            GameController.Instance.GetEnemiesTeam(caster.gameObject)
        );

        return new List<CharacterActionController>(){
            enemies[UnityEngine.Random.Range(0, enemies.Count)].GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.DealMagicalDamage(caster.GetCharacterData().physicalDamage.Value * magicalDamageMultiplier, caster.GetCharacterData().moveSpeed.Value);
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}
