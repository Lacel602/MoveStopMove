using UnityEngine;

public class ConstantStat : MonoBehaviour
{
    #region Singleton
    //private static ConstantStat _instance;
    //public static ConstantStat Instance
    //{
    //    get
    //    {
    //        // If the instance is null, find an existing instance
    //        if (_instance == null)
    //        {
    //            _instance = FindFirstObjectByType<ConstantStat>();
    //        }

    //        return _instance;
    //    }
    //}
    #endregion

    public static float increaseSize { get; private set; } = 1.1f;
}
