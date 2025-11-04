using System.Speech.Synthesis;

class Progra
{
    static async Task Main(string[] args)
    {
        SpeechSynthesizer speechSythesizer = new SpeechSynthesizer();

        speechSythesizer.Volume = 100;
        speechSythesizer.Rate = 0;

        Console.WriteLine("Seslendirmek istediğiniz metini girin");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            speechSythesizer.Speak(input);
        }
        Console.ReadLine();
        //speech kütüpanesi kullanılar metinden seslendirme işlemi.
    }
}