using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvSoftMandatory2DWorld.Classes.Interfaces
{
    public interface IMovable
    {
        void Move(int newX, int newY, World world);
    }
}
