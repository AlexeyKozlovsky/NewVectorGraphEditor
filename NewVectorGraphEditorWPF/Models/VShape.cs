using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс определяющий векторную фигуру
    /// </summary>
    abstract class VShape {

        #region Fields
        protected VField field;             // Поле, на котором размещена фигура
        protected double positionX;         // Позиция по OX на поле, где размещена фигура
        protected double positionY;         // Позиция по OY на поле, где размещена фигура
        protected double width;             // Ширина фигуры
        protected double height;            // Высота фигуры
        protected VColor fill;              // Цвет заливки фигуры
        protected VColor stroke;            // Цвет границы фигуры
        protected int strokeThickness;      // Толщина границы фигуры

        protected string name;              // Название фигуры
        #endregion

        #region Properties
        public VField Field {
            get => field;  set {
                if (field == null) return;
                field = value;
                field.Add(this);
                this.positionX = 0;
                this.positionY = 0;
            }
        }
        
        public double PositionX {
            get => positionX;
        }
        
        public double PositionY {
            get => positionY;
        }

        public VColor Fill { get => fill; }
        public VColor Stroke { get => stroke; }
        public int StrokeThickness {
            get => strokeThickness;
            set => strokeThickness = value;
        }
        #endregion

        #region Constructors
        // Изначально позиция фигуры не указывается, так как без нахождения на конкретном поле фигура не может иметь позиции
        public VShape(double width, double height) {
            this.width = width;
            this.height = height;
            this.strokeThickness = 2;
            this.stroke = VColor.BLACK;
            this.fill = VColor.WHITE;
        }

        public VShape(double width, double height, VColor fill, VColor stroke) {
            this.width = width;
            this.height = height;
            this.fill = fill;
            this.stroke = stroke;
            this.strokeThickness = 2;
        }

        public VShape(double width, double height, VColor fill, VColor stroke, int strokeThickness) {
            this.width = width;
            this.height = height;
            this.fill = fill;
            this.stroke = stroke;
            this.strokeThickness = strokeThickness;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Считает площадь фигуры, требует переопределения в классах наследниках
        /// </summary>
        /// <returns>Возвращает площать</returns>
        public virtual double GetSquare() => 0;

        /// <summary>
        /// Определяет находится ли точка внутри фигуры, требует переопределения в классах наследниках
        /// </summary>
        /// <param name="x">Положение по оси OX верхнего левого угла, прямоугольника описанного вокруг фигуры на поле</param>
        /// <param name="y">Положение по оси OY верхнего левого угла, прямоугольника описанного вокруг фигуры на поле</param>
        /// <returns>Возвращает true, если точка лежит внутри фигуры, в противном случае false</returns>
        public virtual bool IsIn(double x, double y) => false;

        /// <summary>
        /// Меняет цвет заливки фигуры
        /// </summary>
        /// <param name="fill">Новый цвет заливки</param>
        public void ChangeFillColor(VColor fill) => this.fill = fill;

        /// <summary>
        /// Меняет цвет границы фигуры
        /// </summary>
        /// <param name="stroke">Новый цвет границы</param>
        public void ChangeBorderColor(VColor stroke) => this.stroke = stroke;

        /// <summary>
        /// Меняет размер фигуры
        /// </summary>
        /// <param name="width">Новая ширина</param>
        /// <param name="height">Новая высота</param>
        public void ChangeSize(double width, double height) {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Меняет толщину границ фигуры
        /// </summary>
        /// <param name="strokeThickness">Новая толщина границ фигуры</param>
        public void ChangeStrokeThickness(int strokeThickness) {
            if (strokeThickness > 0) this.strokeThickness = strokeThickness;
        }

        public void SetPosition(double x, double y) {
            positionX = x;
            positionY = y;
        }

        public void SetName(string name) => this.name = name;
        #endregion

    }
}
