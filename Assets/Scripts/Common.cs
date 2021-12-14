using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{

    public static void drawCurve(Vector2 a, Vector2 b, Vector3 c, int resolution, LineRenderer lr)
    {
        DrawLine(curvePoints(a, b, c, resolution), lr);
    }

    public static void drawLine(Vector2[] positions, LineRenderer lr)
    {
        lr.positionCount = positions.Length;
        lr.SetPositions(vec2ArrayToVec3Array(positions));
    }

    public static Vector3[] vec2ArrayToVec3Array(Vector2[] pos)
    {
        Vector3[] array = new Vector3[pos.Length];
        for(int i = 0; i < pos.Length; i++)
        {
            array[i] = pos[i];
        }
        return array;
    }

    public static Vector2[] rotatePoints(Vector2[] points, Vector2 pos, float angle) {
        for(int i = 0; i < pos.Length; i++)
        {
            Vector2 point = points[i];
            float x = (point.x - pos.x) * Mathf.Cos(angle)- (point.y - pos.y) * Mathf.Sin(angle) + pos.x;
            float y = (point.x - pos.y) * Mathf.Sin(angle) + (point.y - pos.y) * Mathf.Cos(angle) + pos.y;
            points[i] = new Vector2(x, y);
        }
        
        return points;
    }

    public static Vector2[] curvePoints(Vector2 a, Vector2 b, Vector2 c, int resolution)
    {
        resolution = Mathf.Clamp(resolution, 1, 100);
        float time = 0f;
        float timeFactor = (100f / resolution) / 100f;
        Vector2[] positions = new Vector2[100];
        for (int i = 0; i < 100; i++)
        {
            positions[i] = quadraticCurve(a, b, c, time);
            time += timeFactor;
        }
        return positions;
    }

    public static Vector2[] circlePoints(Vector2 center, Vector2 radius, float resolution) {
        resolution = Mathf.Clamp(resolution, 1, 100);
        Vector2[] positions = new Vector2[100];
        float x, y, angle = 0f;

        for (int i = 0; i < (resolution + 1); i++)
        {
            x = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius.x) + center.x;
            y = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius.y) + center.y;

            positions[i] = new Vector2(x,y);

            angle += (360f / resolution);
        }
        return positions;
    }
    
    public static Vector2 quadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p = Vector2.Lerp(a, b, t);
        Vector2 p1 = Vector2.Lerp(b, c, t);
        Vector2 pf = Vector2.Lerp(p, p1, t);
        return pf;
    }

    public class PID
    {
        private float prevError, error = 0;
        public float p, i, d, ki, desired = 0;
        public float dt;

        public PID(float kp, float ki, float kd)
        {
            p = kp;
            i = ki;
            d = kd;
        }

        public float update(float x)
        {
            prevError = error;
            error = x - desired;

            float kp = p * error;
            float kd = d * ((error - prevError) / Time.deltaTime);
            ki = i * (ki + error * Time.deltaTime);

            return kp + kd + ki;
        }
    }

    public struct positionAndTime
    {
        public Vector2 pos;
        public float time;
        public positionAndTime(Vector2 p, float t)
        {
            pos = p;
            time = t;
        }
    }

    public static void drawBox(Vector2 p, Vector2 dimensions, LineRender lr, float angle=0f) {

        Vector[] points = new Vector2[
            new Vector2(p.x - dimensions.x/2f, p.y + dimensions.y/2f), //top left
            new Vector2(p.x - dimensions.x/2f, p.y - dimensions.y/2f), // bottom left
            new Vector2(p.x + dimensions.x/2f, p.y + dimensions.y/2f), //top right
            new Vector2(p.x + dimensions.x/2f, p.y - dimensions.y/2f) // top left
        ];
        if(angle != 0f || angle != 360f) {
            points = rotatePoints(points, p, angle);
        }
        drawLine(points, lr);
        
    }

    public static void drawPoint(Vector2 pos, Color color, float radius=1)
    {

    }
}
