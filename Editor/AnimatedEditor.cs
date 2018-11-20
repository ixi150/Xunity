using System;
using UnityEditor;
using UnityEngine;

namespace Xunity.Editor
{
    [CanEditMultipleObjects]
    //[CustomEditor(typeof(...))]
    public class AnimatedEditor : UnityEditor.Editor
    {
        const string UNITY_PREFS_PREFIX = "IXI.Editor";
        const string IS_ANIMATING_PREF = UNITY_PREFS_PREFIX + "IsAnimating";
        const string SAMPLE_COUNT_PREF = UNITY_PREFS_PREFIX + "SampleCount";
        const string PIXELS_PER_UNIT_PREF = UNITY_PREFS_PREFIX + "PixelsPerUnit";

        Material material;
        float lastTime;
        Rect layoutRectangle;

        static bool IsAnimating
        {
            get { return EditorPrefs.GetBool(IS_ANIMATING_PREF, true); }
            set { EditorPrefs.SetBool(IS_ANIMATING_PREF, value); }
        }

        static int SampleCount
        {
            get { return EditorPrefs.GetInt(SAMPLE_COUNT_PREF, 150); }
            set { EditorPrefs.SetInt(SAMPLE_COUNT_PREF, value); }
        }

        static int PixelsPerUnit
        {
            get { return EditorPrefs.GetInt(PIXELS_PER_UNIT_PREF, 30); }
            set { EditorPrefs.SetInt(PIXELS_PER_UNIT_PREF, value); }
        }

        static float Time
        {
            get
            {
                var now = DateTime.Now;
                return now.Minute * 60f + now.Second + now.Millisecond / 1000f;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("=== PREVIEW ===");
            IsAnimating = EditorGUILayout.Toggle("Animate", IsAnimating);
            SampleCount = EditorGUILayout.IntSlider("Samples", SampleCount, 10, 1000);
            PixelsPerUnit = EditorGUILayout.IntSlider("Pixels per Unit", PixelsPerUnit, 1, 100);

            DrawOpenGL();
        }

        protected virtual void DrawThings()
        {

        }

        protected virtual void OnEnable()
        {
            // Find the "Hidden/Internal-Colored" shader, and cache it for use.
            material = new Material(Shader.Find("Hidden/Internal-Colored"));
            lastTime = Time;
        }

        protected void DrawPath(Vector3[] path, Color color)
        {
            // Start drawing in OpenGL Lines, to draw the lines of the grid.
            GL.Begin(GL.LINE_STRIP);
            GL.Color(color);

            foreach (var point in path)
                GL.Vertex(point);

            // End lines drawing.
            GL.End();
        }

        protected void DrawFunction(Vector3 center, Func<float, Vector3> function, Color color)
        {
            // Start drawing in OpenGL Lines, to draw the lines of the grid.
            GL.Begin(GL.LINE_STRIP);
            GL.Color(color);

            float delta = 1f / SampleCount;
            for (var i = 0; i <= SampleCount; i++)
            {
                float currentPercent = i * delta;
                var currentPosition = function(currentPercent) * PixelsPerUnit;
                currentPosition.y *= -1;
                GL.Vertex(currentPosition + center);
            }

            // End lines drawing.
            GL.End();
        }

        protected void DrawCircle(Vector2 center, float pixelSize, Color color)
        {
            GL.Begin(GL.TRIANGLES);
            GL.Color(color);

            int samples = SampleCount / 5;
            float delta = 1f / samples;
            for (var i = 0; i <= samples; i++)
            {
                var a = GetCircleVertex(i * delta) * pixelSize + center;
                var b = GetCircleVertex((i + 1) * delta) * pixelSize + center;
                GL.Vertex(center);
                GL.Vertex(a);
                GL.Vertex(b);
            }

            GL.End();
        }

        protected void DrawLine(Color color, Vector2 start, Vector2 end)
        {
            GL.Begin(GL.LINES);
            GL.Color(color);

            GL.Vertex(start);
            GL.Vertex(end);

            GL.End();
        }

        void DrawOpenGL()
        {
            // Begin to draw a horizontal layout, using the helpBox EditorStyle
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            // Reserve GUI space with a width from 10 to 10000, and a fixed height of 200, and cache it as a rectangle.
            layoutRectangle = GUILayoutUtility.GetRect(200, 500, 200, 500);

            if (Event.current.type == EventType.Repaint)
            {
                float time = Time;
                var deltaTime = time - lastTime;

                // If we are currently in the Repaint event, begin to draw a clip of the size of 
                // previously reserved rectangle, and push the current matrix for drawing.
                GUI.BeginClip(layoutRectangle);
                GL.PushMatrix();

                // Clear the current render buffer, setting a new background colour, and set our
                // material for rendering.
                GL.Clear(true, false, Color.black);
                material.SetPass(0);

                DrawBackground();
                DrawThings();

                // Pop the current matrix for rendering, and end the drawing clip.
                GL.PopMatrix();
                GUI.EndClip();

                if (IsAnimating)
                    Repaint();

                lastTime = time;
            }

            // End our horizontal 
            GUILayout.EndHorizontal();
        }

        void DrawBackground()
        {
            // Start drawing in OpenGL Quads, to draw the background canvas. Set the
            // colour black as the current OpenGL drawing colour, and draw a quad covering
            // the dimensions of the layoutRectangle.
            GL.Begin(GL.QUADS);
            GL.Color(Color.black);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(layoutRectangle.width, 0, 0);
            GL.Vertex3(layoutRectangle.width, layoutRectangle.height, 0);
            GL.Vertex3(0, layoutRectangle.height, 0);
            GL.End();
        }

        Vector2 GetCircleVertex(float circlePercent)
        {
            return Quaternion.AngleAxis(circlePercent * 360f, Vector3.forward) * Vector3.up;
        }
    }
}