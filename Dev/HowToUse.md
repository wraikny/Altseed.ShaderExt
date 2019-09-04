# How to use

## Caution
このライブラリは開発中です。  
親子関係がある際に描画位置が正しいかなどは検証できていません。  

## Initialize
最初に、パッケージを追加します。AltseedのInitialize後に、以下のコードを記述してください。

```C#
{
    // asd.Engine.Initialize( .. );

    Altseed.ShaderExt.Utils.AddPackage();
}
```

## TextureObject2D
### NormalMap
#### Sample
```C#
var normalObj = new TextureObject2DNormalMap()
{
    Texture = asd.Engine.Graphics.CreateTexture2D("sample.png"),
    NormalMap = asd.Engine.Graphics.CreateTexture2D("sample_normal.png"),
    ZPos = 0.0f,
};

normalObj.OnUpdateEvent += () => {
    var mousePos = asd.Engine.Mouse.Position;
    normalObj.Light0 = LightType.Point(mousePos, 100.0f);
};
```
#### Properties
* NormalMap : asd.Texture2D  
法線マップを設定する。
* ZPos : float  
オブジェクトのライティングに使う仮想のZ軸の方向の位置を設定する。
* Light0  Altseed.ShaderExt.LightType  
光源を設定する。
* LightColor0 : asd.Color  
光源の色を設定する。

### HSVOffset
#### Sample
```C#
var hsvObj = new TextureObject2DHSVOffset()
{
    Texture = asd.Engine.Graphics.CreateTexture2D("sample.png"),
    HueOffset = 0.5f,
    SatuationOffset = 0.2f,
    ValueOffset = 0.3f,
};
```
#### Properties
* HueOffset : float  
色相のオフセットを指定する。1.0を超えると0.0からループする。
* SatuationOffset : float  
彩度のオフセットを指定する。
* ValueOffset : float
輝度のオフセットを指定する。


### Disolve
#### Sample
```C#
var disolveObj = new Altseed.ShaderExt.TextureObject2DDisolve
{
    Texture = asd.Engine.Graphics.CreateTexture2D("sample.png"),
    BackGround = Altseed.ShaderExt.BackGround.Discard;
    NoiseSource = Altseed.ShaderExt.NoiseSource.PerlinNoise,
    NoiseSrc = new asd.RectF(0.0f, 0.0f, 10.0f, 10.0f),
};

float count = 0.0f;

disolveObj.OnUpdateEvent += () => {
    count += 0.01f;
    // obj.ZOffset = count;
    disolveObj.Threshold = (float)Math.Sin(count) * 0.5f + 0.5f;
};
```

#### Properties
* BackGround : Altseed.ShaderExt.BackGround  
Disolveで切り抜いたときの背景を取得・設定する。

* NoiseSource : Altseed.ShaderExt.NoiseSource  
Disolveの計算方法を取得・設定する。

* NoiseSrc : asd.RectF  
ノイズを計算するUVの位置とサイズの比率を取得・設定する。 

* Threshold : float  
0.0f ~ 1.0fでDisolveのしきい値を取得・設定する。  
0.0fのとき、完全に表示される。1.0fのとき、完全に消える。

* ZOffset : float  
ノイズのZ軸方向のオフセットを指定する。Texture以外で有効。

## Types
### LightType
class
```C#
static LightType Directional(asd.Vector3DF direction);
static LightType Directional(float x, float y, float z);
static LightType Point(asd.Vector3DF pos);
static LightType Point(float x, float y, float z);
static LightType Point(asd.Vector2DF xy, float z);
```

### BackGround
class
```C#
static Background Discard;
static Background Color(asd.Color color);
static Background Color(byte r, byte g, byte b, byte a);
static Background Color(byte r, byte g, byte b);
static Background Color(int r, int g, int b, int a);
```

### NoiseType
enum
* Random
* BlockNoise
* ValueNoise
* PerlinNoise
* Fbm

### NoiseSource
class
```C#
static NoiseSource Texture(asd.Texture2D texture) {}
static NoiseSource Random;
static NoiseSource BlockNoise;
static NoiseSource ValueNoise;
static NoiseSource PerlinNoise;
static NoiseSource Fbm;
```