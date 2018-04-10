using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHorizontal : MonoBehaviour {
	public float RotationSpeed = 10;
	public float DampingTime =0.2f;
    private Vector3 m_TargetAngles;
    private Vector3 m_FollowAngles;
    private Vector3 m_FollowVelocity;
    private Quaternion m_OriginalRotation;
    private static Vector2 rotationRange = new Vector2(0, 360);
    private static Vector2 _rotationRangeLower = new Vector2();
    public static Vector2 rotationRangeLower
        {
            set
            {
                if (value != new Vector2()) {
                    _rotationRangeLower = value;
                } else {
                    _rotationRangeLower = -rotationRange;
                }
            }
        }
	// Use this for initialization
	void Start () {
		 _rotationRangeLower = -rotationRange;
        m_OriginalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		float inputH;
		float inputV;
        if (m_TargetAngles.y > 180){
            m_TargetAngles.y -= 360;
            m_FollowAngles.y -= 360;
        }
        if (m_TargetAngles.x > 180){
            m_TargetAngles.x -= 360;
            m_FollowAngles.x -= 360;
        }  
        if (m_TargetAngles.y < -180){
            m_TargetAngles.y += 360;
            m_FollowAngles.y += 360;
        }
         if (m_TargetAngles.x < -180){
            m_TargetAngles.x += 360;
            m_FollowAngles.x += 360;
         }
		inputH = Input.GetAxis("Horizontal");

		m_TargetAngles.y += inputH*RotationSpeed;
	

		// clamp values to allowed range
		m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, _rotationRangeLower.y*0.5f, rotationRange.y*0.5f);


		// smoothly interpolate current values to target angles
		m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, DampingTime);

		// update the actual gameobject's rotation
		transform.localRotation = m_OriginalRotation*Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);
	}
}
