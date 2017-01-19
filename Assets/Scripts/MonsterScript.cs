using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterScript : MonoBehaviour {

    // Fields
    private bool awake, waking;
    public float speed;
    private float change, timer;
    private ParticleSystem smoke;
    private Camera player;
    private GameObject panel;
    private UnityStandardAssets.ImageEffects.CameraMotionBlur blur;
    private UnityStandardAssets.ImageEffects.ScreenOverlay overlay;

    // Use this for initialization
    void Start () {
        awake = false;
        waking = false;
        smoke = this.transform.Find("Cloud Effect").GetComponent<ParticleSystem>();
        player = Camera.allCameras[0];
        speed = 4;
        panel = GameObject.FindObjectOfType<Canvas>().transform.Find("Panel").gameObject;
        blur = player.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>();
        overlay = player.GetComponent<UnityStandardAssets.ImageEffects.ScreenOverlay>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (awake && Time.time > timer && speed <= 6)
        {
            timer += 15;
            speed += 0.5f;
            change = speed * Time.deltaTime;
        }

        if (distance <= 10)
        {
            blur.velocityScale = Mathf.Pow(4, 8 / distance);
            overlay.intensity = Mathf.Abs((distance - 10) / (100 / 13));
        }

        else
        {
            blur.velocityScale = 0;
            overlay.intensity = 0;
        }

        if (awake)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, change);

            if (distance <= 2)
            {
                awake = false;
                Time.timeScale = 0;
                speed = 4;
                panel.SetActive(true);
            }
        }

        else
        {
            if (waking)
            {
                float rise = 3.7f * Time.deltaTime;
                smoke.transform.Translate(0, rise / (transform.position.y + 1), 0);

                if (smoke.transform.position.y >= transform.position.y)
                {
                    waking = false;
                    smoke.transform.position = new Vector3(transform.position.x,
                        transform.position.y, transform.position.z);
                    awake = true;
                }
            }

            else
            {
                timer = Time.time;
            }
        }
	}

    public void WakeMonster()
    {
        change = speed * Time.deltaTime;
        waking = true;
        timer = Time.time + 20.6f;
    }
}
