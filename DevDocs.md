# Develop Docs

## Shader
同じディレクトリにあるファイルのincludeは、現在の実装では ./ からはじめると失敗する。

Shaderを追加する際は [Utils.cs](Dev/Altseed.ShaderExt/Utils.cs) のUtils.Pathと同じように書く。

### Template
Shaders/Altseed.ShaderExt.Shaders/Hoge/hoge.hlsl
```HLSL
#include "../Utils/template.hlsl"

float4 main( const PS_Input Input ) : SV_Target
{
    return float4(Input.UV, 0.0, 1.0);
}
```
Shaders/Altseed.ShaderExt.Shaders/Hoge/hoge.glsl
```HLSL
#include "../Utils/template.glsl"

vec4 main_()
{
    return vec4(inUV.x, 1.0 - inUV.y, 0.0, 1.0);
}

void main()
{
    outOutput = main_();
}
```

### Property
* PostEffectBaseが自動的にセットするプロパティ
```HLSL
// HLSL
float g_second;
float2 g_resolution;
Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );
```

* ShaderObject2DBaseが自動的にセットするプロパティ
```HLSL
// HLSL
float g_second;
float2 g_position;
float2 g_sizeX;
float2 g_sizeY;
```

* ShaderObject2DRectangleが自動的にセットするプロパティ
```HLSL
// HLSL
float2 g_resolution;
float2 g_size;
```

* TextureObject2DWithMaterialが自動的にセットするプロパティ
```HLSL
// HLSL
float2 g_resolution;
float2 g_size;
Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );
```

### Tips
* uvの正規化
```HLSL
float2 uv = Input.UV * g_resolution;
```
* GLSLのuvをHLSLと同じ向きにする  
[Utils/template.glsl](Dev/Shaders/Altseed.ShaderExt.Shaders/Utils/template.glsl) 内のgetUVSameHLSL関数を使う。
Textureから値を取るときはそのままでよいが、ノイズの計算時などは合わせる。
```GLSL
vec2 getUVSameHLSL() {
    return vec2(inUV.x, 1.0 - inUV.y);
}
```