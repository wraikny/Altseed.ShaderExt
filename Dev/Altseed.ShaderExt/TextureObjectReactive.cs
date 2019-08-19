using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altseed.ShaderExt
{
    internal class TextureObject2DReactive : asd.TextureObject2D
    {
        public event Action OnAddedEvent = delegate { };
        public event Action OnUpdateEvent = delegate { };
        public event Action OnDisposeEvent = delegate { };
        public event Action OnRemovedEvent = delegate { };
        public event Action OnDrawAdditionallyEvent = delegate { };
        public event Action<asd.Collision2DInfo> OnCollisionEnterEvent = delegate { };
        public event Action<asd.Collision2DInfo> OnCollisionStayEvent = delegate { };
        public event Action<asd.Collision2DInfo> OnCollisionExitEvent = delegate { };

        public TextureObject2DReactive()
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
    }
}
