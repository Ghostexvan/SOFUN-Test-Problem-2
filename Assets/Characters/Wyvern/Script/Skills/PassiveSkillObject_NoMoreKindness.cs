using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_NoMoreKindness : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_NoMoreKindness passiveSkill_NoMoreKindness = new PassiveSkill_NoMoreKindness();

    public override Skill_Base GetSkillInstance() => passiveSkill_NoMoreKindness.GetSkillInstance();
}
