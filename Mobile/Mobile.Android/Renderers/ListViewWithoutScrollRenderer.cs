using Android.Views;
using Mobile.Controls;
using Mobile.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListViewWithoutScroll), typeof(ListViewWithoutScrollRenderer))]
namespace Mobile.Droid.Renderers
{
    public class ListViewWithoutScrollRenderer : ListViewRenderer
    {
        private int _mPosition;

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.Down)
            {
                // Record the position the list the touch landed on
                _mPosition = this.Control.PointToPosition((int)e.GetX(), (int)e.GetY());
                return base.DispatchTouchEvent(e);
            }

            if (e.ActionMasked == MotionEventActions.Move)
            {
                // Ignore move eents
                return true;
            }

            if (e.ActionMasked == MotionEventActions.Up)
            {
                // Check if we are still within the same view
                if (this.Control.PointToPosition((int)e.GetX(), (int)e.GetY()) == _mPosition)
                {
                    base.DispatchTouchEvent(e);
                }
                else
                {
                    // Clear pressed state, cancel the action
                    Pressed = false;
                    Invalidate();
                    return true;
                }
            }

            return base.DispatchTouchEvent(e);
        }
    }

}