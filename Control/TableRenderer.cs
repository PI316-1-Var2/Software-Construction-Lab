using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class TableRenderer
    {
        private const string TopLeftJoin = "╔";
        private const string TopRightJoin = "╗";
        private const string BottomLeftJoin = "╚";
        private const string BottomRightJoin = "╝";
        private const string TopJoin = "╤";
        private const string BottomJoin = "╧";
        private const string LeftJoin = "╟";
        private const string RightJoin = "╢";
        private const string CenterJoin = "┼";
        private const char HorizontalSeparator = '─';
        private const string VerticalSeparator = "│";
        private const char HorizontalBoundary = '═';
        private const string VerticalBoundary = "║";

        private string[] _headers;
        private List<string[]> _rows = new List<string[]>();

        public int Padding { get; set; } = 1;
        public bool HeaderTextAlignRight { get; set; }
        public bool RowTextAlignRight { get; set; }

        public void SetHeaders(params string[] headers)
        {
            _headers = headers;
        }

        public void AddRow(params string[] row)
        {
            _rows.Add(row);
        }

        public void ClearRows()
        {
            _rows.Clear();
        }

        private int[] GetMaxCellWidths(List<string[]> table)
        {
            var maximumColumns = 0;
            foreach (var row in table)
            {
                if (row.Length > maximumColumns)
                    maximumColumns = row.Length;
            }

            var maximumCellWidths = new int[maximumColumns];
            for (int i = 0; i < maximumCellWidths.Count(); i++)
                maximumCellWidths[i] = 0;

            var paddingCount = 0;
            if (Padding > 0)
            {
                paddingCount = Padding * 2;
            }

            foreach (var row in table)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    var maxWidth = row[i].Length + paddingCount;

                    if (maxWidth > maximumCellWidths[i])
                        maximumCellWidths[i] = maxWidth;
                }
            }

            return maximumCellWidths;
        }

        private StringBuilder CreateStartingLine(int[] maximumCellWidths, int rowColumnCount, StringBuilder formattedTable)
        {
            for (int i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TopLeftJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary), TopRightJoin));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", TopLeftJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TopJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary), TopRightJoin));
                else
                    formattedTable.Append(string.Format("{0}{1}", TopJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary)));
            }

            return formattedTable;
        }

        private StringBuilder CreateClosingLine(int[] maximumCellWidths, int rowColumnCount, StringBuilder formattedTable)
        {
            for (int i = 0; i < rowColumnCount; i++)
            {
                if (i == 0 && i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", BottomLeftJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary), BottomRightJoin));
                else if (i == 0)
                    formattedTable.Append(string.Format("{0}{1}", BottomLeftJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary)));
                else if (i == rowColumnCount - 1)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", BottomJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary), BottomRightJoin));
                else
                    formattedTable.Append(string.Format("{0}{1}", BottomJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalBoundary)));
            }

            return formattedTable;
        }

        private StringBuilder CreateValueLine(int[] maximumCellWidths, string[] row, bool alignRight, StringBuilder formattedTable)
        {
            int cellIndex = 0;
            int lastCellIndex = row.Length - 1;

            var paddingString = string.Empty;
            if (Padding > 0)
                paddingString = string.Concat(Enumerable.Repeat(' ', Padding));

            foreach (var column in row)
            {
                var restWidth = maximumCellWidths[cellIndex];
                if (Padding > 0)
                    restWidth -= Padding * 2;

                var cellValue = alignRight ? column.PadLeft(restWidth, ' ') : column.PadRight(restWidth, ' ');

                if (cellIndex == 0 && cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", VerticalBoundary, paddingString, cellValue, paddingString, VerticalBoundary));
                else if (cellIndex == 0)
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", VerticalBoundary, paddingString, cellValue, paddingString));
                else if (cellIndex == lastCellIndex)
                    formattedTable.AppendLine(string.Format("{0}{1}{2}{3}{4}", VerticalSeparator, paddingString, cellValue, paddingString, VerticalBoundary));
                else
                    formattedTable.Append(string.Format("{0}{1}{2}{3}", VerticalSeparator, paddingString, cellValue, paddingString));

                cellIndex++;
            }

            return formattedTable;
        }

        private StringBuilder CreateSeperatorLine(int[] maximumCellWidths, int previousRowColumnCount, int rowColumnCount, StringBuilder formattedTable)
        {
            var maximumCells = Math.Max(previousRowColumnCount, rowColumnCount);

            for (int i = 0; i < maximumCells; i++)
            {
                if (i == 0 && i == maximumCells - 1)
                {
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", LeftJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator), RightJoin));
                }
                else if (i == 0)
                {
                    formattedTable.Append(string.Format("{0}{1}", LeftJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator)));
                }
                else if (i == maximumCells - 1)
                {
                    if (i > previousRowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", TopJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator), TopRightJoin));
                    else if (i > rowColumnCount)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", BottomJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator), BottomRightJoin));
                    else if (i > previousRowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", CenterJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator), TopRightJoin));
                    else if (i > rowColumnCount - 1)
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", CenterJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator), BottomRightJoin));
                    else
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", CenterJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator), RightJoin));
                }
                else
                {
                    if (i > previousRowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", TopJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator)));
                    else if (i > rowColumnCount)
                        formattedTable.Append(string.Format("{0}{1}", BottomJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator)));
                    else
                        formattedTable.Append(string.Format("{0}{1}", CenterJoin, string.Empty.PadLeft(maximumCellWidths[i], HorizontalSeparator)));
                }
            }

            return formattedTable;
        }

        public override string ToString()
        {
            var table = new List<string[]>();

            var firstRowIsHeader = false;
            if (_headers?.Any() == true)
            {
                table.Add(_headers);
                firstRowIsHeader = true;
            }

            if (_rows?.Any() == true)
                table.AddRange(_rows);

            if (!table.Any())
                return string.Empty;

            var formattedTable = new StringBuilder();

            var previousRow = table.FirstOrDefault();
            var nextRow = table.FirstOrDefault();

            int[] maximumCellWidths = GetMaxCellWidths(table);

            formattedTable = CreateStartingLine(maximumCellWidths, nextRow.Count(), formattedTable);

            int rowIndex = 0;
            int lastRowIndex = table.Count - 1;

            for (int i = 0; i < table.Count; i++)
            {
                var row = table[i];

                var align = RowTextAlignRight;
                if (i == 0 && firstRowIsHeader)
                    align = HeaderTextAlignRight;

                formattedTable = CreateValueLine(maximumCellWidths, row, align, formattedTable);

                previousRow = row;

                if (rowIndex != lastRowIndex)
                {
                    nextRow = table[rowIndex + 1];
                    formattedTable = CreateSeperatorLine(maximumCellWidths, previousRow.Count(), nextRow.Count(), formattedTable);
                }

                rowIndex++;
            }

            formattedTable = CreateClosingLine(maximumCellWidths, previousRow.Count(), formattedTable);

            return formattedTable.ToString();
        }
    }
}
