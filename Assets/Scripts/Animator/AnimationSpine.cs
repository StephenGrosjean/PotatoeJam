﻿using UnityEngine;
using Spine.Unity;

public class AnimationSpine : StateMachineBehaviour {

    [SerializeField] private string animName;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private string parameterToEnable;
    [SerializeField] private bool enableParameterOnPlay;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        SkeletonAnimation Anim = animator.GetComponentInChildren<SkeletonAnimation>();
        if (enableParameterOnPlay) {
            animator.SetBool(parameterToEnable, true);
        }
        Anim.AnimationState.SetAnimation(0, animName, false).TimeScale = speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
      override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enableParameterOnPlay) {
            animator.SetBool(parameterToEnable, false);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
