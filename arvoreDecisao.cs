using System;
using System.Collections.Generic;
  class arvoreDecisao
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin decision tree demo \n");

      double[][] dataX = new double[51][];
      dataX[0] = new double[] { 1, 0, 0, 8 };  // 0
      dataX[1] = new double[] { 2, 0, 0, 10 };
      dataX[2] = new double[] { 3, 0, 0, 12 };
      dataX[3] = new double[] { 4, 0, 0, 14 };
      dataX[4] = new double[] { 5, 0, 0, 20 };
      dataX[5] = new double[] { 1, 5, 1, 3 };
      dataX[6] = new double[] { 2, 5, 1, 5 };
      dataX[7] = new double[] { 3, 5, 2, 7 };
      dataX[8] = new double[] { 4, 5, 3, 9 };
      dataX[9] = new double[] { 5, 5, 2, 15 };

      dataX[10] = new double[] { 1, 3, 1, 0 };  // 1
      dataX[11] = new double[] { 2, 2, 2, 0 };
      dataX[12] = new double[] { 3, 4, 2, 0 };
      dataX[13] = new double[] { 4, 1, 2, 0 };
      dataX[14] = new double[] { 5, 5, 3, 0 };
      dataX[15] = new double[] { 1, 2, 1, 4 };
      dataX[16] = new double[] { 2, 3, 2, 6 };
      dataX[17] = new double[] { 3, 4, 2, 8 };
      dataX[18] = new double[] { 4, 3, 3, 9 };
      dataX[19] = new double[] { 5, 4, 3, 12 };

      dataX[20] = new double[] { 1, 4, 1, 3 };   // 2
      dataX[21] = new double[] { 2, 1, 1, 8 };  
      dataX[22] = new double[] { 3, 2, 1, 6 };
      dataX[23] = new double[] { 4, 4, 2, 2 };
      dataX[24] = new double[] { 5, 4, 3, 9 };
      dataX[25] = new double[] { 4, 1, 1, 5 };
      dataX[26] = new double[] { 5, 3, 3, 10 };
      dataX[27] = new double[] { 4, 2, 1, 10 };
      dataX[28] = new double[] { 5, 2, 2, 5 };
      dataX[29] = new double[] { 1, 1, 1, 5 };

      dataX[30] = new double[] { 1, 3, 1, 1 };   // 2
      dataX[31] = new double[] { 1, 4, 1, 8 };  
      dataX[32] = new double[] { 2, 3, 2, 5 };
      dataX[33] = new double[] { 2, 4, 2, 3 };
      dataX[34] = new double[] { 2, 2, 1, 8 };
      dataX[35] = new double[] { 4, 4, 3, 10 };
      dataX[36] = new double[] { 1, 5, 1, 2 };
      dataX[37] = new double[] { 2, 1, 2, 0 };
      dataX[38] = new double[] { 3, 5, 2, 3 };
      dataX[39] = new double[] { 4, 1, 2, 0 };

      dataX[40] = new double[] { 5, 5, 2, 10 };   // 2
      dataX[41] = new double[] { 3, 2, 1, 3 };  
      dataX[42] = new double[] { 3, 1, 2, 2 };
      dataX[43] = new double[] { 2, 0, 0, 5 };
      dataX[44] = new double[] { 5, 4, 3, 10 };
      dataX[45] = new double[] { 5, 4, 2, 7 };
      dataX[46] = new double[] { 5, 4, 2, 3 };
      dataX[47] = new double[] { 4, 4, 1, 1 };
      dataX[48] = new double[] { 4, 3, 1, 10 };
      dataX[49] = new double[] { 4, 2, 2, 5 };
      dataX[50] = new double[] { 2, 2, 1, 3 };

      int[] dataY = 
        new int[51] { 1, 1, 2, 3, 3, 0, 0, 0, 0, 0,
                      0, 0, 0, 0, 0, 1, 1, 1, 1, 1,
                      1, 2, 2, 2, 2, 3, 3, 3, 3, 1,
                      1, 1, 1, 1, 1, 1, 0, 0, 0, 0,
                      0, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                      2 };

      Console.WriteLine("Inimaogos-item subset looks like: ");
      Console.WriteLine("1, 0, 0, 8 -> 1");
      Console.WriteLine("2, 0, 0, 10 -> 1");
      Console.WriteLine("3, 0, 0, 12 -> 2");
      Console.WriteLine(" . . . ");

      Console.WriteLine("\nBuilding 7-node 3-class decision tree");
      DecisionTree dt = new DecisionTree(250, 4);
      dt.BuildTree(dataX, dataY);

      // Console.WriteLine("\nTree is: ");
      // dt.Show();  // show all nodes in tree

      Console.WriteLine("\nDone. Nodes 0 and 4 are:");
      dt.ShowNode(0);
      dt.ShowNode(4);

      Console.WriteLine("\nComputing prediction accuracy on reference data:");
      double acc = dt.Accuracy(dataX, dataY);
      Console.WriteLine("Classification accuracy = " + acc.ToString("F4"));

      double[] x = new double[] { 4.0, 4.0, 2.5, 0.0 };
      Console.WriteLine("\nPredicting class for (1.0, 0.0, 0.0, 0.0)");
      int predClass = dt.Predict(x, verbose: true);

      Console.WriteLine("\nEnd demo ");
      Console.ReadLine();
    } // Main

  } // Program class

  class DecisionTree
  {
    public int numNodes;
    public int numClasses;
    public List<Node> tree;

    public DecisionTree(int numNodes, int numClasses)
    {
      this.numNodes = numNodes;
      this.numClasses = numClasses;
      this.tree = new List<Node>();
      for (int i = 0; i < numNodes; ++i)
        this.tree.Add(new Node());
    }

    public void BuildTree(double[][] dataX, int[] dataY)
    {
      // prep the list and the root node
      int n = dataX.Length;

      List<int> allRows = new List<int>();
      for (int i = 0; i < n; ++i)
        allRows.Add(i);

      this.tree[0].rows = new List<int>(allRows);

      for (int i = 0; i < this.numNodes; ++i)  // finish root, and do remaining
      {
        this.tree[i].nodeID = i;

        SplitInfo si = GetSplitInfo(dataX, dataY, this.tree[i].rows, this.numClasses);  // get the split info
        tree[i].splitCol = si.splitCol;
        tree[i].splitVal = si.splitVal;

        tree[i].classCounts = ComputeClassCts(dataY, this.tree[i].rows, this.numClasses);
        tree[i].predictedClass = ArgMax(tree[i].classCounts);

        int leftChild = (2 * i) + 1;
        int rightChild = (2 * i) + 2;

        if (leftChild < numNodes)
          tree[leftChild].rows = new List<int>(si.lessRows);
        if (rightChild < numNodes)
          tree[rightChild].rows = new List<int>(si.greaterRows);
      } // i

    } // BuildTree()

    public void Show()
    {
      for (int i = 0; i < this.numNodes; ++i)
        ShowNode(i);
    }

    public void ShowNode(int nodeID)
    {
      Console.WriteLine("\n==========");
      Console.WriteLine("Node ID: " + this.tree[nodeID].nodeID);
      Console.WriteLine("Node split column: " + this.tree[nodeID].splitCol);
      Console.WriteLine("Node split value: " + this.tree[nodeID].splitVal.ToString("F2"));
      Console.Write("Node class counts: ");
      for (int c = 0; c < this.numClasses; ++c)
        Console.Write(this.tree[nodeID].classCounts[c] + "  ");
      Console.WriteLine("");
      Console.WriteLine("Node predicted class: " + ArgMax(this.tree[nodeID].classCounts));
      Console.WriteLine("==========");
    }

    public int Predict(double[] x, bool verbose)
    {
      bool vb = verbose;
      int result = -1;
      int currNodeID = 0;
      string rule = "IF (*)";  // if any column is any value . . 
      while (true)
      {
        if (this.tree[currNodeID].rows.Count == 0)  // at an empty node
          break;

        if (vb) Console.WriteLine("\ncurr node id = " + currNodeID);

        int sc = this.tree[currNodeID].splitCol;
        double sv = this.tree[currNodeID].splitVal;
        double v = x[sc];
        if (vb) Console.WriteLine("Comparing " + sv + " in column " + sc + " with " + v);

        if (v < sv)
        {
          if (vb) Console.WriteLine("attempting move left");

          currNodeID = (2 * currNodeID) + 1;
          if (currNodeID >= this.tree.Count)
            break;
          result = this.tree[currNodeID].predictedClass;
          rule += " AND (column " + sc + " < " + sv + ")";
        }
        else
        {
          if (vb) Console.WriteLine("attempting move right");
          currNodeID = (2 * currNodeID) + 2;
          if (currNodeID >= this.tree.Count)
            break;
          result = this.tree[currNodeID].predictedClass;
          rule += " AND (column " + sc + " >= " + sv + ")";
        }

        if (vb) Console.WriteLine("new node id = " + currNodeID);
      }

      if (vb) Console.WriteLine("\n" + rule);
      if (vb) Console.WriteLine("Predcited class = " + result);
      
      return result;
    } // Prediction

    public double Accuracy(double[][] dataX, int[] dataY)
    {
      int numCorrect = 0;
      int numWrong = 0;
      for (int i = 0; i < dataX.Length; ++i)
      {
        int predicted = Predict(dataX[i], verbose:false);
        int actual = dataY[i];
        if (predicted == actual)
          ++numCorrect;
        else
          ++numWrong;
      }
      // Console.WriteLine("correct = " + numCorrect);
      // Console.WriteLine("wrong   = " + numWrong);
      return (numCorrect * 1.0) / (numWrong + numCorrect);
    }

    private static SplitInfo GetSplitInfo(double[][] dataX, int[] dataY, List<int> rows, int numClasses)
    {
      // given a set of parent rows, find the col and value, and less-rows and greater-rows of
      // partition that gives lowest resulting mean impurity or entropy
      int nCols = dataX[0].Length;
      SplitInfo result = new SplitInfo();

      int bestSplitCol = 0;
      double bestSplitVal = 0.0;
      double bestImpurity = double.MaxValue;
      List<int> bestLessRows = new List<int>();
      List<int> bestGreaterRows = new List<int>();  // actually >=

      foreach (int i in rows)  // traverse the specified rows of the ref data
      {
        for (int j = 0; j < nCols; ++j)
        {
          double splitVal = dataX[i][j];  // curr value to evaluate as possible best split value
          List<int> lessRows = new List<int>();
          List<int> greaterRows = new List<int>();
          foreach (int ii in rows)  // walk down curr column
          {
            if (dataX[ii][j] < splitVal)
              lessRows.Add(ii);
            else
              greaterRows.Add(ii);
          } // ii

          double meanImp = MeanImpurity(dataY, lessRows, greaterRows, numClasses);

          if (meanImp < bestImpurity)
          {
            bestImpurity = meanImp;
            bestSplitCol = j;
            bestSplitVal = splitVal;

            bestLessRows = new List<int>(lessRows);  // could use a CopyOf() helper
            bestGreaterRows = new List<int>(greaterRows);
          }

        } // j
      } // i

      result.splitCol = bestSplitCol;
      result.splitVal = bestSplitVal;
      result.lessRows = new List<int>(bestLessRows);
      result.greaterRows = new List<int>(bestGreaterRows);

      return result;
    }

    private static double Impurity(int[] dataY, List<int> rows, int numClasses)
    {
      // Gini impurity
      // dataY is all Y (class) values; rows tells which ones to analyze

      if (rows.Count == 0) return 0.0;

      int[] counts = new int[numClasses];  // counts for each of the classes
      double[] probs = new double[numClasses];  // frequency each class
      for (int i = 0; i < rows.Count; ++i)
      {
        int idx = rows[i];  // pts into refY
        int c = dataY[idx];  // class
        ++counts[c];
      }

      for (int c = 0; c < numClasses; ++c)
        if (counts[c] == 0) probs[c] = 0.0;
        else probs[c] = (counts[c] * 1.0) / rows.Count;

      double sum = 0.0;
      for (int c = 0; c < numClasses; ++c)
        sum += probs[c] * probs[c];

      return 1.0 - sum;
    }

    private static double MeanImpurity(int[] dataY, List<int> rows1, List<int> rows2, int numClasses)
    {
      if (rows1.Count == 0 && rows2.Count == 0)
        return 0.0;

      double imp1 = Impurity(dataY, rows1, numClasses); // 0.0 if rows Count is 0
      double imp2 = Impurity(dataY, rows2, numClasses);
      int count1 = rows1.Count;
      int count2 = rows2.Count;
      double wt1 = (count1 * 1.0) / (count1 + count2);
      double wt2 = (count2 * 1.0) / (count1 + count2);
      double result = (wt1 * imp1) + (wt2 * imp2);
      return result;
    }

    private static int[] ComputeClassCts(int[] dataY, List<int> rows, int numClasses)
    {
      int[] result = new int[numClasses];
      foreach (int i in rows)
      {
        int c = dataY[i];
        ++result[c];
      }
      return result;
    }

    private static int ArgMax(int[] classCts)
    {
      int largeCt = 0;
      int largeIndx = 0;
      for (int i = 0; i < classCts.Length; ++i)
      {
        if (classCts[i] > largeCt)
        {
          largeCt = classCts[i];
          largeIndx = i;
        }
      }
      return largeIndx;
    }

    //private static List<int> CopyOf(List<int> rows)
    //{
    //  List<int> result = new List<int>();
    //  foreach (int i in rows)
    //    result.Add(i);
    //  return result;
    //}

    // ----------

    public class Node
    {
      public int nodeID;
      public List<int> rows;  // which ref data rows
      public int splitCol;
      public double splitVal;
      public int[] classCounts;
      public int predictedClass;
    }

    public class SplitInfo  // helper struc
    {
      public int splitCol;
      public double splitVal;
      public List<int> lessRows;
      public List<int> greaterRows;
    }

  } // DecisionTree class
// DecisionTree class