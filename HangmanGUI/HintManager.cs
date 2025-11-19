using System.Collections.Generic;

namespace HangmanGUI
{
    public class HintManager
    {
        private Dictionary<string, string> poeticHints = new Dictionary<string, string>()
        {
            // Animals
            { "elephant", "With trunk so long and ears so wide, in savannas I reside." },
            { "giraffe", "Tall as trees with spots so neat, I stretch my neck to reach the treat." },
            { "penguin", "I waddle on ice, black and white, diving deep into the night." },
            { "dolphin", "In ocean blue, I leap and play, intelligent in every way." },
            { "tiger", "Striped in orange, fierce and bold, through jungles I proudly stroll." },

            // Names
            { "alexander", "A conqueror great, with empire vast, history's pages he amassed." },
            { "isabella", "Queen of Spain, explorers she sent, new worlds discovered, time well spent." },
            { "oliver", "A twist in tales, or a name so fine, in stories and life, I brightly shine." },
            { "sophia", "Wisdom's name, in Greek it means, knowledge and grace in all my dreams." },
            { "jacob", "From Bible's tale, a ladder he climbed, to heavens above, his faith sublime." },

            // Cars
            { "ferrari", "Red as fire, speed so fast, Italian pride, built to last." },
            { "mustang", "Wild and free, American dream, galloping power, like a stream." },
            { "lamborghini", "Bull's fierce charge, in yellow hue, exotic beast, breaking through." },
            { "porsche", "German precision, sleek and swift, engineering's gift." },
            { "tesla", "Electric whisper, future's call, silent revolution, standing tall." },

            // Fruits/Vegetables
            { "strawberry", "Red and sweet, on vines I grow, summer's kiss, in fields I show." },
            { "broccoli", "Green and curly, tree-like crown, healthy crunch, never let down." },
            { "pineapple", "Spiky crown, golden heart, tropical delight, a juicy start." },
            { "carrot", "Orange root, buried deep, rabbit's favorite, secrets I keep." },
            { "blueberry", "Tiny spheres of midnight blue, antioxidant power, good for you." },

            // People
            { "nelsonmandela", "Freedom's fighter, rainbow nation, peace's architect, reconciliation." }
        };

        public string GetHint(string word)
        {
            if (poeticHints.ContainsKey(word.ToLower()))
            {
                return poeticHints[word.ToLower()];
            }
            return "A mystery word, guess with care, poetic hints are rare.";
        }
    }
}
