using UnityEngine;

[ExecuteInEditMode]
public class CameraControl : MonoBehaviour
{
    public bool m_rotate_by_time = false;
    public float m_rotate_speed = -10.0f;
    public Camera m_camera;
    public Transform m_look_target;

    public float m_follow_strength;
    public Vector3 m_target_offset;
    Vector3 m_look_pos;

    void Awake()
    {
        if (m_look_target == null) {
            Destroy(gameObject);
            return;
        }

        m_look_pos = m_look_target.position;
    }

    void Update()
    {
        if (m_camera == null || m_look_target == null)
            return;

        Transform cam_t = m_camera.GetComponent<Transform>();

        if (Input.GetKeyUp(KeyCode.R)) { m_rotate_by_time = !m_rotate_by_time; }

        Vector3 pos = cam_t.position - m_look_target.position;
        if (m_rotate_by_time) {
            pos = Quaternion.Euler(0.0f, Time.deltaTime * m_rotate_speed, 0) * pos;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            float ry = Input.GetAxis("Mouse X") * 1.0f;
            float rxz = Input.GetAxis("Mouse Y") * 0.05f;
            pos = Quaternion.Euler(0.0f, ry, 0) * pos;
            pos.y += rxz;
        }
        
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        pos += pos.normalized * wheel * 4.0f;
        cam_t.position = pos + m_look_target.position;

        if (Input.GetMouseButton(2)) {
            float xz = Input.GetAxis("Mouse X") * -0.1f;
            float y = Input.GetAxis("Mouse Y") * -0.1f;
            var rel = m_camera.cameraToWorldMatrix * new Vector4(xz, y, 0.0f, 0.0f);
            cam_t.position = cam_t.position + (Vector3)rel;
            m_look_target.position = m_look_target.position + (Vector3)rel;
        }

        m_look_pos += (m_look_target.position - m_look_pos) * m_follow_strength;
        cam_t.transform.LookAt(m_look_pos + m_target_offset);
    }
}
