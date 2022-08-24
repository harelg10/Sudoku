using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int Next = 0;
        int time = 0;
        int rows = 9;
        int cols = 9;
        int ResourceIndex = 4;

        int LoopCount = 0;
        int sclx;
        int scly;
        Image[] ImageArr;
        PictureBox[,] PB;
        int[,] Board;
        int[,] LastBoard;
        List<int[,]> BoardsByNum;
        List<Point> SavedLocations;
        List<Point> SavedValues;
        List<Point> PointsForSuper;
        int[,] Reserve_Board;
        int[,] Reserve_LastBoard;
        List<int[,]> Reserve_BoardsByNum;
        List<Point> Reserve_SavedLocations;
        List<Point> Reserve_SavedValues;
        public Form1()
        {
            InitializeComponent();
            Setup();
            ShowBoard();
            this.KeyPreview = true;
            this.SizeChanged += Form1_SizeChanged;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                    QuickSetup();
                    ShowBoard();
                    break;

                case Keys.Z:

                    QuickSetup(); 
                    ShowHelpBoard(Next);
                    Next++;
                    Next %= 9;
                    break;

                case Keys.P:
                    QuickSetup();
                    Board = new int[,] {
                    { 0,1,0,9,2,0,0,7,0 },
                    { 3,6,0,0,5,0,0,0,0 },
                    { 0,0,0,0,4,0,0,0,0 },

                    { 0,0,0,0,0,0,0,0,0 },
                    { 6,0,0,0,0,0,5,4,0 },
                    { 0,8,2,7,0,0,0,9,0 },

                    { 0,0,0,0,0,0,0,0,9 },
                    { 0,0,8,0,0,4,0,0,0 },
                    { 0,0,0,8,0,0,3,0,6 } };
                    ShowBoard();
                    break;

                case Keys.O:
                    QuickSetup();
                    Board = new int[,] {
                    { 5,3,0,0,7,0,0,0,0 },
                    { 6,0,0,1,9,5,0,0,0 },
                    { 0,9,8,0,0,0,0,6,0 },
                    { 8,0,0,0,6,0,0,0,3 },
                    { 4,0,0,8,0,3,0,0,1 },
                    { 7,0,0,0,2,0,0,0,6 },
                    { 0,6,0,0,0,0,2,8,0 },
                    { 0,0,0,4,1,9,0,0,5 },
                    { 0,0,0,0,8,0,0,7,9 }};
                    ShowBoard();
                    break;

                case Keys.I:
                    QuickSetup();
                    Board = new int[,] {
                    { 0,5,0,2,0,0,8,0,4},
                    { 9,0,0,8,0,0,0,0,0 },
                    { 0,0,0,0,3,1,0,0,0 },
                    { 6,0,2,0,0,0,0,7,0 },
                    { 1,0,0,5,0,4,0,0,3 },
                    { 0,3,0,0,0,0,4,0,8 },
                    { 0,0,0,9,2,0,0,0,0 },
                    { 0,0,0,0,0,3,0,0,7 },
                    { 8,0,6,0,0,7,0,1,0 } };
                    ShowBoard();
                    break;

                case Keys.U:
                    QuickSetup();
                    Board = new int[,] {
                    { 0,0,0,0,0,0,0,0,0 },
                    { 0,0,0,0,0,3,0,8,5 },
                    { 0,0,1,0,2,0,0,0,0 },
                    { 0,0,0,5,0,7,0,0,0 },
                    { 0,0,4,0,0,0,1,0,0 },
                    { 0,9,0,0,0,0,0,0,0 },
                    { 5,0,0,0,0,0,0,7,3 },
                    { 0,0,2,0,1,0,0,0,0 },
                    { 0,0,0,0,4,0,0,0,9 }};
                    ShowBoard();
                    break;

                case Keys.Y:
                    QuickSetup();
                    Board = new int[,] {
                    { 0,0,0,0,8,0,0,4,0 },
                    { 1,0,0,7,0,0,5,0,0 },
                    { 0,5,4,3,0,0,7,0,0 },
                    { 0,0,0,0,0,0,9,1,0 },
                    { 6,0,0,0,0,0,0,0,4 },
                    { 0,7,9,0,0,0,0,0,0 },
                    { 0,0,6,0,0,7,8,5,0 },
                    { 0,0,5,0,0,3,0,0,6 },
                    { 0,1,0,0,4,0,0,0,0 }};
                    ShowBoard();
                    break;

                case Keys.S:

                    PointsForSuper = new List<Point>();
                    GetAllSuperPoints();
                    SuperSolve();
                    Solve();
                    ShowBoard();
                    break;

                case Keys.Q:
                    ResourceIndex++;
                    ResourceIndex %= 5;
                    Snap();
                    ShowBoard();
                    break;
                case Keys.Escape:
                    Close();
                    break;


                case Keys.D0:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 0;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D1:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 1;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D2:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 2;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D3:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 3;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D4:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 4;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D5:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 5;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D6:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 6;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D7:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 7;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D8:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 8;
                                ShowBoard();
                            }
                        }
                    }
                    break;
                case Keys.D9:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (CursorOn(PB[i, j].Location))
                            {
                                Board[i, j] = 9;
                                ShowBoard();
                            }
                        }
                    }
                    break;




                default:
                    break;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //ShowBoard();




            this.Controls.Clear();

            int x;
            sclx = (this.Width-15) / (cols + 1);
            scly = (this.Height-25) / (rows + 2);
            int y;
            int dist = 15;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    PictureBox Ptemp = new PictureBox()
                    {
                        Size = new Size(sclx - 2, scly - 2),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = ImageArr[Board[i, j]],
                        BorderStyle = BorderStyle.Fixed3D

                    };
                    x = j * sclx;
                    y = i * scly;
                    if (j > 2)
                        x += dist;
                    if (j > 5)
                        x += dist;

                    if (i > 2)
                        y += dist;
                    if (i > 5)
                        y += dist;


                    Ptemp.Location = new Point(x, y);

                    PB[i, j] = new PictureBox();
                    PB[i, j] = Ptemp;
                    PB[i, j].Click += PBClicked;
                    PB[i, j].MouseWheel += PBWheel;
                    this.Controls.Add(PB[i, j]);

                }
            }

            Button SolveButton = new Button
            {
                Text = "Solve",
                Size = new Size(PB[0, 4].Width, 20),
                BackColor = Color.White,
                Visible = true,
                Anchor = AnchorStyles.Bottom,
                Location = new Point(PB[0, 4].Location.X, (PB[8, 4].Location.Y + (PB[0, 0].Height + 5)))

            };
            SolveButton.Click += SolveClicked;
            this.Controls.Add(SolveButton);

        }
        void ShowBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    PB[i, j].Image = ImageArr[Board[i, j]];
                }
            }
        }
        private void SolveClicked(object sender, EventArgs e)
        {
            Solve();
        }
        private void SaveBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    LastBoard[i, j] = Board[i, j];
                }
            }

        }
        private bool IsBoardEqual()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (LastBoard[i, j] != Board[i, j])
                        return false;
                }
            }
            return true;
        }
        private void Solve()
        {
            bool Super = false;
            List<int> Results = new List<int>(3);
            for (int loop = 0; loop < 1; loop++)
            {
                do
                {
                    SaveBoard();
                    LoopCount++;
                    for (int i = 0; i < 9; i++)
                    {
                        
                        ExtremeSolve(SolveByNumber(i + 1), i + 1);
                        
                        SolveIfLonely();
                        SolveByNumber(i + 1);

                    }

                    if (IsBoardEqual())
                    {
                        Super = true;
                    /*
                        PointsForSuper = new List<Point>();
                        GetAllSuperPoints();
                        Results = SuperSolve();
                        if (Results[2] > 0)
                            Board[Results[0], Results[1]] = Results[2];
                    */
                    }
                 



                    ShowBoard();
                } while (!IsBoardEqual() && CheckBoard());
            }
            if (Super && !IsBoardFull())
            {
                PointsForSuper = new List<Point>();
                GetAllSuperPoints();
                //Results = SuperSolve();/////////////////////////////////
                if (Results.Count > 0 && Results[2] > 0)
                {
                    Board[Results[0], Results[1]] = Results[2];

                    ShowBoard();

                    Task t = Task.Delay(time);
                    t.Wait();

                    for (int k = SavedLocations.Count - 1; k >= 0; k--)
                    {
                        if (SavedLocations[k].X == Results[0] && SavedLocations[k].Y == Results[1])
                        {
                            SavedLocations.RemoveAt(k);
                            SavedValues.RemoveAt(k);
                        }
                    }



                    Super = false;
                    Solve();
                    return;
                }
            }
            if (CheckBoard())
            {
                MessageBox.Show("תקין\n מהלכים:" + LoopCount.ToString());
            }
            else
            {
                MessageBox.Show("לא תקין\n ");
            }
        } 


        private List<int> SuperSolve()
        {
            if (PointsForSuper.Count < 1)
            {
                MessageBox.Show("תקוע");
                return new List<int>(3);
            }

            List<int> PosNums = new List<int>(2);
            Point NextPoint = PointsForSuper.Last();
            PointsForSuper.RemoveAt(PointsForSuper.Count - 1);

            List<int> Results = new List<int>(3);
            Results.Add(NextPoint.X);
            Results.Add(NextPoint.Y);
            Results.Add(0);

            
            bool Legit = true;
            bool FirstNum = true;
            bool SecondNum = true;
            
            

            if (NextPoint.X >= 0)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (BoardsByNum[k][NextPoint.X, NextPoint.Y] != 0)
                    {
                        PosNums.Add(k + 1);
                    }
                }
                if (PosNums.Count != 2)
                    MessageBox.Show("Error PosNum");

                //מספר ראשון

                ReserveAllBoards();
                Board[NextPoint.X, NextPoint.Y] = PosNums[0];
                while (Legit)
                {
                    SaveBoard();
                    for (int i = 0; i < 9; i++)
                    {
                        ExtremeSolve(SolveByNumber(i + 1), i + 1);
                        SolveIfLonely();
                        SolveByNumber(i + 1);

                    }
                    if (!CheckBoard() || IsBoardEqual())
                    {
                        Legit = false;
                        if (!CheckBoard())
                        {
                            FirstNum = false;
                            RestoreAllBoards();
                            //Board[NextPoint.X, NextPoint.Y] = PosNums[1]; /////////////////
                            //return;
                        }
                    }
                }
                RestoreAllBoards();

                //מספר שני
                Legit = true;
                ReserveAllBoards();
                Board[NextPoint.X, NextPoint.Y] = PosNums[1];
                while (Legit)
                {
                    SaveBoard();
                    for (int i = 0; i < 9; i++)
                    {
                        ExtremeSolve(SolveByNumber(i + 1), i + 1);
                        SolveIfLonely();
                        SolveByNumber(i + 1);
                    }

                    if (!CheckBoard() || IsBoardEqual())
                    {
                        Legit = false;
                        if (!CheckBoard())
                            SecondNum = false; 
                        RestoreAllBoards();
                    }
                   // else
                     //   Next = 0;
                    SaveBoard();
                }
                RestoreAllBoards();


                if (FirstNum && !SecondNum)
                {
                    //Board[NextPoint.X, NextPoint.Y] = PosNums[0];
                    Results[2] = PosNums[0];
                    return Results;
                }
                else if (!FirstNum && SecondNum)
                {
                    //Board[NextPoint.X, NextPoint.Y] = PosNums[1];
                    Results[2] = PosNums[1];
                    return Results;
                }
                else if (!IsBoardFull())
                    return SuperSolve();
                //MessageBox.Show("תקוע");

            }
            return Results;

        }

        private void GetAllSuperPoints()
        {
            
            int count;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    count = 0;
                    for (int k = 0; k < 9; k++)
                    {
                        count += BoardsByNum[k][i, j];
                        if (count == 3 || Board[i, j] != 0)
                            k = 9;
                    }
                    if (count == 2)
                    {
                        PointsForSuper.Add(new Point(i, j));
                    }
                }
            }
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (Board[i, j] == 0)
                        return false;
                }
            }
            return true;
        }



        private bool CheckBoard()
        {

            for (int i = 0; i < rows; i++)
            {
                if (!ValidRows(i) || !ValidCols(i))
                {
                    return false;
                }
            }
            for (int i = 0; i < rows; i += 3)
            {
                for (int j = 0; j < cols; j += 3)
                {
                    if (!ValidSquare(i,j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ValidSquare(int x, int y)
        {
            int[] Nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = x; i < x+3; i++)
            {
                for (int j = y; j < y + 3; j++)
                {
                    if (Board[i, j] != 0)
                    {
                        if (Nums[(Board[i, j] - 1)] != 0)
                        {
                            Nums[(Board[i, j] - 1)] = 0;
                        }
                        else
                            return false;
                    }
                }

            }
            return true;
        }

        private bool ValidCols(int j)
        {
            int[] Nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < cols; i++)
            {
                if (Board[i, j] != 0)
                {
                    if (Nums[(Board[i, j] - 1)] != 0)
                    {
                        Nums[(Board[i, j] - 1)] = 0;
                    }
                    else
                        return false;

                }
            }
            return true;
        }

        private bool ValidRows(int i)
        {
            int[] Nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int j = 0; j < cols; j++)
            {
                if ( Board[i, j] != 0)
                {
                    if (Nums[(Board[i, j] - 1)] != 0)
                    {
                        Nums[(Board[i, j] - 1)] = 0;
                    }
                    else
                        return false; 
                }
            }
            return true;
        }

        private void RestoreAllBoards()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Board[i, j] = Reserve_Board[i, j];
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    LastBoard[i, j] = Reserve_LastBoard[i, j];
                }
            }

            BoardsByNum = new List<int[,]>();
            BoardsByNum = Reserve_BoardsByNum;

            SavedLocations = new List<Point>();
            SavedLocations = Reserve_SavedLocations;

            SavedValues = new List<Point>();
            SavedValues = Reserve_SavedValues;


            PointsForSuper = new List<Point>();
            GetAllSuperPoints();
        }

        private void ReserveAllBoards()
        {
            Reserve_Board  = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Reserve_Board[i, j] = Board[i, j];
                }
            }

            Reserve_LastBoard = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Reserve_LastBoard[i, j] = LastBoard[i, j];
                }
            }


            Reserve_BoardsByNum = new List<int[,]>();
            Reserve_BoardsByNum = BoardsByNum;

            Reserve_SavedLocations = new List<Point>();
            Reserve_SavedLocations = SavedLocations;

            Reserve_SavedValues = new List<Point>();
            Reserve_SavedValues = SavedValues;


        }


        private void SolveIfLonely()
        {


            int count;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    count = 0;
                    if (Board[i, j] == 0)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            if (BoardsByNum[k][i, j] == 1)
                            {
                                count++;
                            }
                        }
                        if (count == 1)
                        {
                            for (int k = 1; k <= 9; k++)
                            {
                                if (BoardsByNum[k-1][i, j] == 1)
                                {
                                    Board[i, j] = k;
                                    if (!CheckBoard())
                                        MessageBox.Show("Error\n" + i + "," + j + "\n" + k + "\nLonely");
                                    ShowBoard();

                                    Task t = Task.Delay(time);
                                    t.Wait();
                                }
                            }
                        }

                    }
                }

            }
        }

        private void ExtremeSolve(int[,] HelpBoard, int num)
        {
            for (int i = 0; i < rows; i+=3)
            {
                for (int j = 0; j < cols; j += 3)
                {
                    if (SquareCount(HelpBoard, i, j))
                    {
                        HelpBoard = ExtremeRows(HelpBoard, i, j);
                        HelpBoard = ExtremeCols(HelpBoard, i, j);
                    }
                }
            }

            //שורות

            List<int> indexes = new List<int>();
            List<int> TempIndex;

            for (int i = 0; i < rows; i++)
            {
                indexes = RowCount(HelpBoard,i);
                if (indexes.Count() == 2)
                {
                    for (int k = 0; k < rows; k++)
                    {
                        TempIndex = new List<int>();
                        TempIndex = RowCount(HelpBoard, k);
                        if (k != i && TempIndex.Count == 2)
                        {
                            if (indexes[0] == TempIndex[0] && indexes[1] == TempIndex[1])
                            {
                                for (int n = 0; n < rows; n++)
                                {
                                    if (n != i && n != k)
                                    {
                                        HelpBoard[n, indexes[0]] = 0;
                                        HelpBoard[n, indexes[1]] = 0;
                                    }
                                }
                            }
                        }
                    }
                    BoardsByNum[num - 1] = HelpBoard;


                    CheckOtherNumbersInRow(num, i, indexes);

                }
            }

            //טורים

            for (int i = 0; i < cols; i++)
            {
                indexes = ColCount(HelpBoard, i);
                if (indexes.Count() == 2)
                {
                    for (int k = 0; k < cols; k++)
                    {
                        TempIndex = new List<int>();
                        TempIndex = ColCount(HelpBoard, k);
                        if (k != i && TempIndex.Count == 2)
                        {
                            if (indexes[0] == TempIndex[0] && indexes[1] == TempIndex[1])
                            {
                                for (int n = 0; n < cols; n++)
                                {
                                    if (n != i && n != k)
                                    {
                                        HelpBoard[indexes[0], n] = 0;
                                        HelpBoard[indexes[1], n] = 0;
                                    }
                                }
                            }
                        }
                    }
                    BoardsByNum[num - 1] = HelpBoard;
                    CheckOtherNumbersInCol(num, i, indexes);
                }
            }


            BoardsByNum[num - 1] = HelpBoard;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (HelpBoard[i, j] == 1)
                    {
                        if (!CheckRows(HelpBoard, i, j) || !CheckCols(HelpBoard, i, j) || !CheckSquare(HelpBoard, i, j))
                        {
                            Board[i, j] = num;
                            if (!CheckBoard())
                                MessageBox.Show("Error\n" + i + "," +j + "\n" + num+"\nExtream");
                            ShowBoard();


                            Task t = Task.Delay(time);
                            t.Wait();
                            for (int k = SavedLocations.Count-1; k >=0 ; k--)
                            {
                                if (SavedLocations[k].X == i && SavedLocations[k].Y == j)
                                {
                                    SavedLocations.RemoveAt(k);
                                    SavedValues.RemoveAt(k);
                                }
                            }
                        }

                    }
                }
            }

        }

        private void CheckOtherNumbersInRow(int num, int CurrentRow, List<int> indexes)
        {
            List<int> TempIndex;
            for (int i = 0; i < cols; i++)
            {
                if (i + 1 != num)
                {
                    TempIndex = new List<int>();
                    TempIndex = RowCount(BoardsByNum[i], CurrentRow);

                    if (TempIndex.Count == 2)
                    {
                        if (indexes[0] == TempIndex[0] && indexes[1] == TempIndex[1])
                        {
                            Point Loc1 = new Point(CurrentRow, indexes[0]);
                            Point Value = new Point(num, i + 1);

                            Point Loc2 = new Point(CurrentRow, indexes[1]);

                            if (!SavedLocations.Contains(Loc1))
                            {
                                SavedLocations.Add(Loc1);
                                SavedLocations.Add(Loc2);

                                SavedValues.Add(Value);
                                SavedValues.Add(Value);
                            }

                        }
                    }

                }
            }
        }

        private void CheckOtherNumbersInCol(int num, int CurrentCol, List<int> indexes)
        {
            List<int> TempIndex;
            for (int i = 0; i < rows; i++)
            {
                if (i + 1 != num)
                {
                    TempIndex = new List<int>();
                    TempIndex = ColCount(BoardsByNum[i], CurrentCol);

                    if (TempIndex.Count == 2)
                    {
                        if (indexes[0] == TempIndex[0] && indexes[1] == TempIndex[1])
                        {
                            Point Loc1 = new Point(indexes[0], CurrentCol);
                            Point Value = new Point(num, i + 1);

                            Point Loc2 = new Point(indexes[1], CurrentCol);

                            if (!SavedLocations.Contains(Loc1))
                            {
                                SavedLocations.Add(Loc1);
                                SavedLocations.Add(Loc2);

                                SavedValues.Add(Value);
                                SavedValues.Add(Value);
                            }

                        }
                    }

                }
            }
        }

        private List<int> RowCount(int[,] helpBoard, int index)
        {
            List<int> count = new List<int>();
            for (int j = 0; j < cols; j++)
            {
                if (helpBoard[index, j] == 1)
                    count.Add(j);
            }
            return count;
        }
        private List<int> ColCount(int[,] helpBoard, int index)
        {
            List<int> count = new List<int>();
            for (int i = 0; i < rows; i++)
            {
                if (helpBoard[i,index] == 1)
                    count.Add(i);
            }
            return count;
        }

        private int[,] ExtremeCols(int[,] helpBoard, int a, int b)
        {
            int col = -1;
            bool first = true;
            for (int i = a; i < a + 3; i++)
            {
                for (int j = b; j < b + 3; j++)
                {
                    if (helpBoard[i, j] == 1)
                    {
                        if (first)
                        {
                            first = false;
                            col = j;
                        }
                        else if (j != col)
                            col = -1;
                    }

                }
            }
            if (col != -1)
            {

                helpBoard = CleanCol(helpBoard, col, a);
            }
            return helpBoard;
        }

        private int[,] CleanCol(int[,] helpBoard, int col, int a)
        {
            for (int i = 0; i < rows; i++)
            {
                if (i == a)
                    i += 2;
                else
                {
                    helpBoard[i,col] = 0;
                }
            }
            return helpBoard;
        }

        private int[,] ExtremeRows(int[,] helpBoard, int a, int b)
        {
            int row = -1;
            bool first = true;
            for (int i = a; i < a + 3; i++)
            {
                for (int j = b; j < b + 3; j++)
                {
                    if (helpBoard[i, j] == 1)
                    {
                        if (first)
                        {
                            first = false;
                            row = i;
                        }
                        else if (i != row)
                            row = -1;
                    }
                    
                }
            }
            if (row != -1)
            {
                helpBoard=CleanRow(helpBoard, row, b);
            }

            return helpBoard;

        }

        private int[,] CleanRow(int[,] helpBoard, int row, int b)
        {
            for (int j = 0; j < cols; j++)
            {
                if (j == b)
                    j += 2;
                else
                {
                    helpBoard[row, j] = 0;
                }
            }
            return helpBoard;
        }

        private bool SquareCount(int[,] helpBoard, int a, int b)
        {
            int count = 0;
            for (int i = a; i < a+3; i ++)
            {
                for (int j = b; j < b+3; j ++)
                {
                    if (helpBoard[i, j] == 1)
                        count++;
                }
            }
            if (count > 1 && count < 4)
                return true;
            return false;
        }

        private void PBWheel(object sender, MouseEventArgs e)
        {
            Point P = new Point(Cursor.Position.X - this.Location.X - 10, Cursor.Position.Y - this.Location.Y - 33);
            MouseEventArgs Me = (MouseEventArgs)e;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (CursorOn(PB[i, j].Location))
                    {
                        if (Me.Delta > 0)
                        {
                            Board[i, j]++;
                            Board[i, j] %= (rows+1);
                            PB[i, j].Image = ImageArr[Board[i, j]];
                        }
                        else if (Me.Delta < 0)
                        {
                            Board[i, j]--;
                            if (Board[i, j] < 0)
                                Board[i, j] = rows;
                            PB[i, j].Image = ImageArr[Board[i, j]];
                        }
                    }
                }
            }
        }

        
        private void PBClicked(object sender, EventArgs e)
        {
            

            Point P = new Point(Cursor.Position.X - this.Location.X - 10, Cursor.Position.Y - this.Location.Y - 33);
            MouseEventArgs Me = (MouseEventArgs)e;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (CursorOn(PB[i, j].Location))
                    {
                        //MessageBox.Show(i.ToString() + "," + j.ToString());
                        if (Me.Button == MouseButtons.Left)
                        {
                            Board[i, j]++;
                            Board[i, j] %= (rows + 1);
                            PB[i, j].Image = ImageArr[Board[i, j]];
                        }
                        else if (Me.Button == MouseButtons.Right)
                        {
                            Board[i, j]--;
                            if (Board[i, j] < 0)
                                Board[i, j] = rows;
                            PB[i, j].Image = ImageArr[Board[i, j]];
                        }
                    }
                }
            }
        }
        private bool CursorOn(Point pic)
        {
            Point P = new Point(Cursor.Position.X - this.Location.X -10 , Cursor.Position.Y - this.Location.Y  - 29);
            if (P.X>pic.X&& P.X<pic.X+(sclx-2))
                if(P.Y > pic.Y+5 && P.Y < pic.Y + 5+scly)/////////////////////////////
                return true;
            return false;
        }
        void Snap()
        {
            ImageArr = new Image[10];

            Rectangle R;
            int index = 0;
            Image image = WindowsFormsApp1.Properties.Resources.numbers;
            int height = image.Height / 2;
            int width = image.Width / 5;


            if (ResourceIndex == 0)
            {
                image = WindowsFormsApp1.Properties.Resources.numbers;
                height = image.Height / 2;
                width = image.Width / 5;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        R = new Rectangle(width * j, height * i, width, height);
                        ImageArr[index] = WindowsFormsApp1.Properties.Resources.numbers.Clone(R, new Bitmap(image).PixelFormat);
                        index++;
                    }
                }
            }
            if (ResourceIndex == 1)
            {
                image = WindowsFormsApp1.Properties.Resources.numbers1;
                height = image.Height / 2;
                width = image.Width / 5;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        R = new Rectangle(width * j, height * i, width, height);
                        ImageArr[index] = WindowsFormsApp1.Properties.Resources.numbers1.Clone(R, new Bitmap(image).PixelFormat);
                        index++;
                    }
                }
            }
            if (ResourceIndex == 2)
            {
                image = WindowsFormsApp1.Properties.Resources.numbers2;
                height = image.Height / 2;
                width = image.Width / 5;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        R = new Rectangle(width * j, height * i, width, height);
                        ImageArr[index] = WindowsFormsApp1.Properties.Resources.numbers2.Clone(R, new Bitmap(image).PixelFormat);
                        index++;
                    }
                }
            }
            if (ResourceIndex == 3)
            {
                image = WindowsFormsApp1.Properties.Resources.numbers3;
                height = image.Height / 2;
                width = image.Width / 5;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        R = new Rectangle(width * j, height * i, width, height);
                        ImageArr[index] = WindowsFormsApp1.Properties.Resources.numbers3.Clone(R, new Bitmap(image).PixelFormat);
                        index++;
                    }
                }
            }
            if (ResourceIndex == 4)
            {
                image = WindowsFormsApp1.Properties.Resources.numbers4;
                height = image.Height / 2;
                width = image.Width / 5;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        R = new Rectangle(width * j, height * i, width, height);
                        ImageArr[index] = WindowsFormsApp1.Properties.Resources.numbers4.Clone(R, new Bitmap(image).PixelFormat);
                        index++;
                    }
                }
            }


            R = new Rectangle(0, 0, 10, 10);
            ImageArr[0] = WindowsFormsApp1.Properties.Resources.numbers.Clone(R, new Bitmap(image).PixelFormat);

        }
        void QuickSetup()
        {
            LoopCount = 0;

            Next = 0;

            Board = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Board[i, j] = 0;
                    PB[i, j].BackColor = Color.White;

                }
            }


            SavedValues = new List<Point>();
            SavedLocations = new List<Point>();

            BoardsByNum = new List<int[,]>(9);
            int[,] HelpBoard;
            for (int k = 0; k < 9; k++)
            {
                HelpBoard = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (Board[i, j] == 0)
                            HelpBoard[i, j] = 1;
                        else
                            HelpBoard[i, j] = 0;
                    }
                }

                BoardsByNum.Add(HelpBoard);
            }
        }
        void Setup()
        {
            Next = 0;
            this.Size = new Size(500, 550);
            Snap();
            LoopCount = 0;
            Board = new int[rows, cols];
            LastBoard = new int[rows, cols];
            PB = new PictureBox[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Board[i, j] = 0;

                    LastBoard[i, j] = 0;
                }
            }




            this.Controls.Clear();

            int x;
            sclx = this.Width / (cols + 1);
            scly = (this.Height-15) / (rows + 2);
            int y;
            int dist = 15;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    PictureBox Ptemp = new PictureBox()
                    {
                        Size = new Size(sclx - 2, scly - 2),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = ImageArr[Board[i, j]],
                        BorderStyle = BorderStyle.Fixed3D

                    };
                    x = j * sclx;
                    y = i * scly;
                    if (j > 2)
                        x += dist;
                    if (j > 5)
                        x += dist;

                    if (i > 2)
                        y += dist;
                    if (i > 5)
                        y += dist;


                    Ptemp.Location = new Point(x, y);

                    PB[i, j] = new PictureBox();
                    PB[i, j] = Ptemp;
                    PB[i, j].Click += PBClicked;
                    PB[i, j].MouseWheel += PBWheel;
                    this.Controls.Add(PB[i, j]);

                }
            }

            Button SolveButton = new Button
            {
                Text = "Solve",
                Size = new Size(PB[0, 4].Width, 20),
                BackColor = Color.White,
                Visible = true,
                Anchor = AnchorStyles.Bottom,
                Location = new Point(PB[0, 4].Location.X, (PB[8, 4].Location.Y + (PB[0, 0].Height + 5)))

            };
            SolveButton.Click += SolveClicked;
            this.Controls.Add(SolveButton);

            SavedValues = new List<Point>();
            SavedLocations = new List<Point>();

            BoardsByNum = new List<int[,]>(9);
            int[,] HelpBoard;
            for (int k = 0; k < 9; k++)
            {
                HelpBoard = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (Board[i, j] == 0)
                            HelpBoard[i, j] = 1;
                        else
                            HelpBoard[i, j] = 0;
                    }
                }

                BoardsByNum.Add(HelpBoard);
            }


        }

       

        int[,] SolveByNumber(int num)
        {
            int x;
            int[,] HelpBoard;
            HelpBoard = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == 7 && j == 7 && num ==5)
                        x = 1;
                    if (Board[i, j] == 0)
                        HelpBoard[i, j] = 1;
                    else
                        HelpBoard[i, j] = 0;

                    for (int k = 0; k < SavedLocations.Count; k++)
                    {
                        if (SavedLocations[k].X == i && SavedLocations[k].Y == j)
                        {
                            if(num!= SavedValues[k].X && num != SavedValues[k].Y)
                                HelpBoard[i, j] = 0;
                        }
                    }
                }
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (Board[i, j] == num)
                    {
                        for (int k = 0; k < rows; k++)
                        {
                            HelpBoard[i, k] = 0;
                            HelpBoard[k, j] = 0;                           
                        }
                        HelpBoard = DeleteSquare(i, j, HelpBoard);
                        
                    }
                }
            }
            BoardsByNum[num - 1] = HelpBoard;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (HelpBoard[i, j]==  1)
                    {
                        if (!CheckRows(HelpBoard, i, j) || !CheckCols(HelpBoard, i, j) || !CheckSquare(HelpBoard, i, j))
                        {
                            Board[i, j] = num;
                            if (!CheckBoard())
                                MessageBox.Show("Error\n"+ i+","+ j+"\n"+ num + "\nNumber");
                            ShowBoard();


                            Task t = Task.Delay(time);
                            t.Wait();

                            for (int k = SavedLocations.Count - 1; k >= 0; k--)
                            {
                                if (SavedLocations[k].X == i && SavedLocations[k].Y == j)
                                {
                                    SavedLocations.RemoveAt(k);
                                    SavedValues.RemoveAt(k);
                                }
                            }

                        }
                        
                    }
                }
            }
            
            return HelpBoard;
        }

        private bool CheckSquare(int[,] TB, int i, int j)
        {
            int a = 0, b = 0;
            if (i > 2)
                a += 3;
            if (i > 5)
                a += 3;
            if (j > 2)
                b += 3;
            if (j > 5)
                b += 3;
            for (int k = a; k < a+3; k++)
            {
                for (int m = b; m < b+3; m++)
                {
                    if (k != i || m != j)
                    {
                        if (TB[k, m] == 1)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool CheckRows(int[,] b, int i, int j)
        {
            for (int a = 0; a < rows; a++)
            {
                if (b[i, a] == 1 && a != j)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckCols(int[,] b, int i, int j)
        {
            for (int a = 0; a < rows; a++)
            {
                if (b[a, j] == 1 && a != i)
                {
                    return true;
                }
            }
            return false;
        }
        private int[,] DeleteSquare(int i, int j, int[,] B)
        {
            switch (i%3)
            {
                case 0:
                    B = Delete2Next(B, i, j);
                    if (j % 3 == 0)
                    {
                        B = Delete2Next(B, i, j+1);
                        B = Delete2Next(B, i , j+2);
                    }
                    else if (j % 3 == 1)
                    {
                        B = Delete2Next(B, i,j+1);
                        B = Delete2Next(B, i,j-1);
                    }
                    else if (j % 3 == 2)
                    {
                        B = Delete2Next(B, i ,j-1);
                        B = Delete2Next(B, i ,j-2);
                    }
                    break;

                case 1:
                    B = DeleteNextBack(B, i, j);
                    if (j % 3 == 0)
                    {
                        B = DeleteNextBack(B, i, j + 1);
                        B = DeleteNextBack(B, i, j + 2);
                    }
                    else if (j % 3 == 1)
                    {
                        B = DeleteNextBack(B, i, j + 1);
                        B = DeleteNextBack(B, i, j - 1);
                    }
                    else if (j % 3 == 2)
                    {
                        B = DeleteNextBack(B, i, j - 1);
                        B = DeleteNextBack(B, i, j - 2);
                    }
                    break;

                case 2:
                    B = Delete2Back(B, i, j);
                    if (j % 3 == 0)
                    {
                        B = Delete2Back(B, i, j + 1);
                        B = Delete2Back(B, i, j + 2);
                    }
                    else if (j % 3 == 1)
                    {
                        B = Delete2Back(B, i, j + 1);
                        B = Delete2Back(B, i, j - 1);
                    }
                    else if (j % 3 == 2)
                    {
                        B = Delete2Back(B, i, j - 1);
                        B = Delete2Back(B, i, j - 2);
                    }
                    break;

                default:
                    break;
            }





            return B;
        }

        private int[,] DeleteNextBack(int[,] b, int i, int j)
        {
            b[i, j] = 0;
            b[i+1, j] = 0;
            b[i-1, j] = 0;
            return b;
        }

        private int[,] Delete2Back(int[,] b, int i, int j)
        {
            b[i, j] = 0;
            b[i-1, j] = 0;
            b[i-2, j] = 0;
            return b;
        }

        private int[,] Delete2Next(int[,] b, int i, int j)
        {
            b[i, j] = 0;
            b[i+1, j] = 0;
            b[i+2, j] = 0;
             return b;
        }

        void ShowHelpBoard(int Num)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    PB[i, j].Image = ImageArr[BoardsByNum[Num][i, j]];
                }
            }
        }
    }

}