using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mCam;
    private float followXRange;
    private float followYRange;

    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float Xmargin;
    [SerializeField] private float Ymargin;

    private bool endFollow = false;

    private void Awake()
    {
        mCam = GetComponent<Camera>();

        followXRange = (fieldSize.localScale.x * 0.5f) - mCam.orthographicSize * mCam.aspect + Xmargin;
        followYRange = (fieldSize.localScale.y * 0.5f) - mCam.orthographicSize + Ymargin;
    }

    private void Update()
    {
        if (!endFollow)
        {
            var pos = followTarget.position + offset;

            pos.x = Mathf.Clamp(pos.x, -followXRange, followXRange);
            pos.y = Mathf.Clamp(pos.y, -followYRange, followYRange);

            transform.position = pos;
        }
    }
}
