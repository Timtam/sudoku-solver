using System;
using System.Collections.Generic;

namespace sudoku_solver
{
  public static class Solver
  {
    public static int[,] Solve(int[,] sudoku)
    {
      int[,] solution = SolveRecursively(sudoku);

      return solution;
    }

    private static int[,] SolveRecursively(int[,] sudoku)
    {
      Tuple<int, int> open_spot = GetNextOpenSpot(sudoku);
      int[,] possible_solution = sudoku;
      int[,] final_solution;

      while((possible_solution = GetNextSolution(possible_solution, open_spot)) != null)
      {
        if(IsPossibleSolution(possible_solution) == false)
        {
          continue;
        }
        if(IsFinalSolution(possible_solution) == true)
        {
          return possible_solution;
        }
        final_solution = SolveRecursively(possible_solution);
        if(final_solution != null)
        {
          return final_solution;
        }
      }
      return null;
    }

    private static int[,] GetNextSolution(int[,] origin, Tuple<int, int> spot)
    {
      int[,] s = origin.Clone() as int[,];
      s[spot.Item1, spot.Item2]++;

      if(s[spot.Item1, spot.Item2] > s.GetLength(0))
      {
        return null;
      }
      return s;
    }

    private static Tuple<int,int> GetNextOpenSpot(int[,] origin)
    {
      int i,j;
      int n = origin.GetLength(0);

      for(i=n - 1; i >= 0; i--)
        for(j=n - 1; j  >= 0; j--)
          if(origin[i,j] == 0)
            return new Tuple<int,int>(i,j);
      return null;
    }

    private static bool IsPossibleSolution(int[,] sudoku)
    {
      int n = sudoku.GetLength(0);
      int[] check;
      int i,j,k,l;
      int sgsize;

      // we will check columns first, so i will be rows and j will be columns
      for(i=0; i < n; i++)
      {
        check = new int[n + 1];
        for(j=0; j < n; j++)
        {
          check[sudoku[i,j]]++;
          if(check[sudoku[i,j]] > 1 && sudoku[i,j] > 0)
            return false;
        }
      }

      // this one will be rows
      for(j=0; j < n; j++)
      {
        check = new int[n + 1];
        for(i=0; i < n; i++)
        {
          check[sudoku[i,j]]++;
          if(check[sudoku[i,j]] > 1 && sudoku[i,j] > 0)
            return false;
        }
      }

      sgsize = GetSubGridSize(sudoku);

      if(sgsize > 0)
      {
        for(i=0; i < n; i += sgsize)
        {
          for(j=0; j < n; j += sgsize)
          {
            check = new int[n + 1];
            for(k=i; k < i + sgsize; k++)
            {
              for(l=j; l < j + sgsize; l++)
              {
                check[sudoku[k,l]]++;
                if(check[sudoku[k,l]] > 1 && sudoku[k,l] > 0)
                  return false;
              }
            }
          }
        }
      }

      return true;
    }

    private static bool IsFinalSolution(int[,] sudoku)
    {
      int i,j;
      int n = sudoku.GetLength(0);

      for(i=0; i < n; i++)
        for(j=0; j < n; j++)
          if(sudoku[i,j] == 0)
            return false;
      return true;
    }

    private static int GetSubGridSize(int[,] sudoku)
    {
      int n = sudoku.GetLength(0);
      double sqrt = Math.Sqrt(n);

      if(sqrt%1 != 0)
        return 0;

      if(sqrt < 2)
        return 0;

      return (int)sqrt;
    }
  }
}
