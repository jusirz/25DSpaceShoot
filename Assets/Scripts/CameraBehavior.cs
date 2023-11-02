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

        while (Time.time <= _cameraShakeOver)
        {
            Debug.Log("while loop on");
            int _x = Random.Range(-2, 2);
            int _y = Random.Range(-2, 2);
            Vector3 _shakePosition = new Vector3(_x, _y, -10);
            Vector3 _lerpPos = Vector3.Lerp(_homeposition, _shakePosition, .1f);
            transform.position = _lerpPos;
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(0f);
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
