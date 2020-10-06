# UniBuildPlayerWindowInternal

BuildPlayerWindow の internal な機能を使用できるようにするパッケージ

## 使用例

```cs
using Kogane;
using UnityEditor;

public class Example
{
    [MenuItem( "Tools/Hoge" )]
    private static void Hoge()
    {
        // WebGL のモジュールをインストールする Unity Hub のページを開く
        var url = BuildPlayerWindowInternal.GetUnityHubModuleDownloadURL( "WebGL" );
        Help.BrowseURL( url );
    }
}
```
