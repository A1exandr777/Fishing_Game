using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycleController : MonoBehaviour
{
    public static DayCycleController Instance;
    
    [Header("Time Settings")]
    [Tooltip("Duration of full day-night cycle in seconds")]
    public float dayLengthInSeconds = 300f; // 5 minutes default
    [Range(0, 1)] public float currentCyclePosition = 0.25f; // Start at morning

    public int hours;
    public int minutes;

    private bool dayStarted;
    private bool nightStarted;
    
    
    [Header("Lighting")]
    public Light2D globalLight;
    public Gradient lightColorOverDay;
    public AnimationCurve lightIntensityOverDay;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private void Update()
    {
        // Advance time
        currentCyclePosition += Time.deltaTime / dayLengthInSeconds;

        hours = (int)(currentCyclePosition * 24);
        minutes = (int)((currentCyclePosition * 24 * 60 * 60 - hours * 60 * 60) / 60);
        Events.TimeChanged?.Invoke(hours, minutes);
        
        if (currentCyclePosition >= 1f)
        {
            currentCyclePosition = 0f;
            OnNewDay();
        }

        UpdateDayNightCycle();
    }

    private void UpdateDayNightCycle()
    {
        // Update global light
        globalLight.color = lightColorOverDay.Evaluate(currentCyclePosition);
        globalLight.intensity = lightIntensityOverDay.Evaluate(currentCyclePosition);
        
        // Toggle day/night objects based on thresholds
        var isDay = currentCyclePosition is > 0.25f and < 0.75f;
        if (!dayStarted && isDay)
        {
            dayStarted = true;
            nightStarted = false;
            Events.DayStarted?.Invoke();
        }
        if (!nightStarted && !isDay)
        {
            nightStarted = true;
            dayStarted = false;
            Events.NightStarted?.Invoke();
        }
        
        SoundController.Instance.UpdateAmbiance(isDay);
    }

    private void OnNewDay()
    {
        // Trigger day-specific events
        Debug.Log("A new day begins!");
    }

    // Call this to skip to specific time (0-1)
    public void SetTimeOfDay(float normalizedTime)
    {
        currentCyclePosition = Mathf.Clamp01(normalizedTime);
        UpdateDayNightCycle();
    }
}
