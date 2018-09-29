using ProjectValkyrie.Components;
using ProjectValkyrie.Entities;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectValkyrie.Managers
{
    class PhysicsManager
    {
        private Vector2 maxPixelViewport;
        private Vector2 maxMeterViewport;

        private Vector2 cameraOffset;

        public PhysicsManager(Vector2 pixels, Vector2 meters)
        {
            maxPixelViewport = pixels;
            maxMeterViewport = meters;
            cameraOffset = new Vector2();
        }

        public Vector2 MaxPixelViewport { get => maxPixelViewport; set => maxPixelViewport = value; }
        public Vector2 CameraOffset     { get => cameraOffset;     set => cameraOffset = value; }
        public Vector2 MaxMeterViewport { get => maxMeterViewport; set => maxMeterViewport = value; }

        public Vector2 ConvertToScreenCoordinates(Vector2 v)
        {
            Vector2 Result = new Vector2();

            float pixelsPerMeter = (maxPixelViewport.X) / (maxMeterViewport.X);

            Result.X = (v.X - cameraOffset.X) * pixelsPerMeter;
            Result.Y = (v.Y - cameraOffset.Y) * pixelsPerMeter;

            return Result;
        }
    }
}
