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
    public class CS2dGraph : Control
    {
        public CS2dGraph()
        {

        }

        public CS2dBase CS { get; set; }


        // HitTestCore Remarks MSDN
        // 可以通过重写 HitTestCore 方法来重写对视觉对象的默认命中测试支持。 这意味着，在调用 HitTest 方法时，将调用 HitTestCore 的重写实现。 
        // 当命中测试位于可视对象的边框内时，将调用重写的方法，即使该坐标在视觉对象的几何图形之外也是如此。
        //
        // 重写位置命中测试方法 HitTestCore
        // 使鼠标位置在按钮范围内即可命中触发
        //
        // Override default hit test support in visual object
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            Point pt = hitTestParameters.HitPoint;

            // Perform custom actions during the hit test processing,
            // which may include verifying that the point actually
            // falls within the rendered content of the visual.

            // Return hit on bounding rectangle of visual object.
            return new PointHitTestResult(this, pt);
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (CS != null) CS.GraphRender(drawingContext);

        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            if (CS != null) CS.GraphMouseWheel(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (CS != null) CS.GraphMouseMove(e);

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (CS != null) CS.GraphMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (CS != null) CS.GraphMouseUp(e);
        }



    }
}
