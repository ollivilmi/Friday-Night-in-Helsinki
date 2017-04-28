using System;
using Player;

namespace Dialogue
{
	public class DialogueBar
	{
		private string[] dialogue1, dialogue2Positive, dialogue2Neutral,
		dialogue2Negative, dialogue3Positive, dialogue3Neutral, dialogue3Negative;
		private Random random;
        private string[] dialogue, answers;
		private int select;
		public DialogueBar ()
		{
			this.dialogue1 = new string[] { "Hey.", "Hello.", "Greetings.", "Yes?", "Sup." };
			this.dialogue2Positive = new string[] {
				"Haha.", 
				"Great.", 
				"Indeed, haha.", 
				"I like how you think."};
			this.dialogue2Neutral = new string[] { 
				"The weather is ok I guess.", 
				"It's been a very usual friday so far.",
				"I guess there's nothing special going on tonight.",
				"I'm bored.",
				"Mmhh.."};
			this.dialogue2Negative = new string[] {
				"What's wrong with you?",
				"Did you come here to fight?", 
				"Are you always this annoying?", 
				"Do you hate the sun?"};
			this.dialogue3Positive = new string[] { 
				"It was fun talking to you.", 
				"I hope I run into you again.", 
				"Have a nice day.", 
				"It was nice to meet you."};
			this.dialogue3Neutral = new string[] { 
				"I guess we're done here.", 
				"That's all.", 
				"Ooookay..", 
				"See you." };
			this.dialogue3Negative = new string[] { 
				"Get the hell out of my face.", 
				"One more word and you are done.", 
				"Are you kidding me?", 
				"That was the last straw." };
			this.random = new Random ();
			this.dialogue = new string[4];
            this.answers = new string[3];
			this.select = 0;
		}

        /// <summary>
        /// Function to randomize player answer options by using Next(1,4).
        /// Gets random string[3] dialogue and string[3] answers "Positive" "Neutral" or "Negative"
        /// which are used in a Dictionary later on to define the outcome of selecting
        /// this answer.
        /// </summary>
        /// <returns>string[3] Dialogue and out answers[3]</returns>
		public string[] randomSelection() {
			for (int i = 0; i < 3; i++)
			{
				select = random.Next (1, 4);
					switch (select) 
					{
						case 1:
						answers[i] = DialoguePositive.getOutcome();
                        dialogue[i] = DialoguePositive.getAnswer();
						break;
						case 2: 
						answers[i] = DialogueNeutral.getOutcome();
                        dialogue[i] = DialogueNeutral.getAnswer();
						break;
						case 3: 
						answers[i] = DialogueNegative.getOutcome();
                        dialogue[i] = DialogueNegative.getAnswer();
						break;
					}
			}
			return dialogue;
		}
        /// <summary>
        /// Randomizes first level dialogue for the NPC and answers for the player.
        /// </summary>
        /// <param name="answers">Answer options which can be "Positive", "Neutral" or "Negative".</param>
        /// <returns>Dialogue strings for the answer options.</returns>
		public string[] startDialogue1(out string[] answers)
		{
            dialogue[3] = dialogue1[random.Next(0, dialogue1.Length)];
            answers = this.answers;
			return randomSelection ();
		}

        /// <summary>
        /// Uses the user's answer to randomize a reaction dialogue, then randomizes new
        /// answers for the player.
        /// </summary>
        /// <param name="selection">Player's last answer, "Positive", "Neutral" or "Negative".</param>
        /// <param name="answers">Answer options which can be "Positive", "Neutral" or "Negative".</param>
        /// <returns>Dialogue strings for the answer options.</returns>
		public string[] startDialogue2(string selection, out string[] answers) //Parameter reply uses user input 1-3
		{
			switch (selection) {
			case "Positive":
                    dialogue[3] = dialogue2Positive[random.Next(0, dialogue2Positive.Length)];
                    answers = this.answers;
				return randomSelection ();
			case "Neutral":
                    dialogue[3] = dialogue2Neutral[random.Next(0, dialogue2Neutral.Length)];
                    answers = this.answers;
				return randomSelection ();
			case "Negative":
                    dialogue[3] = dialogue2Negative[random.Next(0, dialogue2Negative.Length)];
                    answers = this.answers;
				return randomSelection ();
			default:
                    answers = this.answers;
                    return dialogue;
			}
		}

        /// <summary>
        /// Uses the user's answer to randomize a reaction dialogue, then randomizes new
        /// answers for the player.
        /// </summary>
        /// <param name="selection">Player's last answer, "Positive", "Neutral" or "Negative".</param>
        /// <param name="answers">Answer options which can be "Positive", "Neutral" or "Negative".</param>
        /// <returns>Dialogue strings for the answer options.</returns>
		public string[] startDialogue3(string selection, out string[] answers)
		{
			switch (selection) {
                case "Positive":
                    dialogue[3] = dialogue3Positive[random.Next(0, dialogue3Positive.Length)];
                    answers = this.answers;
				return randomSelection ();
                case "Neutral":
                    dialogue[3] = dialogue3Neutral[random.Next(0, dialogue3Neutral.Length)];
                    answers = this.answers;
				return randomSelection ();
                case "Negative":
                    dialogue[3] = dialogue3Negative[random.Next(0, dialogue3Negative.Length)];
                    answers = this.answers;
				return randomSelection ();
			default:
                    answers = this.answers;
                    return dialogue;
			}
		}
	}
}

