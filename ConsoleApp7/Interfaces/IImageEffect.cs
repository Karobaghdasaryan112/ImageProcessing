using ImageProcessing.Image;

namespace ImageProcessing.Interfaces
{
    public interface IImageEffect
    {
        string Name { get; }
        void Apply(ImageData image, object parameter);
    }
}
