namespace StateMachine.CharacterState
{
    public class Follow : IState<Character>
    {
        private readonly Character character;

        public Follow(Character character)
        {
            this.character = character;
        }

        public void Enter(Character entity)
        {
            throw new System.NotImplementedException();
        }

        public void Exit(Character entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Character entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
