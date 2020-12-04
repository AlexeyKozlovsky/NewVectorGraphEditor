using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс, определяющий треугольник
    /// </summary>
    [Serializable]
    class VTriangle : VShape {
        #region Constructors
        public VTriangle(double width, double height) : base(width, height) { }
        public VTriangle(double width, double height, VColor fill, VColor stroke) :
            base(width, height, fill, stroke) { }
        public VTriangle(double width, double height, VColor fill, VColor stroke, int strokeThickness) :
            base(width, height, fill, stroke, strokeThickness) { }
        #endregion

        #region Methods
        public override double GetSquare() => 0.5 * width * height;
        public override bool IsIn(double x, double y) =>
            true;
        #endregion
    }
}
