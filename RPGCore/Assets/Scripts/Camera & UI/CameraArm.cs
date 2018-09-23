using UnityEngine;

public class CameraArm : MonoBehaviour {

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        //Camera Follow
        transform.position = player.transform.position;
    }

}
