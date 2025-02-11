using ImageProcessing.Image;
using ImageProcessing.Interfaces;
using System.Collections.Concurrent;

namespace ImageProcessing.Process
{
    public class ImageProcessor : ILogable
    {
        private string _logMessage;
        private object _lock = new object();    
        private readonly ConcurrentQueue<(ImageData, IImageEffect, object)> _processingQueue = new();
        private readonly List<Task> _tasks = new();


        public string Message
        {
            get => _logMessage;
            set => _logMessage = value;
        }


        public void AddToQueue(ImageData image, IImageEffect effect, object parameter)
        {
            _processingQueue.Enqueue((image, effect, parameter));
            Message = $"Image {image.Name} with effect {effect.Name} added to the queue.";
            Log(Message);
        }

        public void StartProcessing()
        {
            while (_processingQueue.TryDequeue(out var item))
            {
                var (image, effect, parameter) = item;
                _tasks.Add(Task.Run(() =>
                {
                    effect.Apply(image, parameter);
                    Message = $"Applied {effect.Name} to {image.Name} with parameter {parameter}.";
                    lock (_lock)
                    {
                        Log(Message);
                    }
                }));
            }

            Task.WhenAll(_tasks).Wait();
            Message = "Processing complete.";

            Log(Message);

        }

        public void Log(string message)
        {
            File.AppendAllText(ILogable.LogPath, $"{DateTime.Now}: {message}\n");
        }
    }

}
