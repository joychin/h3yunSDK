# H3YunSDK 测试项目配置说明

## 配置文件设置

本测试项目使用 `appsettings.json` 配置文件来管理 H3Yun SDK 的配置信息。

### 配置步骤

1. 在 `H3YunSDK.Tests` 项目根目录下找到 `appsettings.json` 文件
2. 修改配置文件中的 `EngineCode` 和 `EngineSecret` 为您的实际值：

```json
{
  "H3Yun": {
    "BaseUrl": "https://www.h3yun.com",
    "EngineCode": "your_actual_engine_code",
    "EngineSecret": "your_actual_engine_secret",
    "TimeoutSeconds": 30
  }
}
```

### 配置说明

- `BaseUrl`: H3Yun API 的基础地址
- `EngineCode`: 您的引擎代码
- `EngineSecret`: 您的引擎密钥
- `TimeoutSeconds`: 请求超时时间（秒）

### 注意事项

- 请确保不要将真实的 `EngineCode` 和 `EngineSecret` 提交到版本控制系统
- 建议在本地开发时创建 `appsettings.local.json` 文件来覆盖默认配置
- 配置文件会在构建时自动复制到输出目录

### 使用方式

测试类会自动从配置文件中读取配置信息，无需手动修改测试代码中的硬编码值。配置读取通过 `TestConfiguration` 类实现。