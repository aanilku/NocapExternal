using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxControlToolkit.HTMLEditor;

namespace MyControls
{
    /// <summary>
    /// Summary description for CustomEditor
    /// </summary>
    public class CustomEditor : Editor
    {
        public CustomEditor()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        protected override void FillBottomToolbar()
        {
            //  BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
            //  BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
        }
    }
}