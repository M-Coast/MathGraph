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

using Coast.Math;
using Coast.Math.Expression;

namespace Demo_Expression
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            ////System.Windows.Expression

            //Lex lex = new Lex();

            ////lex.DebugTest();


            //ExpressionCode = "1+2+3*4+5/6-7*8\r\n100,-1,+1,-1.0,+1.0\r\nz=a*x+b*y+c";
            ExpressionCode = "z=a*x+b*y+2";

            //z = +a * -x * (b * y + 1)//形如这样的编译能通过，需要注意

            ExpressionCode = "z=3*x+4*y+5" + "+sin(23)+cos(0)+pow(2,3)";




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TestLex();


            Test2();
        }

        private void TestLex()
        {
            Lex lex = new Lex();
            List<Token> tokenList = lex.DebugTest(ExpressionCode);

            string text = string.Empty;

            foreach (Token t in tokenList)
            {
                text += t.ToString_S() + "\r\n";
            }

            TokenListText = text;
        }

        private void Test2()
        {
            Syntax syntax = new Syntax();

            syntax.DebugTest(ExpressionCode);

           string s =  syntax.GenerateCode(syntax.Expressions);

            ExpressionTreeText = s;

            Dictionary<string, double> vt = new Dictionary<string, double>();

            vt.Add("z", 0);
            vt.Add("x", 7);
            vt.Add("y", 8);

            Evaluator ev = new Evaluator();

            ev.Solve(syntax.Expressions, vt);

            //List<string> ops = ev.EVStack.Operations;


            //ExpressionTreeText += "EV:" + "\r\n";
            //foreach (string op in ops)
            //{
            //    ExpressionTreeText += op + "\r\n";
            //}

            ExpressionTreeText +="z = " + vt["z"].ToString();




            double z = 0;

            for(int i=0;i<10000;i++)
            {
                vt["x"] = i;
                vt["y"] = i + 1;
                ev.Solve(syntax.Expressions, vt);
                z = vt["z"];
            }



        }



        public string ExpressionCode
        {
            get { return (string)GetValue(ExpressionCodeProperty); }
            set { SetValue(ExpressionCodeProperty, value); }
        }
        
        public static readonly DependencyProperty ExpressionCodeProperty =
            DependencyProperty.Register("ExpressionCode", typeof(string), typeof(MainWindow), new PropertyMetadata());
        

        public string TokenListText
        {
            get { return (string)GetValue(TokenListTextProperty); }
            set { SetValue(TokenListTextProperty, value); }
        }
        
        public static readonly DependencyProperty TokenListTextProperty =
            DependencyProperty.Register("TokenListText", typeof(string), typeof(MainWindow), new PropertyMetadata());
        

        public string ExpressionTreeText
        {
            get { return (string)GetValue(ExpressionTreeTextProperty); }
            set { SetValue(ExpressionTreeTextProperty, value); }
        }
    
        public static readonly DependencyProperty ExpressionTreeTextProperty =
            DependencyProperty.Register("ExpressionTreeText", typeof(string), typeof(MainWindow), new PropertyMetadata());


    }
}
