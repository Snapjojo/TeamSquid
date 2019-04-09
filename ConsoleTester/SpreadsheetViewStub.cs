using SpreadsheetGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    public class SpreadsheetViewStub : SpreadsheetView {
        public event Action CloseEvent;
        public event Action NewEvent;
        public event Action<string> SaveEvent;
        public event Action<int, int, string> UpdateEvent;
        public event Action<string> FileChosenEvent;
        public event Action<string> SelectionEvent;

        // These four methods cause events to be fired
        public void FireCloseEvent() {
            if (CloseEvent != null) {
                CloseEvent();
                CalledClosed = true;
            }
        }

        public void FireSelectionChange(int col, int row) {
            if (SelectionEvent != null) {
                SelectionEvent(GetName(col, row));
            }
        }

        public void FireUpdateEvent(int col, int row, string content) {
            if (UpdateEvent != null) {
                UpdateEvent(col, row, content);
            }

        }

        public void DrawCell(int col, int row, string Value) {
            this.Value = Value;
        }

        public string GetName(int col, int row) {
            char alph = (char)(col + 65);
            int num = row + 1;
            String name = alph.ToString() + num.ToString();

            return name;
        }

        public void OpenExisting(string filename) {
            if (FileChosenEvent != null) {
                FileChosenEvent(filename);
            }
        }

        public void Save(string filename) {
            if (SaveEvent != null) {
                SaveEvent(filename);
            }
        }

        public void OpenNew() {
            if (NewEvent != null) {
                NewEvent();
            }
        }

        public void SetChanged(bool isChanged) {
            this.Changed = isChanged;
        }

        public void UpdateContentBox(string content) {
            this.ContentBox = content;
        }

        public void UpdateErrorLabel(bool hasError, string error) {
            if (hasError) {
                this.ErrorLabel = error;
            } else {
                this.ErrorLabel = "";
            }
        }

        public void UpdateNameBox(string name) {
            this.NameBox = name;
        }

        public void UpdateValueBox(string value) {
            this.Value = value;
        }

        public String Value {
            set; get;
        }

        public String ContentBox {
            set; get;
        }

        public String ErrorLabel {
            set; get;
        }

        public String NameBox {
            set; get;
        }

        public bool CalledClosed {
            get; private set;
        }

        public bool Changed {
            get; private set;
        }
    }
}
