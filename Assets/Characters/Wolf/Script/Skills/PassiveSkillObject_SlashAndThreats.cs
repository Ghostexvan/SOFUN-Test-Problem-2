using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_SlashAndThreats : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_SlashAndThreats passiveSkill_SlashAndThreats = new PassiveSkill_SlashAndThreats();

    public override Skill_Base GetSkillInstance() => passiveSkill_SlashAndThreats.GetSkillInstance();
}
