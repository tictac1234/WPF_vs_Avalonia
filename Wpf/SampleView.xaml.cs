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

namespace Wpf
{
    /// <summary>
    /// SampleView.xaml の相互作用ロジック
    /// </summary>
    public partial class SampleView : UserControl
    {

        Rect _rect;
        Point _rectPoint;
        Point _clickPt;

        bool _isHit = false;


        public SampleView()
        {
            InitializeComponent();

            ClipToBounds = true;
            _rect = new Rect(0, 0, 64, 64);
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            _clickPt = e.GetPosition(this);
            _rectPoint = new Point(_rect.X, _rect.Y);

            if (_rect.Contains(_clickPt))
            {
                _isHit = true;
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isHit)
            {
                var pos = e.GetPosition(this);
                var add = pos - _clickPt;
                _rect = new Rect(_rectPoint.X + add.X, _rectPoint.Y + add.Y, _rect.Width, _rect.Height);
                this.InvalidateVisual();
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            _isHit = false;
        }


        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);

            var brush = new SolidColorBrush(Colors.Blue);
            dc.DrawRectangle(brush, null, _rect);
        }

        
    }
}
