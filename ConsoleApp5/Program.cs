using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

class RandomWord 
{
  public StreamReader words;
  private int numWords;
  public RandomWord()
  {
    words = new StreamReader("words5_en");
    numWords = count_words();
  }
  private int count_words()
  {
    numWords = 0;
    while (words.ReadLine() is string line)
    {
      numWords++;
    }
    words.Close();
    return numWords;
  }


  public string returnWord()
  {
    Random rand = new Random();
    StreamReader n = new StreamReader("words5_en");
    int pickedWordIndex = rand.Next(0, numWords);
    int i = 0;
    while (i < pickedWordIndex - 1)
    {
      n.ReadLine();
      i++;
    }
    string returnWord = n.ReadLine()!;
    return returnWord;
  }

}

class Wordle
{
  private char[] chars;
  private readonly string initial;
  IDictionary<char, int> initial_dict;
  public Wordle(string word)
  {
    chars = word.ToCharArray();
    initial = word;
    initial_dict = new Dictionary<char, int>();
    ToDictionary();
  }

  private void ToDictionary()
  {
    foreach (char ch in chars)
    {
      if (initial_dict.ContainsKey(ch))
      {
        initial_dict[ch]++;
      }
      else
      {
        initial_dict.Add(ch, 1);
      }
    }

  }

  private IDictionary<char, int> fillDict(char[] array)
  {
    IDictionary<char, int> d = new Dictionary<char, int>();
    foreach (char ch in chars)
    {
      if (d.ContainsKey(ch))
      {
        d[ch]++;
      }
      else
      {
        d.Add(ch, 1);
      }
    }
    return d;
  }

  public void game()
  {
    int tries = 0;
    ConsoleColor color = Console.ForegroundColor;
    Console.WriteLine("Hi! It`s a Wordle game CUI implementation using C# (dotnet 7.0), to exit just write !quit", Console.ForegroundColor = color);
    Console.WriteLine("Start of the game, choose your first 5-letter word:", Console.ForegroundColor = color);
    while (tries < 6)
    {

      int guessed = 0;
      string player_guess = Console.ReadLine()!;
      if (player_guess == "!quit")
      {
        break;
      }
      char[] green_chars = new char[5];
      char[] yellow_chars = new char[5];
      if (player_guess.Length != 5)
      {
        Console.WriteLine("Wrong word length", Console.ForegroundColor = color);
      }
      else
      {

        char[] guess_chars = player_guess.ToCharArray();
        IDictionary<char, int> keyValuePairs = fillDict(guess_chars);

        tries++;
        //green
        for (int i = 0; i < 5; i++)
        {
          if (guess_chars[i] == chars[i])
          {
            keyValuePairs[chars[i]] -= 1;
            green_chars[i] = chars[i];
            guessed += 1;
          }

        }
        //yellow
        for (int i = 0; i < 5; i++)
        {
          if (keyValuePairs.ContainsKey(guess_chars[i]))
          {
            if (keyValuePairs[guess_chars[i]] != 0)
            {
              keyValuePairs[guess_chars[i]] -= 1;
              yellow_chars[i] = guess_chars[i];
            }
          }
        }

        for (int i = 0; i < 5; i++)
        {
          if (green_chars[i] != 0)
          {
            Console.Write($"{green_chars[i]} ", Console.ForegroundColor = ConsoleColor.Green);
          }
          else if (yellow_chars[i] != 0)
          {
            Console.Write($"{yellow_chars[i]} ", Console.ForegroundColor = ConsoleColor.Yellow);
          }
          else
          {
            Console.Write("_ ", Console.ForegroundColor = ConsoleColor.DarkBlue);
          }
        }
        Console.Write('\n');
        if (guessed == 5)
        {
          Console.WriteLine($"Congrats! You guessed the word: {initial}", Console.ForegroundColor = color);
          break;
        }
        else
        {
          Console.WriteLine("Take a guess once again:", Console.ForegroundColor = color);
        }

      }

    }
    Console.WriteLine($"You lost, your word was: {initial}", Console.ForegroundColor = color);
  }
}


internal class Program
{
  static void Main(string[] args)
  {
    RandomWord r = new RandomWord();
    string wordToGuess = r.returnWord();
    Wordle game = new Wordle(wordToGuess);
    game.game();
  }
}