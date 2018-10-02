using ProjectValkyrie.Components;
using ProjectValkyrie.Entities;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectValkyrie.Math;

namespace ProjectValkyrie.Managers
{
    class PhysicsManager
    {
        private long currentId = -1;
        private Vector2 maxPixelViewport;
        private Vector2 maxMeterViewport;

        private Vector2 cameraOffset;

        private Dictionary<long, PhysicsComponent> components;

        public PhysicsManager(Vector2 pixels, Vector2 meters)
        {
            maxPixelViewport = pixels;
            maxMeterViewport = meters;
            cameraOffset = new Vector2();
            components = new Dictionary<long, PhysicsComponent>();
        }

        public PhysicsComponent Get(long id)
        {
            return components[id];
        }

        public long Add(PhysicsComponent p)
        {
            currentId++;
            long id = currentId;
            components.Add(id, p);
            return id;
        }

        public void Update(GameTime t)
        {
            foreach(PhysicsComponent p in components.Values)
            {
                p.Update(t); // This will generate events from the physics objects if they trigger
            }
        }

        public List<long> GetIntersections()
        {
            List<long> pcs = new List<long>();
            return pcs;
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

    internal class QuadTree
    {
        private QuadTree ul;
        private QuadTree ur;
        private QuadTree ll;
        private QuadTree lr;
        private Vector2 min;
        private Vector2 max;

        private List<PhysicsComponent> components;
        private bool leaf;


        internal QuadTree Ul { get => ul; set => ul = value; }
        internal QuadTree Ur { get => ur; set => ur = value; }
        internal QuadTree Ll { get => ll; set => ll = value; }
        internal QuadTree Lr { get => lr; set => lr = value; }
        public bool Leaf { get => leaf; set => leaf = value; }

        public QuadTree(Vector2 mn, Vector2 mx)
        {
            min = mn;
            max = mx;
            components = new List<PhysicsComponent>();
            leaf = true;
        }

        public bool Insert(PhysicsComponent p)
        {
            if (p.MinBoundingBox.X > min.X && p.MinBoundingBox.Y > min.Y && p.MaxBoundingBox.X <= max.X && p.MaxBoundingBox.Y <= max.Y)
            {
                // This PC is within the BB, check if this will fit in children
                if (components.Count >= 4) // Eventually, refactor this to resort components into children
                {
                    if(leaf)
                    {
                        Vector2 mid = MathUtils.MidPoint(min, max);
                        ul = new QuadTree(min, mid);
                        ur = new QuadTree(new Vector2(mid.X, min.Y), new Vector2(max.X, mid.Y));
                        ll = new QuadTree(new Vector2(min.X, mid.Y), new Vector2(mid.X, max.Y));
                        lr = new QuadTree(MathUtils.MidPoint(min, max), max);
                        leaf = false;
                    }

                    if (ul.Insert(p)) return true;
                    if (ur.Insert(p)) return true;
                    if (ll.Insert(p)) return true;
                    if (lr.Insert(p)) return true;

                    components.Add(p); // Children couldn't hold, go ahead and add here -- oh well.
                    return true;
                }
                else
                {
                    components.Add(p);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public List<PhysicsComponent> FindPossibleHits(PhysicsComponent p)
        {
            List<PhysicsComponent> results = new List<PhysicsComponent>();

            if (MathUtils.Intersect(p.MinBoundingBox, p.MaxBoundingBox, min, max)) // P intersects our bounding box, add the components and check the children
            {
                results.AddRange(components);

                if(!leaf)
                {
                    results.AddRange(ul.FindPossibleHits(p));
                    results.AddRange(ur.FindPossibleHits(p));
                    results.AddRange(ll.FindPossibleHits(p));
                    results.AddRange(lr.FindPossibleHits(p));
                }
            }

            return results;
        }
    }
}
