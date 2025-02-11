
namespace ImageProcessing.Interfaces
{
    public interface ILogable
    {
        protected const string LogPath = @"../../../Files/Logger.json";
        public string Message { get; protected set; }

        void Log(string message);
    }
}
