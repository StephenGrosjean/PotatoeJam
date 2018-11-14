using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the animations for the player are triggered from here
/// </summary>

public class AnimatorNames : MonoBehaviour {

    public int Stages; // 1 = Mouth; 2 = Arms; 3 = Legs;

    [Header("No Arms")]
    [SerializeField] private string IdleNoArms;
    [SerializeField] private string WalkNoArms;
    [SerializeField] private string DashNoArms;
    [SerializeField] private string ChargeNoArms;
    [SerializeField] private string AspirerNoArms;

    [Header("Arms")]
    [SerializeField] private string IdleArms;
    [SerializeField] private string WalkArms;
    [SerializeField] private string ChargeArms;
    [SerializeField] private string DashArms;
    [SerializeField] private string AspirerArms;
    [SerializeField] private string SmashArms;

    private Animator Anim;

    private void Start() {

        Anim = GetComponent<Animator>();
    }

    public void PlayAnimations(string Animation) {
        switch (Animation) {
            case "Idle":
                if (Stages == 1) {
                    Anim.Play(IdleNoArms);
                }
                else if (Stages == 2) {
                    Anim.Play(IdleArms);
                }
                break;


            case "Walk":
                if (Stages == 1) {
                    Anim.Play(WalkNoArms);
                }
                else if (Stages == 2) {
                    Anim.Play(WalkArms);
                }
                break;


            case "Charge":
                if (Stages == 1) {
                    Anim.Play(ChargeNoArms);
                }
                else if (Stages == 2) {
                    Anim.Play(ChargeArms);
                }
                break;

            case "Dash":
                if (Stages == 1) {
                    Anim.Play(DashNoArms);
                }
                else if (Stages == 2) {
                    Anim.Play(DashArms);
                }
                break;

            case "Inhale":
                if (Stages == 1) {
                    Anim.Play(AspirerNoArms);
                }
                else if (Stages == 2) {
                    Anim.Play(AspirerArms);
                }
                break;

            case "Smash":
                Anim.Play(SmashArms);
                break;
        }
    }
}
