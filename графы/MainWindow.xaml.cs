using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;

namespace графы
{
    public partial class MainWindow : Window
    {
        int[,] Mat = new int[0,0];
        Line[,] lines = new Line[0,0];
        int some_indx=0;
        int elips_indx = 0;
        int line_indx = 0;
        int I = 0;
        int eve=0;
        versh A1;
        List<versh> vershes = new List<versh>();

        /*public class pop
        {
            public int[] S;
            public List<versh> Vershes;
            public Line[,] Lines;
            public TextBlock Output;
            public pop(int[] S1, List<versh> V, Line[,] L, TextBlock O)
            {
                S = S1;
                Vershes = V;
                Lines = L;
                Output = O;
            }
        }*/

        public class versh
        {
            public int index;
            public int number;
            public Point kord;
            public Ellipse El;
            public TextBlock Num = new TextBlock();
            public versh(int i, Point pp, int num, Ellipse A, TextBlock B)
            {
                index = i;
                kord.X = pp.X - 14;
                kord.Y = pp.Y - 14;
                number = num;
                El = A;
                Num = B;
            }
        }

        public void creat_ellips(Point pp)
        {
            if ((pp.Y <= 49 && pp.X <= 83) || (pp.Y <= 250 && pp.X >= 1050))
                return;
            foreach (versh A in vershes)
            {
                if (Math.Pow((pp.X - 14) - A.kord.X, 2) + Math.Pow((pp.Y - 14) - A.kord.Y, 2) <= Math.Pow(43, 2))
                    return;
            }

            Ellipse El = new Ellipse();
            TextBlock num = new TextBlock();
            num.Text = Convert.ToString(elips_indx+1);
            num.Foreground = new SolidColorBrush(Colors.White);
            El.Width = 30;
            El.Height = 30;
            El.Fill = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            Canvas.SetLeft(El, pp.X-14);
            Canvas.SetTop(El, pp.Y-14);
            Canvas.SetLeft(num, pp.X-4);
            Canvas.SetTop(num, pp.Y-8);
            El.MouseRightButtonDown += r;
            num.MouseRightButtonDown += r;
            hub.Children.Insert(some_indx, El);
            hub.Children.Insert(some_indx+1, num);
            versh a = new versh(some_indx, pp, elips_indx, El, num);
            vershes.Add(a);
            some_indx +=2;
            elips_indx++;

            int[,] mat = Mat;
            Mat = new int[elips_indx, elips_indx];
            for(int i = 0; i < Math.Sqrt(mat.Length); i++)
                for (int j = 0; j < Math.Sqrt(mat.Length); j++)
                {
                    Mat[i, j] = mat[i, j];
                }
                    ;
        }
        public void ReCreat(versh A)
        {
            Ellipse El = new Ellipse();
            TextBlock num = new TextBlock();
            num.Text = Convert.ToString(A.number + 1);
            num.Foreground = new SolidColorBrush(Colors.White);
            El.Width = 30;
            El.Height = 30;
            El.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Canvas.SetLeft(El, A.kord.X);
            Canvas.SetTop(El, A.kord.Y);
            Canvas.SetLeft(num, A.kord.X + 10);
            Canvas.SetTop(num, A.kord.Y + 6);
            El.MouseRightButtonDown += r;
            num.MouseRightButtonDown += r;
            hub.Children.Insert(some_indx, El);
            hub.Children.Insert(some_indx + 1, num);
            some_indx += 2;
        }
        public void creat_line(versh A)
        {
            if (line_indx % 2 == 0)
            {
                A1 = A;
            }
            else
            {
                A1.El.Stroke = Brushes.Black;
                Line Ln = new Line();
                Ln.X1 = A1.kord.X+14;
                Ln.Y1 = A1.kord.Y+14;
                Ln.X2 = A.kord.X+14;
                Ln.Y2 = A.kord.Y+14;
                Ln.StrokeThickness = 4;
                Ln.Stroke = new SolidColorBrush(Colors.Black);
                hub.Children.Insert(some_indx, Ln);
                some_indx++;
                ReCreat(A1);
                ReCreat(A);
                Mat[A.number, A1.number] = 1;
                Mat[A1.number, A.number] = 1;

                Line[,] mat = lines;
                lines = new Line[elips_indx, elips_indx];
                for (int i = 0; i < Math.Sqrt(mat.Length); i++)
                    for (int j = 0; j < Math.Sqrt(mat.Length); j++)
                    {
                        lines[i, j] = mat[i, j];
                    }
                lines[A.number, A1.number] = Ln;
                lines[A1.number, A.number] = Ln;
                ;
            }
            line_indx++;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void t(object sender, MouseButtonEventArgs e)
        {
            Point pp = e.GetPosition(this);
            creat_ellips(pp);
        }
        private void r(object sender, MouseButtonEventArgs e)
        {
            Point pp = e.GetPosition(this);
            foreach (versh A in vershes)
            {
                if (Math.Pow((pp.X - 14) - A.kord.X, 2) + Math.Pow((pp.Y - 14) - A.kord.Y, 2) <= Math.Pow(15, 2))
                    creat_line(A);
            }
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string ss = "";
            for (int i = 0; i < Math.Sqrt(Mat.Length)+1; i++)
            {
                if (i == 0)
                    ss += "   ";
                else
                    ss += i;
                ss += " ";
                for (int j = 0; j < Math.Sqrt(Mat.Length); j++)
                {
                    if(i==0)
                    {
                        ss += j + 1;
                        ss += " ";
                        continue;
                    }
                    ss += " ";
                    ss += Mat[i-1, j];
                }
                ss += "\n";
            }
            outPut.Text = ss;
        }
        //проверка и реализация
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            int[,] mat = new int[(int)Math.Sqrt(Mat.Length), (int)Math.Sqrt(Mat.Length)];
            for (int i = 0; i < Math.Sqrt(Mat.Length); i++)
                for (int j = 0; j < Math.Sqrt(Mat.Length); j++)
                    mat[i, j] = Mat[i, j];

            int[] S = new int[(line_indx/2)+1];
            int[] C = new int[(line_indx / 2) + 1];
            int I=0, J=0, prov=0, provCh=0;
            string ss = "";

            //проверка на элеров цикл
            for (int i = 0; i < Math.Sqrt(Mat.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(Mat.Length); j++)
                {
                    provCh += Mat[i, j];
                }
                if (provCh % 2 == 1)
                    break;
                provCh = 0;
            }
            if (provCh % 2 == 1)
            {
                provCh = 0;
                for (int i = 0; i < Math.Sqrt(Mat.Length); i++)
                {
                    for (int j = 0; j < Math.Sqrt(Mat.Length); j++)
                    {
                        provCh += Mat[i, j];
                    }
                    if (provCh % 2 == 1)
                        prov++;
                    if (prov >= 2)
                        break;
                }
            }
            if (prov == 2)
            {
                ss += "не эйлеров графов";
                outPut.Text = ss;
                return;
            }
            //выбр начальной вершины
            if (prov == 0)
                S[0] = 0;
            else
            {
                provCh = 0;
                for (int i = 0; i < Math.Sqrt(Mat.Length); i++)
                {
                    for (int j = 0; j < Math.Sqrt(Mat.Length); j++)
                    {
                        provCh += Mat[i, j];
                    }
                    if (provCh % 2 == 1)
                        S[0] = i;
                }
            }
            //построение элерова цикла или пути
            while (true)
            {
                if(serchVer(mat, S[I])!=-1)
                {
                    S[I + 1] = serchVer(mat, S[I]);
                    mat[S[I], S[I + 1]] = 0;
                    mat[S[I+1], S[I]] = 0;
                    I++;
                }
                else
                {
                    //цикл или путь найден
                    if(symMat(mat)==0)
                    {
                        if( eve >= S.Length - 1)
                        {
                            eve = 0;
                            draw(S);
                            return;
                        }
                        if (prov != 0)
                            ss += "элеров путь:\n";
                        else
                            ss += "эйлеров цикл:\n";
                        foreach(int A in S)
                        {
                            ss += A+1;
                            ss += " ";
                        }
                        outPut.Text = ss;
                        draw1(S,eve);
                        eve++;
                        return;
                    }
                    else
                    {
                        C[J] = S[I];
                        S[I] = 0;
                        I--;
                        J++;
                        if (serchVer(mat, S[I]) == -1)
                            continue;
                        S[I + 1] = serchVer(mat, S[I]);
                        mat[S[I], S[I + 1]] = 0;
                        mat[S[I + 1], S[I]] = 0;
                        I++;

                        mat[S[I-1], C[J - 1]] = 1;
                        mat[C[J - 1], S[I-1]] = 1;
                        for (int i=0; i<J-1; i++)
                        {
                            mat[C[i], C[i + 1]] = 1;
                            mat[C[i+1], C[i]] = 1;
                        }
                    }
                }
            }
        }//конец метода

        public int serchVer(int[,] mat, int ver)
        {
            for(int i=0;i< Math.Sqrt(mat.Length);i++)
            {
                if (mat[ver, i] == 1)
                    return i;
            }
            return -1;
        }
        public int symMat(int[,] mat)
        {
            int sum = 0;
            foreach (int A in mat)
                sum += A;
            return sum;
        }

        public void draw1(int[] S, int eve)
        {
            ColorAnimation col1 = new ColorAnimation();
            col1.From = Colors.Black;
            col1.To = Colors.Blue;
            col1.Duration = TimeSpan.FromSeconds(0);
            lines[S[eve], S[eve + 1]].Stroke.BeginAnimation(SolidColorBrush.ColorProperty, col1);
        }
        public void draw(int[] S)
        {
            foreach(UIElement lin in lines)
            {
                if(lin is Line)
                    ((Line)lin).Stroke = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
