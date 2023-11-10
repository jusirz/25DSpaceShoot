using System.Collections;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private Vector3 _homeposition = new Vector3(0, 1, -10);

    private float _elapsed = 1f;
    private float _cameraShakeOver;
    


    private void Start()
    {
        transform.position = _homeposition;

    }
    private IEnumerator CameraShake()
    {
        int x;
        int y;

        while (Time.time <= _cameraShakeOver)
        {
            Debug.Log("while loop on");
            x = Random.Range(-2, 2);
            y = Random.Range(-2, 2);
            Vector3 _shakePosition = new Vector3(x, y, -10);
            Vector3 _lerpPos = Vector3.Lerp(_homeposition, _shakePosition, .1f);
            transform.position = _lerpPos;
            yield return new WaitForSeconds(.25f);
        }
        yield return null;
        transform.position = _homeposition;
    }

    private void ShakeTimer()
    {
        _cameraShakeOver = Time.time + _elapsed;
    }
    public void ActiveCameraShake()
    {
        Debug.Log("camerashakestarted");
        ShakeTimer();
        StartCoroutine(CameraShake());
        StopCoroutine(CameraShake());
    }
}
