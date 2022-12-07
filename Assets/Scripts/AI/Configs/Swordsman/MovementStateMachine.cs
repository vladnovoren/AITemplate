using UnityEngine;
using AI.Base;
using AI.Movement.Chase;
using AI.Movement.Roam;

namespace AI.Configs.Swordsman
{
    public class MovementStateMachine : StateMachine
    {
        public MovementStateMachine(GameObject agent, GameObject enemy)
        {
            RoamStateMachine = new RoamStateMachine(agent);
            ChaseStateMachine = new ChaseStateMachine(agent, enemy);

            var toChaseDecision = new ToChaseDecision(agent, enemy);
            var toChaseTransition = new Transition(toChaseDecision, ChaseStateMachine.ChaseState);
            RoamStateMachine.AddTransitionToAllStates(toChaseTransition);

            MergeCore(this, RoamStateMachine);
            MergeCore(this, ChaseStateMachine);
            Entry = RoamStateMachine.Entry;
        }

        public RoamStateMachine RoamStateMachine { get; private set; }
        public ChaseStateMachine ChaseStateMachine { get; private set; }
    }
}