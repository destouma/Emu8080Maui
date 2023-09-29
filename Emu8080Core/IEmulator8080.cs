using System; 
using System.Collections.Generic;
using System.Text;

namespace Emu8080Core
{
    internal interface IEmulator8080
    {
        void LoadFileInMemoryAt(int address, String filePath);
        void Run();
        void KeyUp(Keys8080 key);
        void KeyDown(Keys8080 key);
    }
}
