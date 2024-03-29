using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_AbundantCrops : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_AbundantCrops activeSkill_AbundantCrops = new ActiveSkill_AbundantCrops();

    public override Skill_Base GetSkillInstance() => activeSkill_AbundantCrops.GetSkillInstance();
}
