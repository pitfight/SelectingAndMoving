using UnityEngine;

namespace StateMachine.CharacterState
{
    public class Leader : IState<Character>
    {
        private readonly Character character;

        public Leader(Character character)
        {
            this.character = character;
        }

        public void Enter(Character entity)
        {
            Debug.Log(entity + " :Enter");
            entity.SetLeaderMaterial();
        }

        public void Exit(Character entity)
        {
            Debug.Log(entity + " :Exit");
            entity.navMeshAgent.ResetPath();
        }

        public void Update(Character entity)
        {
            Debug.Log(entity + " :Update");
            entity.navMeshAgent.SetDestination(entity.CurrentDestination);
        }
    }
}
