using UnityEngine;
using UnityEngine.TextCore.Text;

namespace StateMachine.CharacterState
{
    public class Follow : IState<Character>
    {
        private readonly Character leader;

        public Follow(Character character)
        {
            leader = character;
        }

        public void Enter(Character entity)
        {
            Debug.Log(entity + " :Enter");
        }

        public void Exit(Character entity)
        {
            Debug.Log(entity + " :Exit");
            entity.navMeshAgent.ResetPath();
        }

        public void Update(Character entity)
        {
            Debug.Log(entity + " :Update");
            entity.navMeshAgent.SetDestination(leader.transform.position);
        }
    }
}
