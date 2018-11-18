using UnityEngine;

public class PanelDetector : MonoBehaviour {
    [SerializeField] private Transform mainPanel;
    [SerializeField] private string currentPanel;
    public string CurrentPanel {
        get { return currentPanel; }
        set { currentPanel = value; }
    }

    private const int posPlay = -935;
    private const int posCredits = 952;
    private const int posMain = 0;
    private const int posSettings = 525;

    // Update is called once per frame
    void Update () {
        transform.position = mainPanel.position;

        if(transform.localPosition.x < posPlay) {
            CurrentPanel = "Play";
        }
        if (transform.localPosition.x > posCredits) {
            CurrentPanel = "Credits";
        }
        if (transform.localPosition.x > posPlay && transform.localPosition.x < posCredits && transform.localPosition.y < posSettings) {
            CurrentPanel = "Main";
        }
        if (transform.localPosition.y > posSettings) {
            CurrentPanel = "Settings";
        }


    }


}
