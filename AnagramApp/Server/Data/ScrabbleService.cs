public class ScrabbleService : IScrabbleService
{
    private List<string> _scrabbleWords;
    private string filePath = "Data/dictionary.txt";

    public ScrabbleService(){
        _scrabbleWords = File.ReadAllLines(filePath).ToList();
    }
    public List<string> GetScrabbleWords()
    {
        return _scrabbleWords;
    }
}