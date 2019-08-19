using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    public class EmptyDrawnObject2D : IDisposable
    {
        internal readonly asd.TextureObject2D coreObject;

        public EmptyDrawnObject2D()
        {
            var obj = new TextureObject2DReactive
            {
                //Texture = asd.Engine.Graphics.CreateEmptyTexture2D(1, 1, asd.TextureFormat.rgb)
                Texture = Utils.CreateTexture2DInternal("empty1x1.png")
            };

            obj.OnAddedEvent += () => {
                OnAdded();
                OnAddedEvent();
            };

            obj.OnRemovedEvent += () =>
            {
                OnRemoved();
                OnRemovedEvent();
            };

            obj.OnUpdateEvent += () =>
            {
                OnUpdate();
                OnUpdateEvent();
            };

            obj.OnDisposeEvent += () => {
                OnDispose();
                OnDisposeEvent();
            };

            obj.OnDrawAdditionallyEvent += () => {
                OnDrawAdditionally();
                OnDrawAdditionallyEvent();
            };

            obj.OnCollisionEnterEvent += x => {
                OnCollisionEnter(x);
                OnCollisionEnterEvent(x);
            };

            obj.OnCollisionStayEvent += x =>
            {
                OnCollisionStay(x);
                OnCollisionStayEvent(x);
            };

            obj.OnCollisionExitEvent += x =>
            {
                OnCollisionExit(x);
                OnCollisionExitEvent(x);
            };

            coreObject = obj;
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
