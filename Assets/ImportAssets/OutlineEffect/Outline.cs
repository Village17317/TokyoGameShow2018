using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace cakeslice
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class Outline : MonoBehaviour
    {
        public Renderer Renderer { get; private set; }

        [Range(-1,3)]public int color;
        public bool eraseRenderer;

        private void Awake()
        {
            Renderer = GetComponent<Renderer>();
        }

        public void ObjectEnable() {
            IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
            .Select(c => c.GetComponent<OutlineEffect>())
            .Where(e => e != null);

            foreach(OutlineEffect effect in effects) {
                effect.AddOutline(this);
            }
        }

        public void ObjectDisable() {
            IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
                .Select(c => c.GetComponent<OutlineEffect>())
                .Where(e => e != null);

            foreach(OutlineEffect effect in effects) {
                effect.RemoveOutline(this);
            }
        }
    }
}