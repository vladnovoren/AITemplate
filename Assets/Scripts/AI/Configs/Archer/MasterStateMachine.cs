using UnityEngine;
using AI.Base;
using AI.Configs.Archer.Fight;
using AI.Common.Roam;
using AI.Interaction;

namespace AI.Configs.Archer
{
    public class MasterStateMachine : StateMachine
    {
        public MasterStateMachine(GameObject agent, GameObject firePoint,
                                  GameObject arrowPrefab, GameObject enemy,
                                  SpottingManager spottingManager)
        {
            RoamStateMachine = new RoamStateMachine(agent);
            FightStateMachine = new FightStateMachine(agent, firePoint, arrowPrefab, enemy);
            MergeCore(this, RoamStateMachine);
            MergeCore(this, FightStateMachine);
            var roamToFightDecision = new RoamToFightDecision(spottingManager);
            var roamToFightTransition = new Transition(roamToFightDecision, FightStateMachine.Entry);
            RoamStateMachine.AddTransitionToAllStates(roamToFightTransition);
            Entry = RoamStateMachine.Entry;
        }

        public RoamStateMachine RoamStateMachine { get; private set; }
        public FightStateMachine FightStateMachine { get; private set; }
    }
}