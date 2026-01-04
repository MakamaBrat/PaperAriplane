using System.Collections;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public DistanceCounter distanceController;
    public Transform spawner;
    public Transform sp;
    public Transform distance;
    public MenuTravel menutravel;
    public Transform plane;
    public Transform plane2;
    public Transform explo;
    public Transform weapon;

    [Header("Slider")]
    public Transform slider;
    public float sliderSpeed = 3f;
    public float amplitude = 3f;

    [Header("Timer")]
    public float minTime = 1f;
    public float maxTime = 5f;

    [Header("Background Animator")]
    public Animator backgroundAnimator;

    private float startY;
    private float direction = 1f;

    private bool sliderMoving;
    private bool timerRunning;

    private float timer;
    private float timerDuration;

    void Awake()
    {
        // сохраняем исходную позицию ОДИН раз
        startY = slider.position.y;
    }

    void OnEnable()
    {
        // жёсткий ресет
        Vector3 pos = slider.position;
        pos.y = startY;
        slider.position = pos;

        plane.gameObject.SetActive(false);
        plane2.gameObject.SetActive(false);
        direction = 1f;
        sliderMoving = true;
        timerRunning = false;
        timer = 0f;

        explo.gameObject.SetActive(false);
        weapon.gameObject.SetActive(true);
        distance.gameObject.SetActive(false);

        backgroundAnimator.Play("BackgroundState");
        spawner.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleInput();

        if (sliderMoving)
            MoveSlider();

        if (timerRunning)
            RunTimer();
    }

    // --------------------
    // INPUT (OLD INPUT)
    // --------------------
    void HandleInput()
    {
        if (timerRunning)
            return;

        // ЛКМ или ТАП
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    IEnumerator make2()
    {
        yield return new WaitForSeconds(0.5f);
        plane2.gameObject.SetActive(true);
        plane.gameObject.SetActive(false);
    }

    void OnClick()
    {
        StopAllCoroutines();
        StartCoroutine(make2());

        sliderMoving = false;

        plane.gameObject.SetActive(true);
        explo.gameObject.SetActive(true);
        weapon.gameObject.SetActive(true);
        distance.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        float normalized = Mathf.InverseLerp(
            startY - amplitude,
            startY + amplitude,
            slider.position.y
        );

        timerDuration = Mathf.Lerp(minTime, maxTime, normalized);

        plane2.transform.position = sp.transform.position;
        backgroundAnimator.Play("BackgroundMove");
        weapon.gameObject.SetActive(false);

        timerRunning = true;
        timer = 0f;
    }

    // --------------------
    // SLIDER
    // --------------------
    void MoveSlider()
    {
        Vector3 pos = slider.position;
        pos.y += direction * sliderSpeed * Time.deltaTime;

        if (pos.y >= startY + amplitude)
        {
            pos.y = startY + amplitude;
            direction = -1f;
        }
        else if (pos.y <= startY - amplitude)
        {
            pos.y = startY - amplitude;
            direction = 1f;
        }

        slider.position = pos;
    }

    // --------------------
    // TIMER
    // --------------------
    void RunTimer()
    {
        timer += Time.deltaTime;

        if (timer >= timerDuration)
        {
            timerRunning = false;
            GameOver();
        }
    }

    void GameOver()
    {
        menutravel.makeMenu(3);
        distanceController.StopAndSaveBest();
        Debug.Log("GAME OVER");
    }
}
