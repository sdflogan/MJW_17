
using UnityEngine;

namespace MJW.Graple
{
    [ExecuteInEditMode()]
    public class GrapleManager : MonoBehaviour
    {
        public Transform[] points;
        public LineRenderer lineRenderer;

        // Use this for initialization
        void Start()
        {
            lineRenderer.positionCount = points.Length;
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < points.Length; ++i)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }
    }
}