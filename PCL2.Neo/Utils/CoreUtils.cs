using System.Threading;

namespace PCL2.Neo.Utils;

public static class CoreUtils
{
    private static int _uuid = 1;

    
    /// 获取一个全程序内不会重复的数字（伪 Uuid）。
    /// </summary>
    public static int GetUuid()
    {
        return Interlocked.Increment(ref _uuid);
    }
}