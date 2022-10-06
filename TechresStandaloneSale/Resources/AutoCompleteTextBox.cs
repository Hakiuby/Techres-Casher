using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace TechresStandaloneSale.Resources
{
    /// <summary>
    /// Class AutoCompleteTextBox.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.TextBox" />
    internal class AutoCompleteTextBox : TextBox
    {
        #region Properties

        /// <summary>
        /// Gets the popup.
        /// </summary>
        /// <value>The popup.</value>
        Popup Popup => this.Template.FindName("PART_Popup", this) as Popup;

        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <value>The item list.</value>
        ListBox ItemList => this.Template.FindName("PART_ItemList", this) as ListBox;

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IList<object> ItemsSource { get; set; }

        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceproperty = DependencyProperty.Register(
                                                    "ItemsSource", typeof(object), typeof(AutoCompleteTextBox));

        /// <summary>
        /// Gets or sets the container item style.
        /// </summary>
        /// <value>The container item style.</value>
        public Style ItemContainerStyle { set; get; }

        /// <summary>
        /// The container item style property
        /// </summary>
        public static readonly DependencyProperty ContainerItemStyleproperty = DependencyProperty.Register(
                                                    "ItemContainerStyle", typeof(Style), typeof(AutoCompleteTextBox));

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public Func<object, string, bool> Filter { get; set; }
        /// <summary>
        /// The container item style property
        /// </summary>
        public static readonly DependencyProperty Filterproperty = DependencyProperty.Register(
                                                    "Filter", typeof(Predicate<object>), typeof(AutoCompleteTextBox));
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteTextBox"/> class.
        /// </summary>
        /// <summary>
        /// Called when [apply template].
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ItemList != null && this.Popup != null)
            {
                this.KeyDown += new KeyEventHandler(this.AutoCompleteTextBox_KeyDown);
                this.PreviewKeyDown += new KeyEventHandler(this.AutoCompleteTextBox_PreviewKeyDown);
                this.ItemList.PreviewMouseDown += new MouseButtonEventHandler(this.ItemList_PreviewMouseDown);
                this.ItemList.KeyDown += new KeyEventHandler(this.ItemList_KeyDown);

                this.ItemList.Items.Clear();

                // Create binding for item collection
                Binding binding = new Binding { Path = new PropertyPath("ItemsSource"), Source = this };
                BindingOperations.SetBinding(this.ItemList, ItemsControl.ItemsSourceProperty, binding);

                // Create binding for container item style
                Binding styleBinding = new Binding { Path = new PropertyPath("ItemContainerStyle"), Source = this };
                BindingOperations.SetBinding(this.ItemList, ItemsControl.ItemContainerStyleProperty, styleBinding);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the AutoCompleteTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Popup.IsOpen = false;
                this.UpdateSource();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ItemList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void ItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {
                var tb = e.OriginalSource as ListBoxItem;
                this.Text = (tb.Content as string);
                if (e.Key == Key.Enter)
                {
                    this.Popup.IsOpen = false;
                    this.UpdateSource();
                }

            }
        }

        /// <summary>
        /// Updates the source.
        /// </summary>
        void UpdateSource()
        {
            if (this.GetBindingExpression(TextBox.TextProperty) != null)
            {
                this.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        /// <summary>
        /// Handles the PreviewMouseDown event of the ItemList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        void ItemList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var tb = e.OriginalSource as TextBlock;
                if (tb != null)
                {
                    this.Text = tb.Text;
                    this.UpdateSource();
                    this.Popup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the AutoCompleteTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void AutoCompleteTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && this.ItemList.Items.Count > 0 && !(e.OriginalSource is ListBoxItem))
            {
                this.ItemList.Focus();
                this.ItemList.SelectedIndex = 0;
                var lbi = ItemList.ItemContainerGenerator.ContainerFromIndex(this.ItemList.SelectedIndex) as ListBoxItem;
                lbi?.Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (this.Popup != null && this.ItemList != null && this.ItemList.ItemsSource != null)
            {
                try
                {
                    // Refreshes the view or specifies that the view needs to be refreshed when the defer cycle completes.
                    if (this.Filter != null)
                    {
                        this.ItemList.Items.Filter = member => this.Filter(member, this.Text);
                    }
                    else
                    {
                        // The below is default filter
                        this.ItemList.Items.Filter = member =>
                        {
                            string str = member.ToString();
                            return str.StartsWith(this.Text, StringComparison.CurrentCultureIgnoreCase) &&
                                   !(string.Equals(str, this.Text, StringComparison.CurrentCultureIgnoreCase));
                        };
                    }

                    if (this.ItemList.Items.Count > 0)
                    {
                        // Show popup
                        this.Popup.IsOpen = true;
                    }
                }
                catch
                {
                    // Catch all exception from external filter
                }
            }
        }
    }
}
