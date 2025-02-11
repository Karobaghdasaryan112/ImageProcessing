using ImageProcessing.Image;
using ImageProcessing.Interfaces;

namespace ImageProcessing.Effects
{
    public class BlurEffect : IImageEffect
    {
        public string Name => "Blur";
        public void Apply(ImageData image, object parameter)
        {
            Console.WriteLine($"Applying Blur ({parameter}px) to {image.Name}");
        }
    }
}
