using UnityEngine;
using System.Collections;
using Tangle;

public class Test : MonoBehaviour
{
    LogicalMapping m_mapping;

    // Use this for initialization
    void Start ()
    {
        var names = Input.GetJoystickNames ();
        if (names.Length > 0) {
            var lm = MappingParser.GetMappingForController (names [0]);
            if (lm != null) {
                m_mapping = lm;
            }
        } else {
            m_mapping = MappingParser.GetMappingForController ("default");
        }
    }

    // Update is called once per frame
    void Update ()
    {
        TInput.Mapping = m_mapping;
        if(TInput.GetButton ("laser")) {
            Debug.Log ("laser fire");
        }
        var stick = new Vector3 (TInput.GetAxisRaw ("pitch"), TInput.GetAxisRaw ("yaw"), TInput.GetAxisRaw ("roll"));
        if (stick.magnitude > 0) {
            Debug.Log ("stick: " + stick);
        }
        var throttle = TInput.GetAxisRaw ("throttle");
        if (Mathf.Abs (throttle) > 0) {
            Debug.Log ("throttle: " + throttle);
        }
    }
}