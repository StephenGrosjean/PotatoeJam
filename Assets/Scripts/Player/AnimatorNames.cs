using UnityEngine;

/// <summary>
/// All the animations for the player are triggered from here
/// </summary>

public class AnimatorNames : MonoBehaviour {

    [SerializeField] private int stages; // 1 = Mouth; 2 = Arms; 3 = Legs;

    public int Stages
    {
        get { return stages; }
        set { stages = value; }
    }

    [Header("No Arms")]
    [SerializeField] private string idleNoArms;
    [SerializeField] private string walkNoArms;
    [SerializeField] private string dashNoArms;
    [SerializeField] private string chargeNoArms;
    [SerializeField] private string aspirerNoArms;

    [Header("Arms")]
    [SerializeField] private string idleArms;
    [SerializeField] private string walkArms;
    [SerializeField] private string chargeArms;
    [SerializeField] private string dashArms;
    [SerializeField] private string aspirerArms;
    [SerializeField] private string smashArms;

    [Header("Legs")]
    [SerializeField] private string idleLegs;
    [SerializeField] private string walkLegs;
    [SerializeField] private string kickLegs;
    [SerializeField] private string jumpLegs;

    private Animator anim;

    private void Start() {

        anim = GetComponent<Animator>();

       
    }


    public void PlayAnimations(string animation) {
        switch (animation) {
            case "Idle":
                if (Stages == 1) {
                    anim.Play(idleNoArms);
                }
                else if (Stages == 2) {
                    anim.Play(idleArms);
                }
                else if (Stages == 3) {
                    anim.Play(idleLegs);
                }
                break;


            case "Walk":
                if (Stages == 1) {
                    anim.Play(walkNoArms);
                }
                else if (Stages == 2) {
                    anim.Play(walkArms);
                }
                else if (Stages == 3) {
                    anim.Play(walkLegs);
                }
                break;


            case "Charge":
                if (Stages == 1) {
                    anim.Play(chargeNoArms);
                }
                else if (Stages == 2) {
                    anim.Play(chargeArms);
                }
                break;

            case "Dash":
                if (Stages == 1) {
                    anim.Play(dashNoArms);
                }
                else if (Stages == 2) {
                    anim.Play(dashArms);
                }
                break;

            case "Inhale":
                if (Stages == 1) {
                    anim.Play(aspirerNoArms);
                }
                else if (Stages == 2) {
                    anim.Play(aspirerArms);
                }
                break;

            case "Smash":
                anim.Play(smashArms);
                break;

            case "Jump":
                anim.Play(jumpLegs);
                break;
            
            case "Kick":
                anim.Play(kickLegs);
                break;
        }
    }
}
