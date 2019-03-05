using UnityEngine;

public static class FyUtility
{

    /// <summary>
    /// 対象がnullであるときにデバッグ用ログを出す関数
    /// </summary>
    /// <param name="_object">チェックする対象</param>
    /// <param name="_gameObject">対象を持つGameObject</param>
    public static void NullCheck(UnityEngine.Object _object, GameObject _gameObject = null)
    {
        if (_object == null)
        {
            FyUtility.LogError(_object.name + " is null.", _gameObject);
        }
    }

    /// <summary>
    /// エラーログをイイ感じに表示する
    /// </summary>
    /// <param name="_msg">ログに出力させる文字</param>
    public static void LogError(string _msg, GameObject _gameObject = null)
    {
        Debug.LogError("<color=red>" + _msg + "</color>", _gameObject); Debug.Break();
    }
}
