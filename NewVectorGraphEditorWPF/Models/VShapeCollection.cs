using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    /// <summary>
    /// Класс, определяющий коллекцию фигур
    /// </summary>
    [Serializable]
    class VShapeCollection : IEnumerable, IEnumerator {

        #region IEnumarator and IEmunerable implementation
        private int position = -1;
        public object Current {
            get {
                if (position >= 0 && position < shapes.Count)
                    return shapes[position];
                throw new InvalidOperationException();
            }
        }
        
        public IEnumerator GetEnumerator() => shapes.GetEnumerator();

        public bool MoveNext() {
            if (position < shapes.Count - 1) {
                position++;
                return true;
            }
            return false;
        }

        public void Reset() => position = -1;
        #endregion

        #region fields
        protected List<VShape> shapes;              // Список фигур в коллекции
        #endregion

        #region Constructors
        public VShapeCollection() => this.shapes = new List<VShape>();
        #endregion

        #region Methods
        /// <summary>
        /// Добавляет новую фигуру в коллекцию
        /// </summary>
        /// <param name="shape"></param>
        public virtual void Add(VShape shape) => this.shapes.Add(shape);

        /// <summary>
        /// Удаляет фигуру из коллекции
        /// </summary>
        /// <param name="shape">Фигура, которую надо удалить</param>
        public virtual void Remove(VShape shape) => this.shapes.Remove(shape);

        /// <summary>
        /// Удаляет фигуру из коллекции
        /// </summary>
        /// <param name="i">Индекс удаляемой фигуры в списке фигур</param>
        public virtual void RemoveAt(int i) => this.shapes.RemoveAt(i);


        /// <summary>
        /// Меняет фигуры местами в коллекции
        /// </summary>
        /// <param name="i">Индекс первой фигуры в коллекции</param>
        /// <param name="j">Индекс второй фигуры в коллекции</param>
        public virtual void Exchange(int i, int j) => (this.shapes[i], this.shapes[j]) = (this.shapes[j], this.shapes[i]);

        /// <summary>
        /// Меняет фигуры местами в коллекции
        /// </summary>
        /// <param name="sh1">Фигура 1</param>
        /// <param name="sh2">Фигура 2</param>
        public virtual void Exchange(VShape sh1, VShape sh2) =>
            (this.shapes[this.shapes.IndexOf(sh1)], this.shapes[this.shapes.IndexOf(sh2)]) =
            (this.shapes[this.shapes.IndexOf(sh2)], this.shapes[this.shapes.IndexOf(sh1)]);

        /// <summary>
        /// Поднимает фигуру на один план вверх
        /// </summary>
        /// <param name="sh">Фигура, которую надо поднять</param>
        public virtual void LiftUp(VShape sh) {
            int i = this.shapes.IndexOf(sh);
            if (i < this.shapes.Count - 1) Exchange(i, i + 1);
        }

        /// <summary>
        /// Поднимает фигуру на один план вверх
        /// </summary>
        /// <param name="i">Индекс фигуры в коллекции, которую надо поднять</param>
        public virtual void LiftUp(int i) {
            if (i < this.shapes.Count - 1) Exchange(i, i + 1);
        }

        /// <summary>
        /// Опускает фигуру на один план вниз
        /// </summary>
        /// <param name="sh">Фигура, которую надо опустить</param>
        public virtual void LiftDown(VShape sh) {
            int i = this.shapes.IndexOf(sh);
            if (i > 0) Exchange(i, i - 1);
        }

        /// <summary>
        /// Опускает фигуру на один план вниз
        /// </summary>
        /// <param name="i">Индекс фигуры в коллекции, которую надо опустить</param>
        public virtual void LiftDown(int i) {
            if (i > 0) Exchange(i, i - 1);
        }


        /// <summary>
        /// Помещает фигуру на передний план
        /// </summary>
        /// <param name="i">Индекс поднимаемой фигуры в коллекции</param>
        public virtual void LiftStraightUp(int i) {
            if (i < this.shapes.Count - 1) {
                LiftUp(i);
                LiftStraightUp(i + 1);
            }
        }

        /// <summary>
        /// Помещает фигуру на задний план
        /// </summary>
        /// <param name="sh">Фигура, которую надо поместить на задний план</param>
        public virtual void LiftStraightUp(VShape sh) {
            int i = this.shapes.IndexOf(sh);
            if (i == -1) throw new InvalidOperationException();

            LiftStraightUp(i);
        }

        public virtual void LiftStraightDown(int i) {
            if (i > 0) {
                LiftDown(i);
                LiftStraightUp(i - 1);
            }
        }

        public virtual void LiftStraightDown(VShape sh) {
            int i = this.shapes.IndexOf(sh);
            if (i == -1) throw new InvalidOperationException();

            LiftStraightDown(i);
        }

        public int GetShapeIndex(VShape sh) => this.shapes.IndexOf(sh); 
        #endregion

    }
}
