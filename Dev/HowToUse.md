# How to use

## Caution
このライブラリは開発中です。  
親子関係がある際に描画位置が正しいかなどは検証できていません。  

## Initialize
最初に、パッケージを追加します。AltseedのInitialize後に、以下のコードを記述してください。

```C#
Altseed.ShaderExt.Utils.AddPackage();
```

## TextureObject2D
### NormalMap

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


### Disolve
以下のように記述します。
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

その他はasd.TextureObject2Dと同様です。

## Types
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
static NoiseSource Random {}
static NoiseSource BlockNoise {}
static NoiseSource ValueNoise {}
static NoiseSource PerlinNoise {}
static NoiseSource Fbm {}
```
