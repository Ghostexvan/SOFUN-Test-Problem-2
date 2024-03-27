using System.Collections.Generic;
using UnityEngine;

abstract public class Skill_Base
{
    public abstract Skill_Base GetSkillInstance();
    public abstract bool CheckCondition(CharacterActionController caster);

    public abstract List<CharacterActionController> GetTargetList(CharacterActionController caster);

    public abstract void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList);
}
