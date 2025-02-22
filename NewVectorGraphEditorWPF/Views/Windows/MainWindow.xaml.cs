﻿using NewVectorGraphEditorWPF.Models;
using NewVectorGraphEditorWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NewVectorGraphEditorWPF {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        #region Fields
        private MainWindowVM vm;
        private DrawCollection<Shape> shapes;
        private int selectedShapeIndex;
        private bool isPressed;

        
        #endregion

        #region Properties
        private DrawCollection<Shape> Shapes {
            get => shapes; set {
                shapes = value;
                OnPropertyChanged("Shapes");
            }
        }
        #endregion

        public MainWindow() {
            InitializeComponent();
            vm = (MainWindowVM)DataContext;
            shapes = new DrawCollection<Shape>();
            selectedShapeIndex = -1;
            vm.Field.VField = new VField(canvas.Width, canvas.Height);
            propGrid.Visibility = Visibility.Hidden;
        }

        #region Methods
        #region Update Methods
        private void UpdateTrianglePoints() {
            if (shapes[selectedShapeIndex] is Polygon) {
                Polygon shape = (Polygon)shapes[selectedShapeIndex];
                shape.Points = new PointCollection() {
                    new Point(shape.Width/2, 0),
                    new Point(0, shape.Height),
                    new Point(shape.Width, shape.Height)
                };
            }
        }

        private void UpdatePropGrid() {
            propGrid.Visibility = Visibility.Visible;
            selectedShapeIndex = vm.Field.VField.GetSelectedShapeIndex();
            currentElementNameTextBox.Text = vm.Field.VField.NamesStrings[vm.Field.VField.GetSelectedShapeIndex()];
            currentElementHeightTextBox.Text = vm.Field.VField[selectedShapeIndex].Height.ToString();
            currentElementWidthTextBox.Text = vm.Field.VField[selectedShapeIndex].Width.ToString();
            currentElementThicknessTextBox.Text = vm.Field.VField[selectedShapeIndex].StrokeThickness.ToString();
        }

        private void UpdateElementsList() {
            int i = 0;
            elementsListBox.Items.Clear();
            foreach (string name in vm.Field.VField.NamesStrings) {
                elementsListBox.Items.Add(name);
                i++;
            }
        }

        private void UpdateSelectedShapeWithPropGrid() {
            double newWidth = double.Parse(currentElementWidthTextBox.Text);
            double newHeight = double.Parse(currentElementHeightTextBox.Text);
            int newThickness = int.Parse(currentElementThicknessTextBox.Text);

            if (newWidth <= 0 || newHeight <= 0 || newThickness <= 0) {
                System.Windows.MessageBox.Show("Характеристики не могут быть отрицательными");
                UpdatePropGrid();
                return;
            }

            VShape shape = vm.Field.VField.GetSelectedShape();
            shape.ChangeSize(newWidth, newHeight);
            shape.ChangeStrokeThickness(newThickness);
            shapes[selectedShapeIndex].Width = newWidth;
            shapes[selectedShapeIndex].Height = newHeight;
        }

        private void UpdateSelectedWidth() {
            double newWidth = 0;
            try { newWidth = double.Parse(currentElementWidthTextBox.Text); } catch {
                System.Windows.MessageBox.Show("Ширина введена некорректно");
                currentElementWidthTextBox.Text = vm.Field.VField.GetSelectedShape().Width.ToString();
                return;
            }

            if (newWidth <= 0) {
                System.Windows.MessageBox.Show("Ширина введена некорректно");
                currentElementWidthTextBox.Text = vm.Field.VField.GetSelectedShape().Width.ToString();
                return;
            }

            VShape shape = vm.Field.VField.GetSelectedShape();
            shape.ChangeSize(newWidth, shape.Height);
            shapes[selectedShapeIndex].Width = newWidth;

            if (shape is VTriangle) UpdateTrianglePoints();
        }

        private void UpdateSelectedHeight() {
            double newHeight = 0;
            try { newHeight = double.Parse(currentElementHeightTextBox.Text); } catch {
                System.Windows.MessageBox.Show("Высота введена некорректно");
                currentElementHeightTextBox.Text = vm.Field.VField.GetSelectedShape().Height.ToString();
                return;
            }

            if (newHeight <= 0) {
                System.Windows.MessageBox.Show("Высота не может быть отрицательна");
                currentElementHeightTextBox.Text = vm.Field.VField.GetSelectedShape().Height.ToString();
                return;
            }

            VShape shape = vm.Field.VField.GetSelectedShape();
            shape.ChangeSize(shape.Width, newHeight);
            shapes[selectedShapeIndex].Height = newHeight;
            if (shape is VTriangle) UpdateTrianglePoints();
        }

        private void UpdateSelectedThickness() {
            int newThickness = 0;
            try { newThickness = int.Parse(currentElementThicknessTextBox.Text); } catch {
                System.Windows.MessageBox.Show("Толщина введена некорректно");
                currentElementThicknessTextBox.Text = vm.Field.VField.GetSelectedShape().StrokeThickness.ToString();
                return;
            }

            if (newThickness <= 0) {
                System.Windows.MessageBox.Show("Толщина не может быть отрицательна");
                currentElementThicknessTextBox.Text = vm.Field.VField.GetSelectedShape().StrokeThickness.ToString();
                return;
            }

            VShape shape = vm.Field.VField.GetSelectedShape();
            shape.ChangeStrokeThickness(newThickness);
            shapes[selectedShapeIndex].StrokeThickness = newThickness;
        }

        private void UpdateShapes() {
            canvas.Children.Clear();
            shapes = new DrawCollection<Shape>();
            int i = 0;
            foreach (VShape shape in vm.Field.VField) {
                Shape sh = new Ellipse();
                if (shape is VEllipse) {
                    sh = new Ellipse();
                } else if (shape is VRectangle) {
                    sh = new Rectangle();
                } else if (shape is VTriangle) {
                    sh = new Polygon();
                    ((Polygon)sh).Points = new PointCollection() {
                        new Point(0, shape.Height),
                        new Point(shape.Width, shape.Height),
                        new Point(shape.Width/2, 0)
                    };
                }

                sh.Width = shape.Width;
                sh.Height = shape.Height;
                sh.Fill = new SolidColorBrush(Color.FromArgb(shape.Fill.Alpha, shape.Fill.Red, shape.Fill.Green, shape.Fill.Blue));
                sh.Stroke = new SolidColorBrush(Color.FromArgb(shape.Stroke.Alpha, shape.Stroke.Red, shape.Stroke.Green, shape.Stroke.Blue));
                sh.StrokeThickness = shape.StrokeThickness;

                
                Canvas.SetLeft(sh, shape.PositionX);
                Canvas.SetTop(sh, shape.PositionY);
                canvas.Children.Add(sh);
                shapes.Add(sh);
            }
        }
        #endregion
        #region Drawing Methods

        /// <summary>
        /// Создает на векторном поле эллипс и рисует его на канвасе
        /// </summary>
        /// <param name="x">Координата верхнего левого угла описанного прямоугольника по оси OX</param>
        /// <param name="y">Координата верхнего левого угла описанного прямоугольника по оси OY</param>
        private void DrawEllipse(double x, double y) {
            VEllipse vEl = new VEllipse(0, 0);
            vm.Field.VField.Add(vEl);
            vEl.SetPosition(x, y);
            vEl.ChangeBorderColor(VColor.BLACK);
            vEl.ChangeFillColor(VColor.WHITE);
            

            Shape el = new Ellipse();
            el.Width = 0;
            el.Height = 0;
            el.Fill = new SolidColorBrush(Color.FromArgb(vEl.Fill.Alpha, vEl.Fill.Red, vEl.Fill.Green, vEl.Fill.Blue));
            el.Stroke = new SolidColorBrush(Color.FromArgb(vEl.Stroke.Alpha, vEl.Stroke.Red, vEl.Stroke.Green, vEl.Stroke.Blue));
            //el.Fill = Brushes.Red;
            //el.Stroke = Brushes.Black;
            el.StrokeThickness = vEl.StrokeThickness;
            Canvas.SetLeft(el, x);
            Canvas.SetTop(el, y);

            canvas.Children.Add(el);
            shapes.Add(el);
            selectedShapeIndex = shapes.Count - 1;
            elementsListBox.Items.Add(vm.Field.VField.NamesStrings[vm.Field.VField.NamesStrings.Count - 1]);
            propGrid.Visibility = Visibility.Visible;
            UpdatePropGrid();
        }

        /// <summary>
        /// Создает на векторном поле прямоугольник и рисует его на канвасе
        /// </summary>
        private void DrawRectangle(double x, double y) {
            VRectangle vRect = new VRectangle(0, 0);
            vm.Field.VField.Add(vRect);
            vRect.SetPosition(x, y);
            vRect.ChangeBorderColor(VColor.BLACK);
            vRect.ChangeFillColor(VColor.WHITE);

            Shape rect = new Rectangle();
            rect.Width = 0;
            rect.Height = 0;
            rect.Fill = new SolidColorBrush(Color.FromArgb(vRect.Fill.Alpha, vRect.Fill.Red, vRect.Fill.Green, vRect.Fill.Blue));
            rect.Stroke = new SolidColorBrush(Color.FromArgb(vRect.Stroke.Alpha, vRect.Stroke.Red, vRect.Stroke.Green, vRect.Stroke.Blue));
            rect.StrokeThickness = vRect.StrokeThickness;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);

            canvas.Children.Add(rect);
            shapes.Add(rect);
            selectedShapeIndex = shapes.Count - 1;
            elementsListBox.Items.Add(vm.Field.VField.NamesStrings[vm.Field.VField.NamesStrings.Count - 1]);
            propGrid.Visibility = Visibility.Visible;
            UpdatePropGrid();
        }

        /// <summary>
        /// Создает на векторном поле треугольник и рисует его на канвасе
        /// </summary>
        private void DrawTriangle(double x, double y) {
            VTriangle vTr = new VTriangle(0, 0);
            vm.Field.VField.Add(vTr);
            vTr.SetPosition(x, y);
            vTr.ChangeFillColor(VColor.WHITE);
            vTr.ChangeBorderColor(VColor.BLACK);

            Polygon tr = new Polygon();
            tr.Width = 0;
            tr.Height = 0;
            tr.Fill = new SolidColorBrush(Color.FromArgb(vTr.Fill.Alpha, vTr.Fill.Red, vTr.Fill.Green, vTr.Fill.Blue));
            tr.Stroke = new SolidColorBrush(Color.FromArgb(vTr.Stroke.Alpha, vTr.Stroke.Red, vTr.Stroke.Green, vTr.Stroke.Blue));
            Canvas.SetLeft(tr, x);
            Canvas.SetTop(tr, y);
            canvas.Children.Add(tr);
            shapes.Add(tr);
            selectedShapeIndex = shapes.Count - 1;
            elementsListBox.Items.Add(vm.Field.VField.NamesStrings[vm.Field.VField.NamesStrings.Count - 1]);
            propGrid.Visibility = Visibility.Visible;
            UpdatePropGrid();
        }

        private void UpdateTriangle(double x, double y) {
            int selectedShapeIndex = vm.Field.VField.GetSelectedShapeIndex();
            if (!(vm.Field.VField.GetSelectedShape() is VTriangle)) return;
            if (selectedShapeIndex == -1) return;

            double selectedPositionX = vm.Field.VField.GetSelectedShape().PositionX;
            double selectedPositionY = vm.Field.VField.GetSelectedShape().PositionY;
            double minX = Math.Min(x, selectedPositionX);
            double minY = Math.Min(y, selectedPositionY);
            double width = Math.Abs(x - selectedPositionX);
            double height = Math.Abs(y - selectedPositionY);


            Polygon tr = (Polygon)shapes[selectedShapeIndex];
            tr.Width = width;
            tr.Height = height;
            tr.Points = new PointCollection() {
                new Point(0, height),
                new Point(width, height),
                new Point(width/2, 0)
            };
            
            Canvas.SetLeft(tr, minX);
            Canvas.SetTop(tr, minY);
            vm.Field.VField.GetSelectedShape().ChangeSize(width, height);

            //vm.Field.VField.GetSelectedShape().SetPosition(minX, minY);

        }
        /// <summary>
        /// Обновляет отрисовку текущей (выделенной) фигуры. Должен выполняться в фоновом потоке!
        /// </summary>
        private void UpdateSelectedShape() {
            if (vm.Mode == EditMode.SelectMode && this.isPressed) {
                Point position = Mouse.GetPosition(canvas);
                this.selectedShapeIndex = vm.Field.VField.GetClickShape(position.X, position.Y);
                if (this.selectedShapeIndex == -1) return;

                VShape currentVShape = this.vm.Field.VField[this.selectedShapeIndex];
                currentVShape.SetPosition(position.X - currentVShape.Width / 2, position.Y - currentVShape.Height / 2);
                Shape currentShape = this.shapes[this.selectedShapeIndex];
                Console.WriteLine($"{currentVShape.PositionX} {currentVShape.PositionY}");
                Canvas.SetLeft(currentShape, currentVShape.PositionX);
                Canvas.SetTop(currentShape, currentVShape.PositionY);
                return;
            }

            if (!isPressed) return;
            double x = Mouse.GetPosition(canvas).X;
            double y = Mouse.GetPosition(canvas).Y;

            if (vm.Mode == EditMode.DrawTrMode) {
                UpdateTriangle(x, y);
                return;
            }

            int selectedShapeIndex = vm.Field.VField.GetSelectedShapeIndex();
            if (selectedShapeIndex == -1) return;

            double selectedPosX = vm.Field.VField.GetSelectedShape().PositionX;
            double selectedPosY = vm.Field.VField.GetSelectedShape().PositionY;


            Shape shape = shapes[selectedShapeIndex];
            Canvas.SetLeft(shape, Math.Min(selectedPosX, x));
            Canvas.SetTop(shape, Math.Min(selectedPosY, y));
            shape.Width = Math.Abs(selectedPosX - x);
            shape.Height = Math.Abs(selectedPosY - y);
            vm.Field.VField.GetSelectedShape().ChangeSize(shape.Width, shape.Height);
            //vm.Field.VField.GetSelectedShape().SetPosition(Math.Min(selectedPosX, x), Math.Min(selectedPosY, y));
            Console.WriteLine("working...");
            Console.WriteLine(shape.Width);
        }
        #endregion
        /// <summary>
        /// Метод для события нажатия левой кнопки мыши по канвасу
        /// </summary>
        private void CanvasMouseLeftDown() {
            
            this.isPressed = true;
            Point position = Mouse.GetPosition(canvas);

            switch (vm.Mode) {
                case EditMode.DrawElMode:
                    DrawEllipse(position.X, position.Y);
                    return;
                case EditMode.DrawRectMode:
                    DrawRectangle(position.X, position.Y);
                    return;
                case EditMode.DrawTrMode:
                    DrawTriangle(position.X, position.Y);
                    return;
            }

        }

        private void Select() {

        }

        /// <summary>
        /// Метод для события CanvasMouseMove
        /// </summary>
        private void MouseMove() {
            if (selectedShapeIndex == -1) return;
            try {
                shapes[selectedShapeIndex]?.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => {
                    UpdateSelectedShape();
                }));
            } catch {
                return;
            }
        }

        /// <summary>
        /// Метод для события отпускания левой кнопки мыши по канвасу
        /// </summary>
        private void CanvasMouseLeftUp() {
            this.isPressed = false;
            UpdatePropGrid();
        }

        /// <summary>
        /// Меняет цвет заливки внутренности фигуры выбранной на поле
        /// </summary>
        private void Fill() {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                vm.Field.VField.GetSelectedShape().ChangeFillColor(new VColor {
                    Alpha = cd.Color.A,
                    Red = cd.Color.R,
                    Green = cd.Color.G,
                    Blue = cd.Color.B
                });
            }

            VColor vFillColor = vm.Field.VField.GetSelectedShape().Fill;
            shapes[selectedShapeIndex].Fill = new SolidColorBrush(Color.FromArgb(vFillColor.Alpha, vFillColor.Red, vFillColor.Green, vFillColor.Blue));
        }

        /// <summary>
        /// Меняет цвет границ фигуры, выбранной на поле
        /// </summary>
        private void Border() {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                vm.Field.VField.GetSelectedShape().ChangeBorderColor(new VColor {
                    Alpha = cd.Color.A,
                    Red = cd.Color.R,
                    Green = cd.Color.G,
                    Blue = cd.Color.B
                });
            }

            VColor vStrokeColor = vm.Field.VField.GetSelectedShape().Stroke;
            shapes[selectedShapeIndex].Stroke = new SolidColorBrush(Color.FromArgb(vStrokeColor.Alpha, vStrokeColor.Red, vStrokeColor.Green, vStrokeColor.Blue));
        }

        /// <summary>
        /// Меняет режим на режим "Выбора"
        /// </summary>
        private void SetSelectMode() => vm.Mode = EditMode.SelectMode;

        /// <summary>
        /// Меняет режим на режим "Рисование эллипса
        /// </summary>
        private void SetDrawElMode() => vm.Mode = EditMode.DrawElMode;

        /// <summary>
        /// Меняет режим на режим "Рисование прямоугольника"
        /// </summary>
        private void SetDrawRectMode() => vm.Mode = EditMode.DrawRectMode;

        /// <summary>
        /// Меняет режим на режим "Риования треугольника
        /// </summary>
        private void SetDrawTrMode() => vm.Mode = EditMode.DrawTrMode;

        private void LiftUp() {
            
        }

        private void LiftStraightUp() {

        }

        private void LiftDown() {

        }

        private void LiftStraightDown() {

        }

        private void SelectionChanged() {
            if (elementsListBox.Items?.Count == 0) return;
            selectedShapeIndex = elementsListBox.SelectedIndex;
            vm.Field.VField.SetSelectedShape(selectedShapeIndex);
            UpdatePropGrid();
        }

        private void Sort() {
            vm.Field.VField.SetSize(canvas.ActualWidth, canvas.ActualHeight);
            vm.Field.VField.Sort();
            UpdateShapes();
            UpdateElementsList();
        }

        private void SaveFile() {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate)) {
                    formatter.Serialize(fs, vm.Field.VField);
                }
            }
        }

        private void OpenFile() {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate)) {
                    vm.Field.VField = (VField)formatter.Deserialize(fs);
                }
            }

            vm.Field.VField.SetSelectedShape(0);
            UpdateShapes();
            UpdateElementsList();
            UpdatePropGrid();
        }
        #endregion

        #region Events
        /// <summary>
        /// Событие нажатия на кнопку заливки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fillBtn_Click(object sender, RoutedEventArgs e) => Fill();

        /// <summary>
        /// Событие нажатия на кнопку окраски границ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void borderBtn_Click(object sender, RoutedEventArgs e) => Border();

        /// <summary>
        /// Событие нажатия на кнопку режима выделения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectModeButton_Click(object sender, RoutedEventArgs e) => SetSelectMode();

        /// <summary>
        /// Событие нажатия на кнопку режима эллипса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawElModeButton_Click(object sender, RoutedEventArgs e) => SetDrawElMode();

        /// <summary>
        /// Событие нажатия на кнопку режима прямоугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawRectModeButton_Click(object sender, RoutedEventArgs e) => SetDrawRectMode();


        /// <summary>
        /// Событие нажатия на кпоку режима треугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawTrModeButton_Click(object sender, RoutedEventArgs e) => SetDrawTrMode();


        /// <summary>
        /// Событие нажатия на кнопку "Открыть файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, RoutedEventArgs e) => OpenFile();

        /// <summary>
        /// Событие нажатия на кнопку "Сохранить файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e) => SaveFile();

        /// <summary>
        /// Событие нажатия на кнопку "На передний план"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void straightUpButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "Поместить выше"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "Поместить ниже"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на кнопку "На задний план"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void straightDownButton_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// Событие нажатия на левую кнопку мыши на канвасе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => CanvasMouseLeftDown();

        /// <summary>
        /// Событие отпускания левой кнопки мыши на канвасе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => CanvasMouseLeftUp();
        private void canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) => MouseMove();

        private void elementsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged();

        private void currentElementWidthTextBox_LostFocus(object sender, RoutedEventArgs e) => UpdateSelectedWidth();

        private void currentElementHeightTextBox_LostFocus(object sender, RoutedEventArgs e) => UpdateSelectedHeight();

        private void currentElementThicknessTextBox_LostFocus(object sender, RoutedEventArgs e) => UpdateSelectedThickness();
        private void sortButton_Click(object sender, RoutedEventArgs e) => Sort();

        #endregion


    }
}
