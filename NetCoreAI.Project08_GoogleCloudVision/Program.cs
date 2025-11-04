using Google.Cloud.Vision.V1;

class Progra
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Resim Yolunu Girin");
        string ImgePach = Console.ReadLine();

        string credentialPach = @"C:\Users\ozana\Desktop\bold-catfish-470516-i9-fd887b2db3bb.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDTIALS", credentialPach);

        try
        {
            var cleint = ImageAnnotatorClient.Create();

            var image = Image.FromFile(ImgePach);
            var response = cleint.DetectText(image);
            Console.WriteLine("Resimdeki metin..\n");
            foreach (var anntoination in response)
            {
                if (!string.IsNullOrEmpty(anntoination.Description))
                {
                    Console.WriteLine(anntoination.Description);
                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu{ex.Message}");
        }
        // google cloud vision api ile resimden metin okuma programı

    }
}