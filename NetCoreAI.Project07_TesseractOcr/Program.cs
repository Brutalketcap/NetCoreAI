using Tesseract;

class Prgram
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Kaçtane resimden yazı okutmak istiyorsunuz");

        int count = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Resimi Yükleyiniz: ");
            string imagePath = Console.ReadLine();

            string tessDataPath = @"C:\tessdate";

            try
            {
                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var imge = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(imge))
                        {
                            string text = page.GetText();
                            Console.WriteLine($"Resimden metin okunuyor...\n {text}");
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu {ex}");
            }
            Console.ReadLine();
            //Resimden betin okuma işlemi.
        }

    }
}