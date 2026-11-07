using UnityEngine;

public class BladeCollapse : MonoBehaviour
{
    private float extendSpeed = 0.1f;
    private bool isBladeOn;
    private float minScaleValue = 0f;
    private float maxScaleValue;
    private float interpolationValue;
    private float currentScale;
    private float initialXScale;
    private float initialZScale;
    public GameObject blade;

    public AudioSource audioSource; 
    public AudioClip powerOnSound;  
    public AudioClip powerOffSound; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxScaleValue = transform.localScale.y;
        initialXScale = transform.localScale.x;
        initialZScale = transform.localScale.z;
        currentScale = maxScaleValue;
        interpolationValue = maxScaleValue/extendSpeed;
        isBladeOn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isBladeOn)
            {
                interpolationValue = -Mathf.Abs(interpolationValue);
                if (audioSource != null && powerOffSound != null)
                {
                    audioSource.PlayOneShot(powerOffSound, 0.6f);
                }
            }
            else
            {
                interpolationValue = Mathf.Abs(interpolationValue);
                if (audioSource != null && powerOnSound != null)
                {
                    audioSource.PlayOneShot(powerOnSound, 1.0f);
                }
            }
        }
        currentScale += interpolationValue * Time.deltaTime;
        currentScale = Mathf.Clamp(currentScale, minScaleValue, maxScaleValue);
        transform.localScale = new Vector3(initialXScale, currentScale, initialZScale);
        isBladeOn = currentScale > 0;

        //to make sure Unity doesn't render the blade if it is deactivated
        if (isBladeOn && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        else if (!isBladeOn && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}
