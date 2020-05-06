namespace Character.Player
{
    public class PlayerRotation : CharacterRotation
    {
        protected override void Update()
        {
            TargetAngle = Rb.velocity;
            base.Update();
        }
    }
}