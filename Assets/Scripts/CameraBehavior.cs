using System.Collections;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private Vector3 _homePosition = new Vector3(0, 1, -10);

    private float _elapsed = 1f;
    private float _cameraShakeOver;

    private void Start()
    {
        transform.position = _homePosition;

    }
    private IEnumerator CameraShake()
    {
        int x;
        int y;

        while (Time.time <= _cameraShakeOver)
        {
            x = Random.Range(-2, 2);
            y = Random.Range(-2, 2);
            Vector3 _shakePosition = new Vector3(x, y, -10);
            Vector3 _lerpPos = Vector3.Lerp(_homePosition, _shakePosition, .1f);
            transform.position = _lerpPos;
            yield return new WaitForSeconds(.25f);
        }
        yield return null;
        transform.position = _homePosition;
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
