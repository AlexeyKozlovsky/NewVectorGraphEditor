using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс, определяющий прямоугольник
    /// </summary>
    [Serializable]
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
        public override bool IsIn(double x, double y) => 
            x >= this.PositionX && x <= this.PositionX + this.Width && y >= this.PositionY && y <= this.PositionY + this.Height;
        #endregion
    }
}
