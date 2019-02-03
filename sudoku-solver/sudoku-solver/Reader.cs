using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace sudoku_solver
{
  public static class Reader
  {
    public static int[,] Read(string file)
    {
      int[,] sudoku;
      StreamReader sr;
      string line;
      string[] parts;
      int i,j,n,p;
      Queue<int> nums = new Queue<int>();
      double sqrt;
      
      if(!File.Exists(file))
        throw new ArgumentException("file doesn't exist");

      sr = File.OpenText(file);

      while((line = sr.ReadLine()) != null)
      {
        if(String.IsNullOrWhiteSpace(line))
          continue;

        parts = Regex.Split(line, String.Empty);

        for(i=0; i < parts.Length; i++)
        {
          if(String.IsNullOrWhiteSpace(parts[i]))
            continue;

          try
          {
            p = int.Parse(parts[i]);
          }
          catch(FormatException)
          {
            throw new ArgumentException("non parseable content found");
          }

          nums.Enqueue(p);
        }
      }

      sr.Close();

      sqrt = Math.Sqrt(nums.Count);

      if(sqrt%1 != 0)
        throw new ArgumentException("sudoku doesn't have correct layout: not same amount of rows and columns");

      n = (int)sqrt;

      sudoku = new int[n, n];

      for(i=0; i < n; i++)
        for(j=0; j < n; j++)
          sudoku[i,j] = nums.Dequeue();
      
      return sudoku;
    }
  }
}
