using UnityEngine;
using AI.Base;
using AI.Configs.Archer.Fight;
using AI.Common.Roam;
using AI.Interaction;
using Utils.Math;

namespace AI.Configs.Archer
{
    public class MasterStateMachine : StateMachine
    {
        public MasterStateMachine(GameObject agent, GameObject firePoint,
                                  GameObject arrowPrefab, GameObject enemy,
                                  SpottingManager spottingManager)
        {
            RoamStateMachine = new RoamStateMachine(agent,
                                                    new Range(1.0f, 1.0f),
                                                    new Range(1.0f, 1.0f));
            FightStateMachine = new FightStateMachine(agent, firePoint, arrowPrefab, enemy);
            MergeCore(this, RoamStateMachine);
            MergeCore(this, FightStateMachine);
            var roamToFightDecision = new RoamToFightDecision(spottingManager);
            var roamToFightTransition = new Transition(roamToFightDecision, FightStateMachine.EntryState);
            RoamStateMachine.AddTransitionToAllStates(roamToFightTransition);
            EntryState = RoamStateMachine.EntryState;
        }

        public RoamStateMachine RoamStateMachine { get; private set; }
        public FightStateMachine FightStateMachine { get; private set; }
    }
}