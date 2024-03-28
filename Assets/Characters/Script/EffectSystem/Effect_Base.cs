abstract public class Effect_Base
{
    public abstract bool ProcessEffect(CharacterActionController to);

    public abstract Effect_Base GetEffectInstance(CharacterActionController caster = null);
}
