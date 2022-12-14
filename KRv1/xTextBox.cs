using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KRv1;

[SuppressMessage("ReSharper", "CommentTypo")]
[SuppressMessage("ReSharper", "IdentifierTypo")]
// ReSharper disable once InconsistentNaming
public class xTextBox : TextBox
{
    public xTextBox() //Самописный TextBox с подсказкой ввода
    {
        Loaded += delegate
        {
            var placetext = new TextBlock() { Foreground = SystemColors.GrayTextBrush };
            var bind = new Binding( "PlaceHolder" );
            bind.Source = this;
            placetext.SetBinding( TextBlock.TextProperty, bind );
 
            var host = Template.FindName( "PART_ContentHost", this ) as ContentControl;
            var tbw = host.Content as FrameworkElement;
 
            var grid = new Grid();
            host.Content = grid;
            grid.Children.Add( placetext );
            grid.Children.Add( tbw );
 
            this.TextChanged += delegate
            {
                placetext.Opacity = string.IsNullOrWhiteSpace( Text ) ? 1 : 0;
            };
        };
    }
    public string PlaceHolder
    {
        get { return (string) GetValue( PlaceHolderProperty ); }
        set { SetValue( PlaceHolderProperty, value ); }
    }
    public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register( "PlaceHolder", typeof( string ), typeof( xTextBox ), new PropertyMetadata( null ) );
}