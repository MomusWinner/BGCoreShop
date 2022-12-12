namespace Game.Characters.Model
{
    public class FloatAnimatorSignal : AnimatorSignal<string, float>
    {
        public FloatAnimatorSignal(string variableName, float value) : base(variableName, value)
        {
        }
    }
}