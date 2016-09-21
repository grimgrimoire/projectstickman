using UnityEngine;
using System.Collections;

public class Boss1AnimationEvent : MonoBehaviour {

    Boss1 bossScript;

    // Use this for initialization
    void Start() {
        bossScript = GetComponentInParent<Boss1>();
    }

    // Update is called once per frame
    void Update() {

    }


    public void JumpToPlayerPosition()
    {
        bossScript.JumpToPlayer();
    }

}
