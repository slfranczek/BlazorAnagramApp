using AnagramApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnagramApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnagramController : ControllerBase
    {
        private readonly IScrabbleService _scrabbleService;

        public AnagramController(IScrabbleService scrabbleService)
        {
            _scrabbleService = scrabbleService;
        }

        [HttpPost]
        public ActionResult<List<string>> GetAnagrams([FromBody] AnagramRequest request)
        {
            // Here you will implement your anagram search logic
            var countMap = transformWordToCountDictionary(request.Word);
            var scrabbleWords =_scrabbleService.GetScrabbleWords().Where(scrabword=>scrabword.Length == request.Word.Length).ToList();
            foreach(char c in countMap.Keys)
            {
                scrabbleWords = scrabbleWords.Where(scrabword=>scrabword.Count(wchar => wchar == c) == countMap[c]).ToList();
            }    
            return Ok(scrabbleWords);
        }

        private Dictionary<char, int> transformWordToCountDictionary(string word)
        {
            Dictionary<char, int> wordCount = new Dictionary<char, int>();
            List<string> scrabbleWords;
            foreach(char c in word)
            {
                if(wordCount.ContainsKey(c))
                {
                    wordCount[c]++;
                }
                else
                {
                    wordCount[c] = 1;
                }

                
            }
            
            return wordCount;
        }
    }
}
