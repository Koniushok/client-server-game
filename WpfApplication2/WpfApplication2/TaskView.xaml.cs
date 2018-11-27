using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games;
using Client;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для TaskView.xaml
    /// </summary>
    public partial class TaskView : ContentControl
    {
        Style Selected;
        Style SelectedEnemy;
        Style MyAndEnemy;
        Style IsFalse;
        bool Ansver;
        GameTask Task { get; set; }

        public TaskView()
        {
            
            InitializeComponent();
            Selected = (Style)FindResource("Selected");
            SelectedEnemy = (Style)FindResource("SelectedEnemy");
            MyAndEnemy = (Style)FindResource("MyAndEnemy");
           IsFalse = (Style)FindResource("IsFalse");
        }

        public void Start(GameTask task, bool enabled)
        {
            Task = task;

            ShowTask();

            Ansver = true;

            if (!enabled)
            {
                AnsverOff();
            }
        }


        private void AnsverClick(object sender, RoutedEventArgs e)
        {
            if (Ansver)
            {
                Button butt = sender as Button;

                AnswerButton0.Style = IsFalse;
                AnswerButton1.Style = IsFalse;
                AnswerButton2.Style = IsFalse;
                AnswerButton3.Style = IsFalse;

                butt.Style = Selected;

                Ansver = false;
               
                int ansver = GetNum(butt);
                MainClient.GameServer.AnswerResult(ansver);
            }

        }

        void ShowTask()
        {
            if (Task is TaskAnswer)
            {
                TaskAnswer task = Task as TaskAnswer;

                AnswerText0.Text = task.Answers[0];
                AnswerText1.Text = task.Answers[1];
                AnswerText2.Text = task.Answers[2];
                AnswerText3.Text = task.Answers[3];

                Question.Text = task.Question;

            }

        }

        public void ResulAnswer(int correctAnswer)
        {
            Button butt = GetButton(correctAnswer);
            if (butt != null)
            {
                butt.BorderBrush = new SolidColorBrush(Color.FromRgb(253, 255, 0));
                butt.BorderThickness = new Thickness(4);              
            }

        }

        public void ResultTask(int my,int enemy)
        {
            Ansver = false;

            Button myBut=GetButton(my);
            Button enemyBut=GetButton(enemy);

            if(myBut!=null)
            myBut.Style = Selected;

            if (enemyBut != null)
                enemyBut.Style = SelectedEnemy;

            if(myBut == enemyBut && myBut!=null)
            {
                myBut.Style = MyAndEnemy;
            }

        }

        void AnsverOff()
        {
            AnswerButton0.IsEnabled = false;
            AnswerButton1.IsEnabled = false;
            AnswerButton2.IsEnabled = false;
            AnswerButton3.IsEnabled = false;
        }

        Button GetButton(int k)
        {
            switch (k)
            {
                case 0:
                    {
                        return AnswerButton0;
                    }
                case 1:
                    {
                        return AnswerButton1;
                    }
                case 2:
                    {
                        return AnswerButton2;
                    }
                case 3:
                    {
                        return AnswerButton3;
                    }
            }
            return null;
        }

        int GetNum(Button but)
        {
            if(but==AnswerButton0)
            {
                return 0;
            }
            if (but == AnswerButton1)
            {
                return 1;
            }
            if (but == AnswerButton2)
            {
                return 2;
            }
            if (but == AnswerButton3)
            {
                return 3;
            }
            return -1;
        }
    }

    public class TextBlockFilled : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
        typeof(string),
        typeof(TextBlockFilled));

        public string Text
        {
            get { return GetValue(TextProperty) as string; }
            set { SetValue(TextProperty, value); }
        }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            FormattedText formattedText = formattedText = new FormattedText(
            Text,
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            new Typeface("Verdana"),
            ActualHeight,
            Brushes.White);
            formattedText.TextAlignment = TextAlignment.Center;
            formattedText.MaxTextWidth = ActualWidth - Padding.Left - Padding.Right;
            formattedText.Trimming = TextTrimming.None;
            double step = ActualHeight / 20;
            for (double i = ActualHeight - step; i >= step; i -= step)
            {
                if (formattedText.Height <= ActualHeight - Padding.Top - Padding.Bottom) break;
                formattedText.SetFontSize(i);
            }
            formattedText.MaxTextHeight = ActualHeight;
            drawingContext.DrawText(formattedText, new Point(Padding.Left, Padding.Top));
        }
    }
}
