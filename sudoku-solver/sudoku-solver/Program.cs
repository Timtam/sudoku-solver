using System;

namespace sudoku_solver
{
  class Program
  {
    static int Main(string[] args)
    {
      int i,j,n;
      int[,] sudoku;
      string file;
      int[,] solution;

      if(args.Length == 0)
      {
        Console.WriteLine("sudoku-solver [FILE]");
        Console.WriteLine();
        Console.WriteLine("FILE - file with sudoku in it");
        Console.WriteLine("only 9 x 9 sudoku supported");
        Console.WriteLine("file must contain one number per character");
        Console.WriteLine("0 = value unknown");
        return 0;
      }

      if(args.Length > 1)
      {
        Console.WriteLine("only one argument supported");
        return 0;
      }

      file = args[0];

      try
      {
        sudoku = Reader.Read(file);
      }
      catch(Exception ex) when(
        ex is ArgumentException ||
        ex is ArgumentOutOfRangeException
      )
      {
        Console.WriteLine("error: " + ex.Message);
        return 1;
      }

      n = sudoku.GetLength(0);

      solution = Solver.Solve(sudoku);

      for(i=0; i < n; i++)
      {
        for(j=0; j < n; j++)
          Console.Write(solution[i,j]);
        Console.WriteLine();
      }

      return 0;
    }
  }
}
