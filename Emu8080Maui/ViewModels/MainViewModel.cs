using AndroidX.Annotations;
using Bumptech.Glide.Load.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using Emu8080Bitmap;
using Emu8080Core;
using Kotlin;
using Xamarin.Google.Crypto.Tink.Prf;

namespace Emu8080Maui
{
	public partial class MainViewModel : ObservableObject, IEmulator8080Listener
    {
        private Emulator8080 Emulator;
        private Thread Thread;
        private MdeColor colorBlack = new MdeColor(0, 0, 0);
        private MdeColor colorWhite = new MdeColor(255, 255, 255);
        private MdeBitmap bitmap;
        private Stream stream;

        [ObservableProperty]
        private String title;

        //[ObservableProperty]
        //ImageSource myImageSource;

        [ObservableProperty]
        ImageSource frameBuffer;

        public MainViewModel()
		{
            Title = "Emulator";
            //MyImageSource = "test.png";

            Thread = new Thread(new ThreadStart(this.ThreadTaskAsync));
            Thread.IsBackground = true;
            Thread.Start();
        }

        public void ChangeLabel(int value)
        {
            Title = "test click #" + value;
        }

        public void DisplayInstruction(int pc, OpCode8080 opCode, byte param1, byte param2)
        {
        }

        public void DisplayRegister(Cpu8080 cpu)
        {
        }

        public void DisplayStatus(Status8080 status)
        {
        }

        public void FrameBufferRefresh(byte[] frameBuffer)
        {
            bitmap = new MdeBitmap(224, 256);
            for (int x = 0; x < 224; x++)
            {
                for (int y = 0; y < 256; y+=8)
                {                    
                    for(int n = 0; n < 8; n++)
                    {
                        byte pix = frameBuffer[(x * (256 / 8)) + y / 8];
                        if (((1 << n) & pix) == 0)
                        {
                            bitmap.SetPixel(x, 255-(y+n), colorBlack);
                        }
                        else
                        {
                            bitmap.SetPixel(x, 255 - (y + n), colorWhite);
                        }
                    }   
                }
            }

            stream = new MemoryStream(bitmap.GetBitmapBytes());
            FrameBuffer = ImageSource.FromStream(() => stream);
        }

        public void InterruptGenerated()
        {
        }

        public void UnimplmentedOperationCode(OpCode8080 opCode)
        {
        }

        private async void ThreadTaskAsync()
        {
            byte[] content;
            const int maxReadSize = 256 * 1024;

            using (var stream = await FileSystem.OpenAppPackageFileAsync("invaders.bin"))
            {
                using (var br = new BinaryReader(stream))
                {
                    content = br.ReadBytes(maxReadSize);
                }
            }

            Emulator = new Emulator8080(this);
            Emulator.LoadBufferInMemoryAt(0, content);
            Emulator.Run();
        }
    }
}