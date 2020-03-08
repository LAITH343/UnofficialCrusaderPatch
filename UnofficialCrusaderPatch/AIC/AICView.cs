﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UCP.AIC
{
    public class AICView
    {
        public void InitUI(Grid grid, RoutedPropertyChangedEventHandler<object> SelectionDisabler)
        {
            TreeView view = new TreeView()
            {
                Background = null,
                BorderThickness = new Thickness(0, 0, 0, 0),
                Focusable = false,
                Name = "AICView"
            };
            view.SelectedItemChanged += SelectionDisabler;

            foreach (AICChange change in AICChange.changes)
            {
                change.InitUI();
                view.Items.Add(change.UIElement);
            }
            grid.Children.Add(view);
            Button button = new Button
            {
                ToolTip = "Reload .aics",
                Width = 20,
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 20, 5),
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/UnofficialCrusaderPatchGUI;component/Graphics/refresh.png")),
                }
            };
            grid.Children.Add(button);
            button.Click += (s, e) => Refresh(s, e, view);
            //grid.Children.Add(button);
        }

        private void Refresh(object s, RoutedEventArgs e, TreeView view)
        {
            AICChange.Refresh(s, e);
            Version.Changes.AddRange(AICChange.changes);
            foreach (AICChange change in AICChange.changes)
            {
                change.InitUI();
                view.Items.Add(change.UIElement);
            }
        }
    }
}
