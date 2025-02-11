using ImageProcessing.Image;
using ImageProcessing.Interfaces;

namespace ImageProcessing.Effects
{
    public class ResizeEffect : IImageEffect
    {
        public string Name => "Resize";
        public void Apply(ImageData image, object parameter)
        {
            Console.WriteLine($"Resizing {image.Name} to {parameter}px");
        }
    }
}
