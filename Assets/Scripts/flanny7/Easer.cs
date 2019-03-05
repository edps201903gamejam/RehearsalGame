using UnityEngine;

public static class Easer
{
    public static float Ease(float _currentTime, float _totalTime, float _startValue, float _endValue)
    {
        var rate = _currentTime / _totalTime;
        return Mathf.Lerp(_startValue, _endValue, rate);
    }

    public static float EaseIn(float _currentTime, float _totalTime)
    {
        return Ease(_currentTime, _totalTime, 0, 1);
    }
    
    public static float EaseOut(float _currentTime, float _totalTime)
    {
        return Ease(_currentTime, _totalTime, 1, 0);
    }

    public static float EaseInOut(float _currentTime, float _totalTime, float _minValue = 0, float _maxValue = 1)
    {
        var halfTime = _totalTime / 2;
        if (_currentTime < halfTime)
        {
            return Ease(_currentTime, halfTime, _minValue, _maxValue);
        }
        else
        {
            return Ease(_currentTime - halfTime, halfTime, _maxValue, _minValue);
        }
    }
}