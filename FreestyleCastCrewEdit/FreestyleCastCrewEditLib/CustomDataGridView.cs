using System.Windows.Forms;
using System.Drawing;
using System;

namespace DoenaSoft.DVDProfiler.FreestyleCastCrewEdit
{
    public class CustomDataGridView : DataGridView
    {
        //vars for custom column/row drag/drop operations
        private Rectangle DragDropRectangle;
        private int DragDropSourceIndex;
        private int DragDropTargetIndex;
        private int DragDropCurrentIndex = -1;
        private int DragDropType; //0=column, 1=row
        private DataGridViewColumn DragDropColumn;
        private object[] DragDropColumnCellValue;
        private System.Threading.Timer ScrollTimer;

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            base.OnDataError(displayErrorDialogIfNoHandler, e);
        }

        protected override void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //runs when the mouse is clicked over a row header cell
            if(e.RowIndex > -1)
            {
                if(e.Button == MouseButtons.Left)
                {
                    //single-click left mouse button
                    if(this.SelectionMode != DataGridViewSelectionMode.RowHeaderSelect)
                    {
                        this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                        this.Rows[e.RowIndex].Selected = true;
                        this.CurrentCell = this[0, e.RowIndex];
                    } //end if
                }
                else if(e.Button == MouseButtons.Right)
                {
                    //single-click right mouse button
                    if(this.SelectionMode != DataGridViewSelectionMode.RowHeaderSelect)
                    {
                        this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    } //end if 
                } //end if
            } //end if
            base.OnRowHeaderMouseClick(e);
        } //end OnRowHeaderMouseClick

        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //runs when the mouse is clicked over a cell
            if(e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if(e.Button == MouseButtons.Left)
                {
                    //single-click left mouse button
                    if(this.SelectionMode != DataGridViewSelectionMode.CellSelect)
                    {
                        this.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    } //end if
                }
                else if(e.Button == MouseButtons.Right)
                {
                    //single-click right mouse button
                    if(this.SelectionMode != DataGridViewSelectionMode.CellSelect)
                    {
                        this.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    } //end if
                } //end if
            } //end if
            base.OnCellMouseClick(e);
        } //end OnCellMouseClick

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //stores values for drag/drop operations if necessary
            if (this.AllowDrop)
            {
                if (this.HitTest(e.X, e.Y).ColumnIndex == -1 && this.HitTest(e.X, e.Y).RowIndex > -1)
                {
                    //if this is a row header cell
                    if (this.Rows[this.HitTest(e.X, e.Y).RowIndex].Selected)
                    {
                        //if this row is selected
                        DragDropType = 1;
                        Size DragSize = SystemInformation.DragSize;
                        DragDropRectangle = new Rectangle(new Point(e.X - (DragSize.Width / 2), e.Y - (DragSize.Height / 2)), DragSize);
                        DragDropSourceIndex = this.HitTest(e.X, e.Y).RowIndex;
                    }
                    else
                    {
                        DragDropRectangle = Rectangle.Empty;
                    } //end if
                }
                else if (this.HitTest(e.X, e.Y).ColumnIndex > -1 && this.HitTest(e.X, e.Y).RowIndex == -1)
                {
                    //if this is a column header cell
                    if (this.Columns[this.HitTest(e.X, e.Y).ColumnIndex].Selected)
                    {
                        DragDropType = 0;
                        DragDropSourceIndex = this.HitTest(e.X, e.Y).ColumnIndex;
                        Size DragSize = SystemInformation.DragSize;
                        DragDropRectangle = new Rectangle(new Point(e.X - (DragSize.Width / 2), e.Y - (DragSize.Height / 2)), DragSize);
                    }
                    else
                    {
                        DragDropRectangle = Rectangle.Empty;
                    } //end if
                }
                else
                {
                    DragDropRectangle = Rectangle.Empty;
                } //end if
            }
            else
            {
                DragDropRectangle = Rectangle.Empty;
            }//end if

            base.OnMouseDown(e);

        } //end OnMouseDown

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //handles drag/drop operations if necessary
            if (this.AllowDrop)
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (DragDropRectangle != Rectangle.Empty && !DragDropRectangle.Contains(e.X, e.Y))
                    {
                        if (DragDropType == 0)
                        {
                            //column drag/drop
                            DragDropEffects DropEffect = this.DoDragDrop(this.Columns[DragDropSourceIndex], DragDropEffects.Move);
                        }
                        else if (DragDropType == 1)
                        {
                            //row drag/drop
                            DragDropEffects DropEffect = this.DoDragDrop(this.Rows[DragDropSourceIndex], DragDropEffects.Move);
                        } //end if
                    } //end if
                } //end if
            } //end if


            base.OnMouseMove(e);
        } //end OnMouseMove

        protected override void OnDragOver(DragEventArgs e)
        {
            //runs while the drag/drop is in progress
            if(this.AllowDrop)
            {
                e.Effect = DragDropEffects.Move;
                if(DragDropType == 0)
                {
                    //column drag/drop
                    int CurCol = this.HitTest(this.PointToClient(new Point(e.X, e.Y)).X, this.PointToClient(new Point(e.X, e.Y)).Y).ColumnIndex;
                    if(DragDropCurrentIndex != CurCol)
                    {
                        DragDropCurrentIndex = CurCol;
                        this.Invalidate(); //repaint
                    } //end if
                }
                else if(DragDropType == 1)
                {
                    //row drag/drop
                    int CurRow = this.HitTest(this.PointToClient(new Point(e.X, e.Y)).X, this.PointToClient(new Point(e.X, e.Y)).Y).RowIndex;
                    if(DragDropCurrentIndex != CurRow)
                    {
                        DragDropCurrentIndex = CurRow;
                        this.Invalidate(); //repaint
                    } //end if
                } //end if
            } //end if

            Point clientPoint = this.PointToClient(new Point(e.X, e.Y));
            if (ScrollTimer == null)
            {
                if (ShouldScrollUp(clientPoint))
                {
                    ScrollTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerScroll), -1, 0, 250);
                }
                else if (ShouldScrollDown(clientPoint))
                {
                    ScrollTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerScroll), 1, 0, 250);
                }
            }
            else if(!ShouldScrollDown(clientPoint) && !ShouldScrollUp(clientPoint))
            {
                StopAutoScrolling();
            }

            base.OnDragOver(e);
        } //end OnDragOver

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            //runs after a drag/drop operation for column/row has completed
            if(this.AllowDrop)
            {
                if(drgevent.Effect == DragDropEffects.Move)
                {
                    Point ClientPoint = this.PointToClient(new Point(drgevent.X, drgevent.Y));
                    if(DragDropType == 0)
                    {
                        //if this is a column drag/drop operation
                        DragDropTargetIndex = this.HitTest(ClientPoint.X, ClientPoint.Y).ColumnIndex;
                        if(DragDropTargetIndex > -1 && DragDropCurrentIndex < this.ColumnCount - 1)
                        {
                            DragDropCurrentIndex = -1;
                            //holds the appearance of the source column
                            DragDropColumn = this.Columns[DragDropSourceIndex];
                            //holds the values of the cells in the source column
                            DragDropColumnCellValue = new object[this.RowCount - 1];
                            for(int i = 0; i < this.RowCount; i++)
                            {
                                //for each cell in the source column
                                if(this.Rows[i].Cells[DragDropSourceIndex].Value != null)
                                {
                                    //if this cell has a value, store it in the object array
                                    DragDropColumnCellValue[i] = this.Rows[i].Cells[DragDropSourceIndex].Value;
                                } //end if
                            } //next i
                            //remove the source column
                            this.Columns.RemoveAt(DragDropSourceIndex);
                            //insert a new column at the target index using the source column as a template
                            this.Columns.Insert(DragDropTargetIndex, new DataGridViewColumn(DragDropColumn.CellTemplate));
                            //copy the source column's header cell to the new column
                            this.Columns[DragDropTargetIndex].HeaderCell = DragDropColumn.HeaderCell;
                            //select the newly-inserted column
                            this.Columns[DragDropTargetIndex].Selected = true;
                            //update the position of the cuurent cell in the DGV
                            this.CurrentCell = this[DragDropTargetIndex, 0];
                            for(int i = 0; i < this.RowCount; i++)
                            {
                                //for each cell in the new column
                                if(DragDropColumnCellValue[i] != null)
                                {
                                    //set the cell's value equal to that of the corresponding cell in the source column
                                    this.Rows[i].Cells[DragDropTargetIndex].Value = DragDropColumnCellValue[i];
                                } //end if
                            } //next i
                            //release resources
                            DragDropColumnCellValue = null;
                            DragDropColumn = null;
                        } //end if
                    }
                    else if(DragDropType == 1)
                    {
                        //if this is a row drag/drop operation
                        DragDropTargetIndex = this.HitTest(ClientPoint.X, ClientPoint.Y).RowIndex;
                        if(DragDropTargetIndex > -1 && DragDropCurrentIndex < this.RowCount - 1)
                        {

                            DragDropCurrentIndex = -1;
                            DataGridViewRow SourceRow = drgevent.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                            m_IsRowDragDrop = true;
                            this.Rows.RemoveAt(DragDropSourceIndex);
                            this.Rows.Insert(DragDropTargetIndex, SourceRow);
                            m_IsRowDragDrop = false;
                            if(RowDragDrop != null)
                            {
                                RowDragDropEventArgs eventArgs;

                                eventArgs = new RowDragDropEventArgs();
                                eventArgs.SoureRow = DragDropSourceIndex;
                                eventArgs.TargetRow = DragDropTargetIndex;
                                RowDragDrop(this, eventArgs);
                            }
                            this.Rows[DragDropTargetIndex].Selected = true;
                            this.CurrentCell = this[0, DragDropTargetIndex];
                        } //end if
                    } //end if
                } //end if
            } //end if
            base.OnDragDrop(drgevent);
        } //end OnDragDrop

        private Boolean m_IsRowDragDrop;

        public Boolean IsRowDragDrop
        {
            get
            {
                return (m_IsRowDragDrop);
            }
        }

        public class RowDragDropEventArgs : EventArgs
        {
            public Int32 SoureRow;

            public Int32 TargetRow;
        }

        public event EventHandler<RowDragDropEventArgs> RowDragDrop;

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            //draws red drag/drop target indicator lines if necessary
            if (DragDropCurrentIndex > -1)
            {
                if (DragDropType == 0)
                {
                    //column drag/drop
                    if (e.ColumnIndex == DragDropCurrentIndex && DragDropCurrentIndex < this.ColumnCount - 1)
                    {
                        //if this cell is in the same column as the mouse cursor
                        Pen p = new Pen(Color.Red, 1);
                        e.Graphics.DrawLine(p, e.CellBounds.Left - 1, e.CellBounds.Top, e.CellBounds.Left - 1, e.CellBounds.Bottom);
                    } //end if
                }
                else if (DragDropType == 1)
                {
                    //row drag/drop
                    if (e.RowIndex == DragDropCurrentIndex && DragDropCurrentIndex < this.RowCount - 1)
                    {
                        //if this cell is in the same row as the mouse cursor
                        Pen p = new Pen(Color.Red, 1);
                        e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
                    } //end if
                } //end if
            } //end if

     

            base.OnCellPainting(e);
        } //end OnCellPainting

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            StopAutoScrolling();
            base.OnMouseUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.SelectedRows.Count > 0)
                {
                    base.OnKeyDown(e);
                }
                else
                {
                    foreach (DataGridViewCell cell in this.SelectedCells)
                    {
                        DataGridViewTextBoxCell textCell;

                        textCell = cell as DataGridViewTextBoxCell;
                        if (textCell != null)
                        {
                            textCell.Value = String.Empty;
                        }
                    }
                }
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        bool ShouldScrollUp(Point location)
        {
            return location.Y > this.ColumnHeadersHeight
                && location.Y < this.ColumnHeadersHeight + 15
                && location.X >= 0
                && location.X <= this.Bounds.Width;
        }

        bool ShouldScrollDown(Point location)
        {
            return location.Y > this.Bounds.Height - 15
                && location.Y < this.Bounds.Height
                && location.X >= 0
                && location.X <= this.Bounds.Width;
        }

        void StopAutoScrolling()
        {
            if (ScrollTimer != null)
            {
                //
                // disable the timer to scroll the screen
                // 
                ScrollTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                ScrollTimer = null;
            }
        }

        void TimerScroll(object state)
        {
            SetScrollBar((int)state);
        }

        bool scrolling = false;

        void SetScrollBar(int direction)
        {
            if (scrolling)
            {
                return;
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(SetScrollBar), new object[] { direction });
            }
            else
            {
                scrolling = true;

                if (0 < direction)
                {
                    if (this.FirstDisplayedScrollingRowIndex < this.Rows.Count - 1)
                    {
                        this.FirstDisplayedScrollingRowIndex++;
                    }
                }
                else
                {
                    if (this.FirstDisplayedScrollingRowIndex > 0)
                    {
                        this.FirstDisplayedScrollingRowIndex--;
                    }
                }

                scrolling = false;
            }

        }

    } //end class

}
