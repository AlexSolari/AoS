using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src
{
    class Control : Entity
    {
        public Control(string path, float x, float y)
        {
            SetGraphic(new Image(path));
            X = x;
            Y = y;
        }
    }
}
