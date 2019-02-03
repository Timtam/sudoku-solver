using System;
using System.IO;
using System.Text.RegularExpressions;

namespace sudoku_solver
{
  public static class Reader
  {
    public static int[,] Read(string file)
    {
      int row = 0, col = 0;
      int[,] sudoku;
      StreamReader sr;
      string line;
      string[] parts;
      int i;
      int p;
      
      if(!File.Exists(file))
        throw new ArgumentException("file doesn't exist");

      sudoku = new int[9,9];

      sr = File.OpenText(file);

      while((line = sr.ReadLine()) != null)
      {
        if(String.IsNullOrWhiteSpace(line))
          continue;

        if(row > 9)
          throw new ArgumentOutOfRangeException("too many rows found in sudoku");

        parts = Regex.Split(line, String.Empty);

        col = 0;

        for(i=0; i < parts.Length; i++)
        {
          if(String.IsNullOrWhiteSpace(parts[i]))
            continue;

          if(col > 9)
            throw new ArgumentOutOfRangeException("too many columns found in row " + row);

          try
          {
            p = int.Parse(parts[i]);
          }
          catch(FormatException)
          {
            throw new ArgumentException("non parseable content at row " + row + " and column " + col);
          }

          sudoku[row,col] = p;
          col++;
        }

        row++;
      }

      sr.Close();
      return sudoku;
    }
  }
}
