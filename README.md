# ILRuntime Protobuf

此仓库修改自 [protobuf3-for-Unity-and-ILRuntime][1]。记录方便以后查看。

相关限制可参考原项目 [readme][2]。

## 使用方式

将 `Google.Protobuf` 下的源文件放到 Unity 工程下。

使用 `Protoc_3.4.0_bin/tool` 下的 protoc 将 .proto 文件转换成 .cs 文件。

```
// 要将花括号中的变量替换成相应的路径
protoc -I={protobufPath} --csharp_out={codeOutputDirectory} {protoPath}
```

## 集成到 ILRuntime

protoc 生成的 cs 文件要放到热更工程下，因为 protoc 生成 cs 文件中用到了 Unity 主工程的 IMessage 接口，根据 [ILRuntime 中跨域继承][3]，我们需要实现一个适配器，在仓库的 `ProtobufAdaptor.cs` 也有提供。在注册 ILRuntime 需要注册这个适配器。

```C#
HotfixDomain.RegisterCrossBindingAdaptor(new ProtobufMessageAdaptor());
HotfixDomain.DelegateManager.RegisterFunctionDelegate<ProtobufMessageAdaptor.Adaptor>();
```

---

PS：相较于原作者主要新增的是 macOS 平台下的 protoc 编译器，原作者提供了 protoc 的源代码，位于 `Protoc_3.4.0_src`，可以自行通过 cmake 编译。

[1]: https://github.com/gongxun/protobuf3-for-Unity-and-ILRuntime
[2]: https://github.com/gongxun/protobuf3-for-Unity-and-ILRuntime/blob/master/readme.md
[3]: https://ourpalm.github.io/ILRuntime/public/v1/guide/cross-domain.html
