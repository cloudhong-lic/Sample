# Sample.NLog.Console说明

* 纯Nlog的使用

* 使用Newtonsoft.Json来解析Object为JSON

```cs
var o1 = new O1
{
    a1 = "bar",
    a2 = 123,
    a3 = new O2
    {
        b1 = 23.32
    }
};

logger.Info("Object: {0}", JsonConvert.SerializeObject(o1));
```