/*****************************************************************************************

    MathGraph
    
    Copyright (C)  Coast


    AUTHOR      :  Coast   
    DATE        :  2020/8/27
    DESCRIPTION :  

 *****************************************************************************************/

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

namespace Coast.Controls
{
    public class TitledValueControl : Control
    {
        static TitledValueControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitledValueControl), new FrameworkPropertyMetadata(typeof(TitledValueControl)));
        }

        public TitledValueControl()
        {

        }


        //Title:
        //  Foreground <- TitleForeground
        //  Backgroud  <- Background(Control)
        //
        //Value:
        //  Foreground <- Foreground(Control)
        //  Backgroud  <- ValueBackground
        //


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TitledValueControl), new PropertyMetadata(string.Empty));
        


        public TextAlignment TitleTextAlignment
        {
            get { return (TextAlignment)GetValue(TitleTextAlignmentProperty); }
            set { SetValue(TitleTextAlignmentProperty, value); }
        }
        
        public static readonly DependencyProperty TitleTextAlignmentProperty =
            DependencyProperty.Register("TitleTextAlignment", typeof(TextAlignment), typeof(TitledValueControl), new PropertyMetadata(TextAlignment.Left));

        

        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }
        
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(TitledValueControl), new PropertyMetadata());

        
        public GridLength TitleWidth
        {
            get { return (GridLength)GetValue(TitleWidthProperty); }
            set { SetValue(TitleWidthProperty, value); }
        }
        
        public static readonly DependencyProperty TitleWidthProperty =
            DependencyProperty.Register("TitleWidth", typeof(GridLength), typeof(TitledValueControl), new PropertyMetadata(GridLength.Auto));

        



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(TitledValueControl), new PropertyMetadata());


        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }
        
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(TitledValueControl), new PropertyMetadata());

        

        public TextAlignment ValueTextAlignment
        {
            get { return (TextAlignment)GetValue(ValueTextAlignmentProperty); }
            set { SetValue(ValueTextAlignmentProperty, value); }
        }
        
        public static readonly DependencyProperty ValueTextAlignmentProperty =
            DependencyProperty.Register("ValueTextAlignment", typeof(TextAlignment), typeof(TitledValueControl), new PropertyMetadata(TextAlignment.Right));


        public Brush ValueBackground
        {
            get { return (Brush)GetValue(ValueBackgroundProperty); }
            set { SetValue(ValueBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ValueBackgroundProperty =
            DependencyProperty.Register("ValueBackground", typeof(Brush), typeof(TitledValueControl), new PropertyMetadata());




        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TitledValueControl), new PropertyMetadata(false));



    }
}
