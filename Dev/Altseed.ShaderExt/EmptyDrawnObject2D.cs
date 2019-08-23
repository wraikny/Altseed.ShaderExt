using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class EmptyDrawnObject2D : IDisposable
    {
        internal readonly GeometryObjec2DtReactive coreObject;

        public EmptyDrawnObject2D()
        {
            coreObject = new GeometryObjec2DtReactive();

            coreObject.OnAddedEvent += () => {
                OnAdded();
                OnAddedEvent();
            };

            coreObject.OnRemovedEvent += () =>
            {
                OnRemoved();
                OnRemovedEvent();
            };

            coreObject.OnUpdateEvent += () =>
            {
                OnUpdate();
                OnUpdateEvent();
            };

            coreObject.OnDisposeEvent += () => {
                OnDispose();
                OnDisposeEvent();
            };

            coreObject.OnDrawAdditionallyEvent += () => {
                OnDrawAdditionally();
                OnDrawAdditionallyEvent();
            };

            coreObject.OnCollisionEnterEvent += x => {
                OnCollisionEnter(x);
                OnCollisionEnterEvent(x);
            };

            coreObject.OnCollisionStayEvent += x =>
            {
                OnCollisionStay(x);
                OnCollisionStayEvent(x);
            };

            coreObject.OnCollisionExitEvent += x =>
            {
                OnCollisionExit(x);
                OnCollisionExitEvent(x);
            };
        }

        public static implicit operator asd.DrawnObject2D(EmptyDrawnObject2D x)
        {
            return x.coreObject;
        }

        #region Event
        /// <summary>
        /// このオブジェクトがレイヤーに登録されたときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action OnAddedEvent = delegate { };

        /// <summary>
        /// このオブジェクトがレイヤーから登録解除されたときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action OnRemovedEvent = delegate { };

        /// <summary>
        /// このオブジェクトが更新されるときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action OnUpdateEvent = delegate { };

        /// <summary>
        /// このオブジェクトが破棄されるときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action OnDisposeEvent = delegate { };

        /// <summary>
        /// このオブジェクトに関する追加の描画処理のイベントを登録できる。
        /// </summary>
        public event Action OnDrawAdditionallyEvent = delegate { };

        /// <summary>
        /// このオブジェクトが別の2Dオブジェクトのコライダと衝突開始するときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action<asd.Collision2DInfo> OnCollisionEnterEvent = delegate { };
        /// <summary>
        /// このオブジェクトが別の2Dオブジェクトのコライダと衝突が継続しているときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action<asd.Collision2DInfo> OnCollisionStayEvent = delegate { };

        /// <summary>
        /// このオブジェクトが別の2Dオブジェクトのコライダと衝突終了したときに呼ばれるイベントを登録できる。
        /// </summary>
        public event Action<asd.Collision2DInfo> OnCollisionExitEvent = delegate { };
        #endregion

        #region Virtual Method

        /// <summary>
        /// オーバーライドして、この2Dオブジェクトがレイヤーに登録されたときの処理を記述できる。
        /// </summary>
        public virtual void OnAdded() { }
        /// <summary>
        /// オーバーライドして、この2Dオブジェクトがレイヤーから登録解除されたときの処理を記述できる。
        /// </summary>
        public virtual void OnRemoved() { }
        /// <summary>
        /// オーバーライドして、この2Dオブジェクトが破棄される際の処理を記述できる。
        /// </summary>
        public virtual void OnUpdate() { }
        /// <summary>
        /// オーバーライドして、この2Dオブジェクトの更新処理を記述できる。
        /// </summary>
        public virtual void OnDispose() { }

        /// <summary>
        /// オーバーライドして、この2Dオブジェクトに関する追加の描画処理を記述できる。
        /// </summary>
        public virtual void OnDrawAdditionally() { }

        /// <summary>
        /// オーバーライドして、この2Dオブジェクトが別の2Dオブジェクトのコライダと衝突開始する際の処理を記述できる。
        /// </summary>
        /// <param name = "collisionInfo" > 衝突に関する情報 </ param >
        public virtual void OnCollisionEnter(asd.Collision2DInfo collision2DInfo) { }

        /// <summary>
        /// オーバーライドして、この2Dオブジェクトが別の2Dオブジェクトのコライダと衝突が継続している際の処理を記述できる。
        /// </summary>
        /// <param name = "collisionInfo" > 衝突に関する情報 </ param >
        public virtual void OnCollisionStay(asd.Collision2DInfo collision2DInfo) { }

        /// <summary>
        /// オーバーライドして、この2Dオブジェクトが別の2Dオブジェクトのコライダと衝突終了した際の処理を記述できる。
        /// </summary>
        /// <param name = "collisionInfo" > 衝突に関する情報 </ param >
        public virtual void OnCollisionExit(asd.Collision2DInfo collision2DInfo) { }

        #endregion


        // Property
        #region asd.Object2D Property

        /// <summary>
        /// このオブジェクトの親オブジェクトを取得する。
        /// </summary>
        public asd.Object2D Parent => coreObject.Parent;

        /// <summary>
        /// このオブジェクトが持っている子オブジェクトのコレクションを取得する。
        /// </summary>
        public IEnumerable<asd.Object2D> Children => coreObject.Children;

        /// <summary>
        /// このオブジェクトが登録されているレイヤーを取得します。
        /// </summary>
        public asd.Layer2D Layer => coreObject.Layer;

        /// <summary>
        /// カメラに表示するグループを取得する。
        /// </summary>
        /// <remarks>
        /// カメラのグループと他のオブジェクトのグループで&amp;でビット演算して1以上だった場合、そのカメラにオブジェクトが描画されるようになる。
        /// </remarks>
        public int CameraGroup
        {
            get => coreObject.CameraGroup;
            set => coreObject.CameraGroup = value;
        }

        /// <summary>
        /// このオブジェクトが更新されるかどうかを取得または設定する。
        /// </summary>
        public bool IsUpdated
        {
            get => coreObject.IsUpdated;
            set => coreObject.IsUpdated = value;
        }

        /// <summary>
        /// このオブジェクトが描画されるかどうかを取得または設定する。
        /// </summary>
        public bool IsDrawn
        {
            get => coreObject.IsDrawn;
            set => coreObject.IsDrawn = value;
        }

        /// <summary>
        /// このオブジェクトが実行中かどうかを取得する。破棄されている場合に false を返す。
        /// </summary>
        public bool IsAlive => coreObject.IsAlive;

        /// <summary>
        /// この2Dオブジェクトを描画する位置を取得または設定する。
        /// </summary>
        public asd.Vector2DF Position
        {
            get => coreObject.Position;
            set => coreObject.Position = value;
        }

        /// <summary>
        /// この2Dオブジェクトを描画する際の回転角度を取得または設定する。
        /// </summary>
        /// <remarks>
        /// 回転角度は度で表記され、回転方向は時計回りである。
        /// </remarks>
        public float Angle
        {
            get => coreObject.Angle;
            set => coreObject.Angle = value;
        }

        /// <summary>
        /// この2Dオブジェクトを描画する際の拡大率を取得または設定する。
        /// </summary>
        public asd.Vector2DF Scale
        {
            get => coreObject.Scale;
            set => coreObject.Scale = value;
        }

        /// <summary>
        /// このオブジェクトが親子関係を考慮して最終的に更新されるかどうかの真偽値を取得します。
        /// </summary>
        public bool AbsoluteBeingUpdated => coreObject.AbsoluteBeingUpdated;

        /// <summary>
        /// このオブジェクトが親子関係を考慮して最終的に描画されるかどうかの真偽値を取得します。
        /// </summary>
        public bool AbsoluteBeingDrawn => coreObject.AbsoluteBeingDrawn;

        #endregion

        #region asd.DrawnObject2D Property

        /// <summary>
        /// この2Dオブジェクトを描画する際に合成する色を取得または設定する。
        /// </summary>
        public asd.Color Color
        {
            get => coreObject.Color;
            set => coreObject.Color = value;
        }

        /// この2Dオブジェクトを描画する際の描画優先度を取得または設定する。描画優先度が高いほど手前に描画される。
        public int DrawingPriority
        {
            get => coreObject.DrawingPriority;
            set => coreObject.DrawingPriority = value;
        }

        /// <summary>
        /// 親子関係を考慮して最終的に描画時にこのオブジェクトに合成する色を取得する。
        /// </summary>
        public asd.Color AbsoluteColor => coreObject.AbsoluteColor;

        /// <summary>
        /// 親子関係を考慮したこのオブジェクトの最終的な描画優先度を取得する。
        /// </summary>
        public int AbsoluteDrawingPriority => coreObject.AbsoluteDrawingPriority;

        #endregion

        /// <summary>
        /// このインスタンスの更新の優先順位を取得または設定する。
        /// </summary>
        public int UpdatePriority
        {
            get => coreObject.UpdatePriority;
            set => coreObject.UpdatePriority = value;
        }

        /// <summary>
        /// この2Dオブジェクトを描画する際の描画原点を取得または設定する。描画原点は拡大・回転・描画の中心となる、画像データ内での座標。
        /// </summary>
        public asd.Vector2DF CenterPosition
        {
            get => coreObject.CenterPosition;
            set => coreObject.CenterPosition = value;
        }

        #region asd.Object2D Method

        /// <summary>
        /// この2Dオブジェクトを描画する際の実際の位置を取得または設定する。親子関係がある場合に、親の位置を考慮した位置を取得できる。
        /// </summary>
        /// <returns>この2Dオブジェクトの位置。</returns>
        public asd.Vector2DF GetGlobalPosition()
        {
            return coreObject.GetGlobalPosition();
        }

        /// <summary>
        /// この2Dオブジェクトを破棄する。
        /// </summary>
        public void Dispose()
        {
            coreObject.Dispose();
        }

        /// <summary>
        /// この2Dオブジェクトを破棄する。
        /// </summary>
        /// <param name = "disposeNative" > ネイティブ リソースも即解放するかどうかの真偽値。</param>
        /// <remarks>破棄状況を同期している子オブジェクトもすべて破棄するが、子オブジェクトの破棄はこのメソッドを呼び出したフレームの最後に実行されるので注意が必要。</remarks>
        public void Dispose(bool disposeNative)
        {
            coreObject.Dispose(disposeNative);
        }

        /// <summary>
        /// 指定した2Dオブジェクトを子オブジェクトとしてこのインスタンスに追加する。
        /// </summary>
        /// <param name = "child" > 追加する子オブジェクト </ param >
        /// < param name="managementMode">子オブジェクトの管理に関する同期設定。フラグをOR演算でつなげて複数指定することができる。</param>
        /// <param name = "transformingMode" > 子オブジェクトの変形に関する同期設定。</param>
        /// <remarks>実際に追加されるのはこのメソッドを呼び出したフレームの最後になるので注意が必要。</remarks>
        public void AddChild(asd.Object2D child,
            asd.ChildManagementMode managementMode,
            asd.ChildTransformingMode transformingMode)
        {
            coreObject.AddChild(child, managementMode, transformingMode);
        }

        /// <summary>
        /// 指定した子オブジェクトをこのインスタンスから削除する。
        /// </summary>
        /// <param name = "child" > 削除する子オブジェクト </ param >
        /// < remarks > 実際に削除されるのはこのメソッドを呼び出したフレームの最後になるので注意が必要。</remarks>
        public void RemoveChild(asd.Object2D child)
        {
            coreObject.RemoveChild(child);
        }

        /// <summary>
        /// 指定したコンポーネントをこの2Dオブジェクトに追加する。
        /// </summary>
        /// <param name = "component" > 追加するコンポーネント </ param >
        /// < param name="key">コンポーネントに関連付けるキー</param>
        /// <remarks>実際に追加されるのはこのメソッドを呼び出したフレームの最後になるので注意が必要。</remarks>
        public void AddComponent(asd.Object2DComponent component, string key)
        {
            coreObject.AddComponent(component, key);
        }

        /// <summary>
        /// 指定したキーを持つコンポーネントを取得する。
        /// </summary>
        /// <param name = "key" > 取得するコンポーネントのキー </ param >
        /// < returns > コンポーネント </ returns >
        public void GetComponent(string key)
        {
            coreObject.GetComponent(key);
        }

        /// <summary>
        /// 指定したコンポーネントをこの2Dオブジェクトから削除する。
        /// </summary>
        /// <param name = "key" > 削除するコンポーネントを示すキー </ param >
        /// < returns > 削除が成功したか否か。キーに対応するコンポーネントがなかった場合は false。</returns>
        /// <remarks>実際に削除されるのはこのメソッドを呼び出したフレームの最後になるので注意が必要。</remarks>
        public void RemoveComponent(string key)
        {
            coreObject.RemoveComponent(key);
        }


        /// <summary>
        /// コライダを追加する
        /// </summary>
        /// <param name = "collider" > 追加するコライダ </ param >
        public void AddCollider(asd.Collider2D collider)
        {
            coreObject.AddCollider(collider);
        }

        /// <summary>
        /// コライダを削除する
        /// </summary>
        /// <param name = "collider" > 削除するコライダ </ param >
        public void RemoveCollider(asd.Collider2D collider)
        {
            coreObject.RemoveCollider(collider);
        }


        /// <summary>
        /// 通常の描画に加えて専用のシェーダーを用いて2Dスプライトを描画する。
        /// </summary>
        /// <param name = "upperLeftPos" > テクスチャの左上の描画位置 </ param >
        /// < param name="upperRightPos">テクスチャの右上の描画位置</param>
        /// <param name = "lowerRightPos" > テクスチャの右下の描画位置 </ param >
        /// < param name="lowerLeftPos">テクスチャの左下の描画位置</param>
        /// <param name = "upperLeftCol" > テクスチャの左上の頂点色 </ param >
        /// < param name="upperRightCol">テクスチャの右上の頂点色</param>
        /// <param name = "lowerRightCol" > テクスチャの右下の頂点色 </ param >
        /// < param name="lowerLeftCol">テクスチャの左下の頂点色</param>
        /// <param name = "upperLeftUV" > テクスチャの左上のUV値 </ param >
        /// < param name="upperRightUV">テクスチャの右上のUV値</param>
        /// <param name = "lowerRightUV" > テクスチャの右下のUV値 </ param >
        /// < param name="lowerLeftUV">テクスチャの左下のUV値</param>
        /// <param name = "material" > 表示に使用するマテリアル </ param >
        /// < param name="alphaBlend">アルファブレンドの種類</param>
        /// <param name = "priority" > 描画の優先順位(大きいほど前面に描画される) </ param >
        /// < remarks >
        /// 専用のシェーダーを用いてスプライトを描画する。
        /// OnDrawAdditionallyの中以外では実行してはいけない。
        /// </remarks>
        protected void DrawSpriteAdditionally(asd.Vector2DF upperLeftPos,
            asd.Vector2DF upperRightPos,
            asd.Vector2DF lowerRightPos,
            asd.Vector2DF lowerLeftPos,
            asd.Color upperLeftCol,
            asd.Color upperRightCol,
            asd.Color lowerRightCol,
            asd.Color lowerLeftCol,
            asd.Vector2DF upperLeftUV,
            asd.Vector2DF upperRightUV,
            asd.Vector2DF lowerRightUV,
            asd.Vector2DF lowerLeftUV,
            asd.Texture2D texture,
            asd.AlphaBlendMode alphaBlend,
            int priority)
        {
            coreObject.DrawSpriteAdditionally(upperLeftPos,
                upperRightPos,
                lowerRightPos,
                lowerLeftPos,
                upperLeftCol,
                upperRightCol,
                lowerRightCol,
                lowerLeftCol,
                upperLeftUV,
                upperRightUV,
                lowerRightUV,
                lowerLeftUV,
                texture,
                alphaBlend,
                priority);
        }

        /// <summary>
        /// 通常の描画に加えて文字列を描画する。
        /// </summary>
        /// <param name = "pos" > 描画位置 </ param >
        /// < param name="color">頂点色</param>
        /// <param name = "font" > フォント </ param >
        /// < param name="text">文字列</param>
        /// <param name = "writingDirection" > 行方向 </ param >
        /// < param name="alphaBlend">アルファブレンドの種類</param>
        /// <param name = "priority" > 描画の優先順位(大きいほど前面に描画される) </ param >
        /// < remarks >
        /// OnDrawAdditionallyの中以外では実行してはいけない。
        /// </remarks>
        protected void DrawTextAdditionally(asd.Vector2DF pos,
            asd.Color color,
            asd.Font font,
            string text,
            asd.WritingDirection writingDirection,
            asd.AlphaBlendMode alphaBlend,
            int priority)
        {
            coreObject.DrawTextAdditionally(
                pos,
                color,
                font,
                text,
                writingDirection,
                alphaBlend,
                priority);
        }
        #endregion

        #region asd.DrawnObject2D Method

        /// <summary>
        /// 描画に関する同期設定を指定して、指定した2Dオブジェクトを子オブジェクトとしてこのインスタンスに追加する。
        /// </summary>
        /// <param name = "child" > 追加する子オブジェクト </ param >
        /// < param name="managementMode">子オブジェクトの管理に関する同期設定。フラグをOR演算でつなげて複数指定することができる。</param>
        /// <param name = "transformingMode" > 子オブジェクトの変形に関する同期設定。</param>
        /// <param name = "drawingMode" > 子オブジェクトの描画に関する同期設定。フラグをOR演算でつなげて複数指定することができる。</param>
        public void AddDrawnChild(
            asd.DrawnObject2D child,
            asd.ChildManagementMode managementMode,
            asd.ChildTransformingMode transformingMode,
            asd.ChildDrawingMode drawingMode)
        {
            coreObject.AddDrawnChild(child, managementMode, transformingMode, drawingMode);
        }

        #endregion
    }
}
