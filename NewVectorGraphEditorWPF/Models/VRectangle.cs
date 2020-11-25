using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс, определяющий прямоугольник
    /// </summary>
    class VRectangle : VShape {
        #region Constructors
        public VRectangle(double width, double height) : base(width, height) { }
        public VRectangle(double width, double height, VColor fill, VColor stroke) :
            base(width, height, fill, stroke) { }
        public VRectangle(double width, double height, VColor fill, VColor stroke, int strokeThickness) :
            base(width, height, fill, stroke, strokeThickness) { }
        #endregion

        #region Methods
        public override double GetSquare() => this.width * this.height;
        #endregion
    }
}
