using UnityEngine;

public class ActivateOnLookat : MonoBehaviour
{
    public new Camera camera;
    public Behaviour target;

    public float thresholdAngle = 30f;
    public float thredholdDuration =  2f;

    private bool isLooking = false;
    private float showingTime;

    private void Awake()
    {
        target.enabled = false;
    }

    private void Update()
    {
        var dir = target.transform.position - camera.transform.position;
        var angle = Vector3.Angle(camera.transform.forward, dir);

        if(angle < thresholdAngle)
        {
            if (isLooking)
            {
                if (!target.enabled && Time.time > showingTime)
                    target.enabled = true;
            }
            else
            {
                isLooking = true;
                showingTime = Time.time + thredholdDuration;
            }
        }
        else if (isLooking)
        {
            isLooking = false;
            target.enabled = false;
        }
    }
}
