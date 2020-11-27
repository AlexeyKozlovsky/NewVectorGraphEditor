using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewVectorGraphEditorWPF.Models {
    class DrawCollection<T> : IEnumerable<T>, IEnumerator<T> {
        #region IEnumerator and IEnumerable implementation
        private int position = -1;
        public T Current {
            get {
                if (position >= 0 && position < objs.Count)
                    return objs[position];
                throw new InvalidOperationException();
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() => objs.Clear();

        public IEnumerator<T> GetEnumerator() => objs.GetEnumerator();

        public bool MoveNext() {
            if (position < 0 || position >= objs.Count - 1) return false;
            position++;
            return true;
        }

        public void Reset() => position = -1;

        IEnumerator IEnumerable.GetEnumerator() => objs.GetEnumerator();
        #endregion

        #region Fields
        private List<T> objs;
        #endregion

        #region Properties
        public T this[int index] { get => objs[index]; }
        #endregion

        #region Methods
        /// <summary>
        /// Добавляет новую фигуру в коллекцию
        /// </summary>
        /// <param name="shape"></param>
        public void Add(T shape) => this.objs.Add(shape);

        /// <summary>
        /// Удаляет фигуру из коллекции
        /// </summary>
        /// <param name="shape">Фигура, которую надо удалить</param>
        public void Remove(T shape) => this.objs.Remove(shape);

        /// <summary>
        /// Удаляет фигуру из коллекции
        /// </summary>
        /// <param name="i">Индекс удаляемой фигуры в списке фигур</param>
        public void RemoveAt(int i) => this.objs.RemoveAt(i);


        /// <summary>
        /// Меняет фигуры местами в коллекции
        /// </summary>
        /// <param name="i">Индекс первой фигуры в коллекции</param>
        /// <param name="j">Индекс второй фигуры в коллекции</param>
        public void Exchange(int i, int j) => (this.objs[i], this.objs[j]) = (this.objs[j], this.objs[i]);

        /// <summary>
        /// Меняет фигуры местами в коллекции
        /// </summary>
        /// <param name="sh1">Фигура 1</param>
        /// <param name="sh2">Фигура 2</param>
        public void Exchange(T sh1, T sh2) =>
            (this.objs[this.objs.IndexOf(sh1)], this.objs[this.objs.IndexOf(sh2)]) =
            (this.objs[this.objs.IndexOf(sh2)], this.objs[this.objs.IndexOf(sh1)]);

        /// <summary>
        /// Поднимает фигуру на один план вверх
        /// </summary>
        /// <param name="sh">Фигура, которую надо поднять</param>
        public void LiftUp(T sh) {
            int i = this.objs.IndexOf(sh);
            if (i < this.objs.Count - 1) Exchange(i, i + 1);
        }

        /// <summary>
        /// Поднимает фигуру на один план вверх
        /// </summary>
        /// <param name="i">Индекс фигуры в коллекции, которую надо поднять</param>
        public void LiftUp(int i) {
            if (i < this.objs.Count - 1) Exchange(i, i + 1);
        }

        /// <summary>
        /// Опускает фигуру на один план вниз
        /// </summary>
        /// <param name="sh">Фигура, которую надо опустить</param>
        public void LiftDown(T sh) {
            int i = this.objs.IndexOf(sh);
            if (i > 0) Exchange(i, i - 1);
        }

        /// <summary>
        /// Опускает фигуру на один план вниз
        /// </summary>
        /// <param name="i">Индекс фигуры в коллекции, которую надо опустить</param>
        public void LiftDown(int i) {
            if (i > 0) Exchange(i, i - 1);
        }


        /// <summary>
        /// Помещает фигуру на передний план
        /// </summary>
        /// <param name="i">Индекс поднимаемой фигуры в коллекции</param>
        public void LiftStraightUp(int i) {
            if (i < this.objs.Count - 1) {
                LiftUp(i);
                LiftStraightUp(i + 1);
            }
        }

        /// <summary>
        /// Помещает фигуру на задний план
        /// </summary>
        /// <param name="sh">Фигура, которую надо поместить на задний план</param>
        public void LiftStraightUp(T sh) {
            int i = this.objs.IndexOf(sh);
            if (i == -1) throw new InvalidOperationException();

            LiftStraightUp(i);
        }
        #endregion

    }
}
