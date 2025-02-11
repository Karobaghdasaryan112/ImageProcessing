using ImageProcessing.Interfaces;
using System.Drawing;

namespace ImageProcessing.Image
{
    public class ImageData : ILogable
    {
        private readonly Stack<(IImageEffect, object)> _appliedEffects = new();
        private string _logMessage;

        public string Name { get; set; }
        public Bitmap Image { get; set; }
        public string Message
        {
            get => _logMessage;
            set => _logMessage = value;
        }

        public ImageData(string name, string imagePath)
        {
            Name = name;
            //Image = new Bitmap(imagePath);
        }

        public void Save(string path)
        {
            //Image.Save(path);
            Log($"Saved image {Name} to {path}");
        }

        public void ApplyEffect(IImageEffect effect, object parameter)
        {
            effect.Apply(this, parameter);
            _appliedEffects.Push((effect, parameter));
            Log($"Applied effect {effect.Name} to image {Name} with parameter {parameter}");
        }

        public void UndoLastEffect()
        {
            if (_appliedEffects.Count > 0)
            {
                var lastEffect = _appliedEffects.Pop();
                Log($"Undid effect {lastEffect.Item1.Name} on image {Name}");
            }
            else
            {
                Log("No effects to undo.");
            }
        }

        public void Log(string message)
        {
            File.AppendAllText(ILogable.LogPath, $"{DateTime.Now}: {message}\n");
        }
    }

}
