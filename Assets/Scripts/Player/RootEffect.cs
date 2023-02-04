using System;
using System.Collections;
using Codice.Client.Commands.TransformerRule;
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

        private Transform follow;

        private TweenerCore<Vector3, Vector3, VectorOptions> tween;

        private float startX;

        private void Start()
        {
            light.transform.localPosition = new Vector2(0, -100);
            StartCoroutine(Run());
            follow = transform.parent;
            transform.SetParent(null, true);
            transform.localScale = Vector3.one;
            rootSprite.localPosition = Vector3.zero;
            rootSprite.localScale = Vector3.one;

            ssc.spline.Clear();
            var pos = follow.position;
            ssc.spline.InsertPointAt(0, transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootBottomYOffset)));
            ssc.spline.SetHeight(0, pointHeight);
            ssc.spline.InsertPointAt(1, transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootTopYOffset)));
            ssc.spline.SetHeight(1, pointHeight);
            ssc.spline.SetTangentMode(1, ShapeTangentMode.Broken);
            startX = pos.x;
        }

        private void Step()
        {
            var pos = follow.position;

            ssc.spline.SetPosition(0, transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootBottomYOffset)));

            light.transform.position = new Vector2(pos.x, pos.y - lightYOffset * 2);
            tween = light.transform.DOMove(new Vector2(pos.x, pos.y + lightYOffset), animDuration)
                .OnComplete(() => { tween = null; });
        }

        private void Update()
        {
            var pos = follow.position;
            var pointPos = transform.InverseTransformPoint(new Vector2(pos.x, pos.y + rootTopYOffset));
            ssc.spline.SetPosition(1, pointPos);

            // SplineUtility.CalculateTangents(pointPos, ssc.spline.GetPosition(0), ssc.spline.GetPosition(0), transform.forward, 10f, out var rightTangent, out var leftTangent);

            // ssc.spline.SetLeftTangent(1, leftTangent);
            // ssc.spline.SetRightTangent(1, rightTangent);
            ssc.spline.SetLeftTangent(1, -transform.up * pointTangentScale);
            
            tween?.ChangeEndValue(new Vector2(pos.x, pos.y + lightYOffset), true);
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
