using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    internal class GeometryObjec2DtReactive : asd.GeometryObject2D
    {
        public event Action OnAddedEvent = delegate { };
        public event Action OnUpdateEvent = delegate { };
        public event Action OnDisposeEvent = delegate { };
        public event Action OnRemovedEvent = delegate { };
        public event Action OnDrawAdditionallyEvent = delegate { };
        public event Action<asd.Collision2DInfo> OnCollisionEnterEvent = delegate { };
        public event Action<asd.Collision2DInfo> OnCollisionStayEvent = delegate { };
        public event Action<asd.Collision2DInfo> OnCollisionExitEvent = delegate { };

        public GeometryObjec2DtReactive()
        {
        }

        protected override void OnAdded()
        {
            base.OnAdded();
            OnAddedEvent();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            OnUpdateEvent();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            OnDisposeEvent();
        }

        protected override void OnRemoved()
        {
            base.OnRemoved();
            OnRemovedEvent();
        }

        protected override void OnDrawAdditionally()
        {
            base.OnDrawAdditionally();
            OnDrawAdditionallyEvent();
        }

        protected override void OnCollisionEnter(asd.Collision2DInfo collisionInfo)
        {
            base.OnCollisionEnter(collisionInfo);
            OnCollisionEnterEvent(collisionInfo);
        }

        protected override void OnCollisionStay(asd.Collision2DInfo collisionInfo)
        {
            base.OnCollisionStay(collisionInfo);
            OnCollisionStayEvent(collisionInfo);
        }

        protected override void OnCollisionExit(asd.Collision2DInfo collisionInfo)
        {
            base.OnCollisionExit(collisionInfo);
            OnCollisionExitEvent(collisionInfo);
        }

        internal new void DrawSpriteAdditionally(asd.Vector2DF upperLeftPos,
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
            base.DrawSpriteAdditionally(upperLeftPos,
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

        internal new void DrawTextAdditionally(asd.Vector2DF pos,
            asd.Color color,
            asd.Font font,
            string text,
            asd.WritingDirection writingDirection,
            asd.AlphaBlendMode alphaBlend,
            int priority)
        {
            base.DrawTextAdditionally(
                pos,
                color,
                font,
                text,
                writingDirection,
                alphaBlend,
                priority);
        }
    }
}
