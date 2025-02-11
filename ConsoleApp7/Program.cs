using ImageProcessing.Image;
using ImageProcessing.Meneger;
using ImageProcessing.Process;

namespace ImageProcessing
{
    public class Program
    {

        private static string GetFilePath(string relativePath)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, @"..\..\..", "Files", relativePath);
        }

        private const string _effectPath = "EffectConfig.json";


        public static void Main(string[] args)
        {

            var processor = new ImageProcessor();
            var pluginManager = new PluginManager();
            var effectLoader = new EffectLoader();


            var effects = effectLoader.LoadEffects(GetFilePath(_effectPath));

            foreach (var effect in effects)
            {
                pluginManager.RegisterPlugin(effect);
            }


            var image1 = new ImageData("Image1", "image1.jpg");
            var image2 = new ImageData("Image2", "image2.jpg");


            processor.AddToQueue(image1, pluginManager.GetPlugins().First(), 5);
            processor.AddToQueue(image2, pluginManager.GetPlugins().First(), 10);


            processor.StartProcessing();

            image1.UndoLastEffect();
            image2.UndoLastEffect();

            effectLoader.ApplyEffectToGroup(new List<ImageData> { image1, image2 }, pluginManager.GetPlugins().First(), 7);


            image1.Save("image1_processed.jpg");
            image2.Save("image2_processed.jpg");

            Console.WriteLine("Processing complete.");
            Console.ReadLine();
        }
    }
}