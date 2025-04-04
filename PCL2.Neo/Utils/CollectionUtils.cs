using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PCL2.Neo.Utils;

public static class CollectionUtils
{
    /// <summary>
    /// 将元素与 List 的混合体拆分为元素组。
    /// </summary>
    public static IList<T> GetFullList<T>(IList<T> data)
    {
        //for (int i = 0; i <= data.Count - 1; i++)
        //{
        //    if (data[i] is ICollection)
        //    {
        //        result.AddRange(data[i]);
        //    }
        //    else
        //    {
        //        result.Add(data[i]);
        //    }
        //}

        //foreach (var item in data)
        //{
        //    if (item is ICollection)
        //    {
        //        result.AddRange(item); // TODO: fix this bug "can not ensure Type"
        //    }
        //    else
        //    {
        //        result.Add(item);
        //    }
        //}

        return data.ToList(); // temp solution： flat once
    }
}
