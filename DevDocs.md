# Develop Docs

## Shader
同じディレクトリにあるファイルのincludeは、現在の実装では ./ からはじめると失敗する。

### Property
PostEffectBaseが自動的にセットするプロパティ
```HLSL
float g_second;
float2 g_resolution;
Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );
```

ShaderObject2DBaseが自動的にセットするプロパティ
```HLSL
float g_second;
float2 g_position;
float2 g_sizeX;
float2 g_sizeY;
```

ShaderObject2DRectangleが自動的にセットするプロパティ
```HLSL
float2 g_resolution;
float2 g_size;
```

TextureObject2DWithMaterialが自動的にセットするプロパティ
```HLSL
float2 g_resolution;
float2 g_size;
Texture2D g_texture : register( t0 );
SamplerState g_sampler : register( s0 );
```

### Tips
* uvの正規化  
uv * g_resolution

* GLSLのuvをHLSLと同じ向きにする  
[Utils/template.glsl](Dev/Shaders/Altseed.ShaderExt.Shaders/Utils/template.glsl) 内のgetUVSameHLSL関数を使う。
```GLSL
vec2 getUVSameHLSL() {
    return vec2(inUV.x, 1.0 - inUV.y);
}
```