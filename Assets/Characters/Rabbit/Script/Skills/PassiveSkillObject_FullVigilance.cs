using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_FullVigilance : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_FullVigilance passiveSkill_FullVigilance = new PassiveSkill_FullVigilance();

    public override Skill_Base GetSkillInstance() => passiveSkill_FullVigilance.GetSkillInstance();
}
