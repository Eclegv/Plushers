using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hit
    {
        public Vector3 Direction;
        public int Damages;

        public Hit(Vector3 direction, int damages)
        {
            Direction = direction;
            Damages = damages;
        }
    }
}
