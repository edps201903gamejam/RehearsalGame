using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FlUtil
{

    public static void NullCheck(UnityEngine.Object _object, GameObject _gameObject = null)
    {
        if (_object == null)
        {
            FlUtil.LogError(_object.name + " is null.", _gameObject);
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

    //--------------------------------------------------------------------------------
    // 引数に渡したオブジェクトをディープコピーしたオブジェクトを生成して返す
    // ジェネリックメソッド版
    //--------------------------------------------------------------------------------
    public static T DeepCopy<T>(T target)
    {
        T result;
        BinaryFormatter b = new BinaryFormatter();
        MemoryStream mem = new MemoryStream();

        try
        {
            b.Serialize(mem, target);
            mem.Position = 0;
            result = (T)b.Deserialize(mem);
        }
        finally
        {
            mem.Close();
        }

        return result;
    }
    // 拡張メソッド版
    public static object DeepCopy(this object target)
    {
        object result;
        BinaryFormatter b = new BinaryFormatter();
        MemoryStream mem = new MemoryStream();

        try
        {
            b.Serialize(mem, target);
            mem.Position = 0;
            result = b.Deserialize(mem);
        }
        finally
        {
            mem.Close();
        }

        return result;
    }
}
