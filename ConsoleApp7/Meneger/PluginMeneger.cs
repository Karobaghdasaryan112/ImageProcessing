using ImageProcessing.Interfaces;

namespace ImageProcessing.Meneger
{
    public class PluginManager : ILogable
    {
        private string _logMessage;
        private readonly List<IImageEffect> _plugins = new();

        public string Message 
        { 
            get => _logMessage;
            set => _logMessage = value; 
        }


        public void RegisterPlugin(IImageEffect effect)
        {
            _logMessage = $"Register effect name: {effect.Name}";
            Log(_logMessage);
            _plugins.Add(effect);
        }

        public IEnumerable<IImageEffect> GetPlugins() => _plugins;

        public void Log(string message)
        {
            File.AppendAllText(ILogable.LogPath, $"{DateTime.Now}: {message}\n");
        }
    }
}
