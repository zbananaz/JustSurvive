using UnityEngine;
using System.Collections;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField]
    private float timeToDisable = 0.5f;
	private float timer;

    private void OnEnable()
    {
        timer = timeToDisable;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            gameObject.SetActive(false);

        }
    }
}

