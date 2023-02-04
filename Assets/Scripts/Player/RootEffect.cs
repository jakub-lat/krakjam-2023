using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.U2D;

namespace Player
{
    public class RootEffect : MonoBehaviour
    {
        [SerializeField] private Light2DBase light;
        [SerializeField] private Transform rootSprite;
        [SerializeField] private float lightYOffset = 5f;
        [SerializeField] private float rootTopYOffset = -3f;
        [SerializeField] private float rootBottomYOffset = -10f;
        [SerializeField] private float pointHeight = 1f;
        [SerializeField] private float pointTangentScale = 7f;

        [SerializeField] private float animDuration = 2f;

        // [SerializeField] private float intervalMin = 1f;
        // [SerializeField] private float intervalMax = 3f;
        [SerializeField] private float interval;


        [SerializeField] private SpriteShapeController ssc;

        [SerializeField] private float heartNearbyMinDistance = 10f;

        private Transform rootHeart;
        private Transform follow;

        private TweenerCore<Vector3, Vector3, VectorOptions> tween;

        private float startX;

        private bool IsNearHeart => Vector3.Distance(follow.position, rootHeart.position) <= heartNearbyMinDistance;
        
        private int boundsHash = 0;

        private void Start()
        {
            rootHeart = HeartInstance.Current.transform;
            
            light.transform.localPosition = new Vector2(0, -100);
            StartCoroutine(Run());
            follow = transform.parent;
            transform.SetParent(null, true);
            transform.localScale = Vector3.one;
            rootSprite.localPosition = Vector3.zero;
            rootSprite.localScale = Vector3.one;

            ssc.spline.Clear();
            var pos = follow.position;
            // ssc.spline.InsertPointAt(0, transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootBottomYOffset)));
            ssc.spline.InsertPointAt(0, rootHeart.position);
            ssc.spline.SetHeight(0, pointHeight);
            ssc.spline.SetTangentMode(0, ShapeTangentMode.Linear);

            ssc.spline.InsertPointAt(1, transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootTopYOffset)));
            ssc.spline.SetHeight(1, pointHeight);
            ssc.spline.SetTangentMode(1, ShapeTangentMode.Broken);
            
            startX = pos.x;
            
            ssc.BakeMesh();
        }

        private void Step()
        {
            var pos = follow.position;

            Vector3 endPos;
            if (IsNearHeart)
            {
                light.transform.position = rootHeart.position;
                ssc.spline.SetLeftTangent(1, Vector3.zero);
                // Debug.Log("is near heart");
                endPos = new Vector2(pos.x, pos.y);
                
                ssc.spline.SetPosition(0, rootHeart.position);
            }
            else
            {
                light.transform.position = new Vector2(pos.x, pos.y - lightYOffset * 2);
                endPos = new Vector2(pos.x, pos.y + lightYOffset);
                
                ssc.spline.SetPosition(0, transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootBottomYOffset)));
            }
            
            tween = light.transform.DOMove(endPos, animDuration)
                .OnComplete(() => { tween = null; });
            
            SpriteShapeFix();
        }

        private void SpriteShapeFix()
        {
            var bounds = new Bounds();
            for (var i = 0; i < ssc.spline.GetPointCount(); ++i)
                bounds.Encapsulate(ssc.spline.GetPosition(i));
            bounds.Encapsulate(transform.position);
     
            if (boundsHash != bounds.GetHashCode())
            {
                ssc.spriteShapeRenderer.SetLocalAABB(bounds);
                boundsHash = bounds.GetHashCode();
            }
        }

        private void Update()
        {
            var pos = follow.position;
            var pointPos = transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootTopYOffset));
            ssc.spline.SetPosition(1, pointPos);

            // SplineUtility.CalculateTangents(pointPos, ssc.spline.GetPosition(0), ssc.spline.GetPosition(0), transform.forward, 10f, out var rightTangent, out var leftTangent);

            // ssc.spline.SetLeftTangent(1, leftTangent);
            // ssc.spline.SetRightTangent(1, rightTangent);
            
            Vector3 endPos;
            if (IsNearHeart)
            {
                endPos = new Vector2(pos.x, pos.y);
            } else
            {
                ssc.spline.SetLeftTangent(1, -transform.up * pointTangentScale);
                endPos = new Vector2(pos.x, pos.y + lightYOffset);
            }
            
            SpriteShapeFix();

            tween?.ChangeEndValue(endPos, true);
        }

        private IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);
                Step();
            }
        }
    }
}
