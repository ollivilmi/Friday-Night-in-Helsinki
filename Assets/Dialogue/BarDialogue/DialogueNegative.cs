using System;
using System.Collections.Generic;

namespace Dialogue
{
	public static class DialogueNegative
	{
		private static List<string> negative = new List<string> () { 
			"Go to hell.", 
			"What's the matter with you?", 
			"Stop bothering me.", 
			"Why are you looking at me?",
			"Please stop.",
			"Don't waste my time.",
			"Do I look like I'm talking to you?" };
		private static int listSize = negative.Count;
		private static Random random = new Random();

		public static string getOutcome() {
			return "Negative";
		}
        public static string getAnswer()
        {
            return negative[random.Next(0, listSize)];
        }
	}
}

