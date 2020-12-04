using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс, определяющий эллипс
    /// </summary>
    [Serializable]
    class VEllipse : VShape {
        #region Constructors
        public VEllipse(double width, double height) : base(width, height) { }
        public VEllipse(double width, double height, VColor fill, VColor stroke) :
            base(width, height, fill, stroke) { }
        public VEllipse(double width, double height, VColor fill, VColor stroke, int strokeThickness) :
            base(width, height, fill, stroke, strokeThickness) { }
        #endregion

        #region Methods
        public override double GetSquare() => 0.25 * Math.PI * width * height;
        public override bool IsIn(double x, double y) =>
            x / Math.Pow(this.PositionX + this.Width / 2, 2) + y / Math.Pow(this.PositionY + this.Height / 2, 2) <= 1;
        #endregion
    }
}
