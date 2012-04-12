namespace Cirrious.Conference.UI.WP7.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// Show http or https as Hyperlinks in a RichTextBox
    /// </summary>
    public class HtmlTextBox : ContentControl
    {
        private readonly Paragraph ContentParagraph;

        public static readonly DependencyProperty HtmlTextProperty = DependencyProperty.Register("HtmlText", typeof(RichTextBox), typeof(HtmlTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty HtmlContentProperty = DependencyProperty.Register("HtmlContent", typeof(string), typeof(HtmlTextBox), new PropertyMetadata(OnHtmlContentChanged));

        public static readonly DependencyProperty UrlForeColorProperty = DependencyProperty.Register("UrlForeColor", typeof(SolidColorBrush), typeof(HtmlTextBox), new PropertyMetadata(OnUrlForeColorPropertyChanged));

        private static void OnHtmlContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HtmlTextBox htmlTextBox = d as HtmlTextBox;
            string htmlContent = (string)e.NewValue;
            htmlTextBox.HtmlContent = htmlContent;
        }

        private static void OnUrlForeColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HtmlTextBox htmlTextBox = d as HtmlTextBox;
            SolidColorBrush solidColorBrush = (SolidColorBrush)e.NewValue;
            htmlTextBox.UrlForeColor = solidColorBrush;
        }

        public HtmlTextBox()
        {
            this.DefaultStyleKey = typeof(HtmlTextBox);
            this.ContentParagraph = new Paragraph();
            this.HtmlText = new RichTextBox();
            if (this.UrlForeColor == default(SolidColorBrush))
            {
                this.UrlForeColor = new SolidColorBrush(Colors.Magenta);
            }
        }

        public RichTextBox HtmlText
        {
            get
            {
                return (RichTextBox)this.GetValue(HtmlTextProperty);
            }

            set
            {
                this.SetValue(HtmlTextProperty, value);
            }
        }

        public string HtmlContent
        {
            get
            {
                return (string)this.GetValue(HtmlContentProperty);
            }

            set
            {
                this.SetValue(HtmlContentProperty, value);
                this.SetContent();
            }
        }

        public SolidColorBrush UrlForeColor
        {
            get
            {
                return (SolidColorBrush)this.GetValue(UrlForeColorProperty);
            }

            set
            {
                this.SetValue(UrlForeColorProperty, value);
            }
        }

        private void SetContent()
        {
            this.ProcessWords();
            HtmlText.Blocks.Add(this.ContentParagraph);
            this.Content = this.HtmlText;
        }

        private void ProcessWords()
        {
            foreach (var word in this.HtmlContent.Split(' '))
            {
                if (word.StartsWith("http://") || word.StartsWith("https://"))
                    this.ContentParagraph.Inlines.Add(GetAsLink(word + " "));
                else
                    this.ContentParagraph.Inlines.Add(GetAsRun(word + " "));
            }
        }

        private Hyperlink GetAsLink(string tweetWord)
        {
            var hl = new Hyperlink
            {
                NavigateUri = new Uri(tweetWord),
                TargetName = "_blank",
                Foreground = this.UrlForeColor
            };

            hl.Inlines.Add(tweetWord);

            return hl;
        }

        private Run GetAsRun(string word)
        {
            if (word.StartsWith("#") || word.StartsWith("@"))
                return GetAsAccentedRun(word);

            return new Run { Text = word };
        }

        private Run GetAsAccentedRun(string word)
        {
            return new Run
            {
                Foreground = this.UrlForeColor,
                Text = word
            };
        }
    }
}
