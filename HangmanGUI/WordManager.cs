using System;
using System.Collections.Generic;

namespace HangmanGUI
{
    public class WordManager
    {
        public enum Category { Animals, Names, Cars, FruitsVegetables, People }

        private Dictionary<Category, List<string>> wordLists = new Dictionary<Category, List<string>>()
        {
            { Category.Animals, new List<string> { "elephant", "giraffe", "penguin", "dolphin", "tiger" } },
            { Category.Names, new List<string> { "alexander", "isabella", "oliver", "sophia", "jacob" } },
            { Category.Cars, new List<string> { "ferrari", "mustang", "lamborghini", "porsche", "tesla" } },
            { Category.FruitsVegetables, new List<string> { "strawberry", "broccoli", "pineapple", "carrot", "blueberry" } },
            { Category.People, new List<string> { "einstein", "cleopatra", "shakespeare", "mariecurie", "nelsonmandela" } }
        };

        public string GetRandomWord(Category category)
        {
            if (wordLists.ContainsKey(category))
            {
                List<string> words = wordLists[category];
                return words[new Random().Next(words.Count)];
            }
            return "default"; // Fallback
        }
    }
}
