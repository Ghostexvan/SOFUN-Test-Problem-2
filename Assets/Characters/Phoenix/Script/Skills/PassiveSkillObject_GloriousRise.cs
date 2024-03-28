using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_GloriousRise : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_GloriousRise passiveSkill_GloriousRise = new PassiveSkill_GloriousRise();

    public override Skill_Base GetSkillInstance() => passiveSkill_GloriousRise.GetSkillInstance();
}
