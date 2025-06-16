# H3YunSDK - 氚云OpenAPI .NET Core SDK

## 简介

H3YunSDK是一个基于.NET Core的氚云OpenAPI客户端SDK，提供了对氚云API的简单封装，使开发者能够方便地集成氚云的业务能力到自己的应用中。

## 功能特性

- 支持氚云OpenAPI的8个公共方法
  - 创建业务对象 (CreateBizObject)
  - 加载业务对象 (LoadBizObject)
  - 更新业务对象 (UpdateBizObject)
  - 删除业务对象 (RemoveBizObject)
  - 获取业务对象列表 (ListBizObjects)
  - 获取工作流信息 (GetWorkflowInfo)
  - 提交工作流 (SubmitWorkflow)
  - 自定义接口调用 (InvokeCustomApi)
- 支持依赖注入
- 异步API设计
- 完善的异常处理
- 详细的日志记录

## 安装

```bash
dotnet add package H3YunSDK
```

## 使用方法

### 1. 配置服务

在`Startup.cs`或者`Program.cs`中配置服务：

```csharp
// 方式1：从配置文件中读取
builder.Services.AddH3YunSdk(builder.Configuration);

// 方式2：手动配置
builder.Services.AddH3YunSdk(options =>
{
    options.BaseUrl = "https://www.h3yun.com";
    options.EngineCode = "你的EngineCode";
    options.EngineSecret = "你的EngineSecret";
    options.TimeoutSeconds = 60;
});
```

### 2. 配置文件示例

在`appsettings.json`中添加配置：

```json
{
  "H3Yun": {
    "BaseUrl": "https://www.h3yun.com",
    "EngineCode": "你的EngineCode",
    "EngineSecret": "你的EngineSecret",
    "TimeoutSeconds": 60
  }
}
```

### 3. 使用SDK

```csharp
public class MyService
{
    private readonly IH3YunClient _h3YunClient;

    public MyService(IH3YunClient h3YunClient)
    {
        _h3YunClient = h3YunClient;
    }

    public async Task CreateBizObjectExample()
    {
        try
        {
            // 创建业务对象
            var request = new CreateBizObjectRequest
            {
                SchemaCode = "你的表单编码",
                BizObject = new BizObject
                {
                    { "F0000001", "测试数据" },
                    { "F0000002", 100 }
                },
                IsSubmit = true
            };

            var response = await _h3YunClient.CreateBizObjectAsync(request);

            if (response.Successful)
            {
                Console.WriteLine($"创建成功，业务对象ID：{response.ReturnData?.ObjectId}");
            }
            else
            {
                Console.WriteLine($"创建失败：{response.ErrorMessage}");
            }
        }
        catch (H3YunException ex)
        {
            Console.WriteLine($"发生异常：{ex.Message}");
        }
    }

    public async Task LoadBizObjectExample(string bizObjectId)
    {
        try
        {
            // 加载业务对象
            var request = new LoadBizObjectRequest
            {
                SchemaCode = "你的表单编码",
                BizObjectId = bizObjectId
            };

            var response = await _h3YunClient.LoadBizObjectAsync(request);

            if (response.Successful)
            {
                Console.WriteLine($"加载成功，业务对象名称：{response.ReturnData?.Name}");
                // 访问自定义字段
                if (response.ReturnData?.TryGetValue("F0000001", out var fieldValue) == true)
                {
                    Console.WriteLine($"字段值：{fieldValue}");
                }
            }
            else
            {
                Console.WriteLine($"加载失败：{response.ErrorMessage}");
            }
        }
        catch (H3YunException ex)
        {
            Console.WriteLine($"发生异常：{ex.Message}");
        }
    }

    public async Task ListBizObjectsExample()
    {
        try
        {
            // 查询业务对象列表
            var request = new ListBizObjectsRequest
            {
                SchemaCode = "你的表单编码",
                Filter = new Filter
                {
                    FromRowNum = 0,
                    ToRowNum = 10,
                    Matcher = new Matcher
                    {
                        Type = "And",
                        And = true,
                        Matchers = new List<ItemMatcher>
                        {
                            new ItemMatcher
                            {
                                Field = "F0000001",
                                CompareType = "Contains",
                                Value = "测试"
                            }
                        }
                    },
                    SortByCollection = new List<SortBy>
                    {
                        new SortBy
                        {
                            Field = "CreatedTime",
                            Direction = "Descending"
                        }
                    }
                }
            };

            var response = await _h3YunClient.ListBizObjectsAsync(request);

            if (response.Successful)
            {
                Console.WriteLine($"查询成功，总数：{response.ReturnData?.TotalCount}");
                if (response.ReturnData?.BizObjects != null)
                {
                    foreach (var bizObject in response.ReturnData.BizObjects)
                    {
                        Console.WriteLine($"业务对象ID：{bizObject.ObjectId}，名称：{bizObject.Name}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"查询失败：{response.ErrorMessage}");
            }
        }
        catch (H3YunException ex)
        {
            Console.WriteLine($"发生异常：{ex.Message}");
        }
    }
}
```

## 高级用法

### 自定义接口调用

```csharp
// 自定义请求类型
public class MyCustomRequest
{
    public string Parameter1 { get; set; } = string.Empty;
    public int Parameter2 { get; set; }
}

// 自定义响应类型
public class MyCustomResponse
{
    public string Result { get; set; } = string.Empty;
}

// 调用自定义接口
public async Task InvokeCustomApiExample()
{
    try
    {
        var request = new MyCustomRequest
        {
            Parameter1 = "测试参数",
            Parameter2 = 100
        };

        var response = await _h3YunClient.InvokeCustomApiAsync<MyCustomRequest, MyCustomResponse>(
            "CustomMethod", request);

        if (response.Successful)
        {
            Console.WriteLine($"调用成功，结果：{response.ReturnData?.Result}");
        }
        else
        {
            Console.WriteLine($"调用失败：{response.ErrorMessage}");
        }
    }
    catch (H3YunException ex)
    {
        Console.WriteLine($"发生异常：{ex.Message}");
    }
}
```

## 注意事项

1. 使用SDK前，请确保已获取氚云的`EngineCode`和`EngineSecret`。
2. 所有API调用都是异步的，请使用`await`关键字等待结果。
3. 请妥善处理异常，特别是`H3YunException`类型的异常。

## 许可证

MIT