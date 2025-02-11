using ImageProcessing.Effects;
using ImageProcessing.Image;
using ImageProcessing.Interfaces;
using Newtonsoft.Json;

public class EffectConfig
{
    public string Name { get; set; }
    public object Parameter { get; set; }
}

public class EffectLoader : ILogable
{
    private string _logMessage;
    public string Message { get => _logMessage; set => _logMessage = value; }



    public List<IImageEffect> LoadEffects(string path)
    {
        var effectConfigs = JsonConvert.DeserializeObject<List<EffectConfig>>(File.ReadAllText(path));

        var effects = new List<IImageEffect>();

        foreach (var config in effectConfigs)
        {
            IImageEffect effect = config.Name switch
            {
                "Blur" => new BlurEffect(),
                "Resize" => new ResizeEffect(),
                _ => throw new ArgumentException($"Unknown effect: {config.Name}")
            };

            effects.Add(effect);
            Message = $"Loaded effect: {config.Name}";
            Log(Message);
        }

        return effects;
    }

    public void ApplyEffectToGroup(List<ImageData> images, IImageEffect effect, object parameter)
    {
        foreach (var image in images)
        {
            image.ApplyEffect(effect, parameter);
            Message = $"Applied {effect.Name} to {image.Name} with parameter {parameter}";
            Log(Message);
        }
    }

    public void Log(string message)
    {
        File.AppendAllText(ILogable.LogPath, $"{DateTime.Now}: {message}\n");
    }
}

