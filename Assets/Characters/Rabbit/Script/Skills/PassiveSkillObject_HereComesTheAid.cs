using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_HereComesTheAid : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_HereComesTheAid passiveSkill_HereComesTheAid = new PassiveSkill_HereComesTheAid();

    public override Skill_Base GetSkillInstance() => passiveSkill_HereComesTheAid.GetSkillInstance();
}
