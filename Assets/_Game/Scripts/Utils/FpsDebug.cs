using UnityEngine;

namespace  Utils
{
    public class FpsDebug : MonoBehaviour
    {
        private const float _fpsBufferPeriod = 5f;

        private float[] _fpsBuffer;
        private int _fpsBufferIndex;
        private float _fpsBufferTimer;

        private float _averageFps;
        private float _minFps;
        private float _maxFps;

        private GUIStyle guiStyle;
        private int _screenWidth;

        private void Start()
        {
            int bufferSize = Mathf.RoundToInt(_fpsBufferPeriod);
            _fpsBuffer = new float[bufferSize];
            _fpsBufferIndex = 0;

            guiStyle = new GUIStyle();
            guiStyle.fontSize = 24;
            guiStyle.normal.textColor = Color.white;

            _screenWidth = Screen.width;
        }

        private void Update()
        {
            float currentFps = 1f / Time.unscaledDeltaTime;

            _fpsBuffer[_fpsBufferIndex] = currentFps;
            _fpsBufferIndex = (_fpsBufferIndex + 1) % _fpsBuffer.Length;

            _fpsBufferTimer += Time.unscaledDeltaTime;
            if (_fpsBufferTimer >= 1f)
            {
                CalculateFpsStats();
                _fpsBufferTimer = 0f;
            }
        }

        private void CalculateFpsStats()
        {
            float sum = 0f;
            _minFps = float.MaxValue;
            _maxFps = float.MinValue;

            int count = 0;
            for (int i = 0; i < _fpsBuffer.Length; i++)
            {
                if (_fpsBuffer[i] > 0)
                {
                    sum += _fpsBuffer[i];
                    _minFps = Mathf.Min(_minFps, _fpsBuffer[i]);
                    _maxFps = Mathf.Max(_maxFps, _fpsBuffer[i]);
                    count++;
                }
            }

            _averageFps = count > 0 ? sum / count : 0;
        }

        private void OnGUI()
        {
            float labelWidth = 200f;
            float xPos = _screenWidth - labelWidth - 10;

            GUI.Label(new Rect(xPos, 10, labelWidth, 30), $"FPS avr(5s): {_averageFps:F0}", guiStyle);
            GUI.Label(new Rect(xPos, 40, labelWidth, 30), $"Min: {_minFps:F0}", guiStyle);
            GUI.Label(new Rect(xPos, 70, labelWidth, 30), $"Max: {_maxFps:F0}", guiStyle);

            GUI.Label(new Rect(xPos, 100, labelWidth, 30), $"Current: {1f / Time.unscaledDeltaTime:F0}", guiStyle);
        }
    }
}