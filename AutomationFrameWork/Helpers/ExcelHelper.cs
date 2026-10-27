
using ClosedXML.Excel;

public static class ExcelHelper
{
    public static IReadOnlyDictionary<string, string> ReadRequiredExcelRow(
        string filePath,
        string sheetName,
        IReadOnlyCollection<string> requiredColumns,
        int rowNumber = 2)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(sheetName);
        ArgumentNullException.ThrowIfNull(requiredColumns);
        if (rowNumber < 2)
            throw new ArgumentOutOfRangeException(nameof(rowNumber), "Data rows must follow the header row.");

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Excel test data file was not found.", filePath);

        using var workbook = new XLWorkbook(filePath);
        if (!workbook.TryGetWorksheet(sheetName, out var worksheet))
            throw new InvalidDataException($"Worksheet '{sheetName}' was not found in '{filePath}'.");

        var headers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var cell in worksheet.Row(1).CellsUsed())
        {
            var header = cell.GetString().Trim();
            if (string.IsNullOrWhiteSpace(header))
                continue;

            if (!headers.TryAdd(header, cell.Address.ColumnNumber))
                throw new InvalidDataException($"Duplicate column '{header}' in worksheet '{sheetName}' of '{filePath}'.");
        }

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var column in requiredColumns)
        {
            if (!headers.TryGetValue(column, out var columnNumber))
                throw new InvalidDataException($"Required column '{column}' is missing from worksheet '{sheetName}' of '{filePath}'.");

            var value = worksheet.Cell(rowNumber, columnNumber)
                .GetFormattedString(System.Globalization.CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidDataException($"Required column '{column}' is blank at row {rowNumber} in worksheet '{sheetName}' of '{filePath}'.");

            values.Add(column, value);
        }

        return values;
    }

    public static List<Dictionary<string, string>> ReadExcel(string filePath, string sheetName)
    {
        var rows = new List<Dictionary<string, string>>();

        try
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(sheetName);

            // Normalize headers: trim + lowercase
            var headers = worksheet.Row(1).Cells()
                .Select(c => c.GetString().Trim().ToLower())
                .ToList();

            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Count; i++)
                {
                    rowData[headers[i]] = row.Cell(i + 1).GetString().Trim();
                }
                rows.Add(rowData);
            }

            // Debug: print headers
            Console.WriteLine("Headers found:");
            foreach (var h in headers)
            {
                Console.WriteLine($"'{h}'");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading Excel file: {ex.Message}");
        }

        return rows;
    }
    public static void WriteResults(string filePath, string sheetName, List<Dictionary<string, string>> results)
    {
        XLWorkbook workbook;

        // Step 1: Create or open workbook
        if (File.Exists(filePath))
        {
            workbook = new XLWorkbook(filePath);
        }
        else
        {
            workbook = new XLWorkbook();
        }

        // Step 2: Always create or get the worksheet for this table
        var worksheet = workbook.Worksheets.Contains(sheetName)
            ? workbook.Worksheet(sheetName)
            : workbook.Worksheets.Add(sheetName);

        // Step 3: Write headers only if sheet is empty
        if (worksheet.LastRowUsed() == null)
        {
            var headers = results[0].Keys;
            Console.WriteLine(headers);
            int colIndex = 1;
            foreach (var header in headers)
            {
                worksheet.Cell(1, colIndex).Value = header;
                colIndex++;
            }
        }

        // Step 4: Find next available row
        int rowIndex = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 2;

        // Step 5: Write data rows
        foreach (var result in results)
        {
            int colIndex = 1;
            foreach (var header in results[0].Keys)
            {
                worksheet.Cell(rowIndex, colIndex).Value = result[header];
                colIndex++;
            }
            rowIndex++;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.Rows().AdjustToContents();

        // Step 6: Save workbook
        workbook.SaveAs(filePath);
    }

    public static Dictionary<string, string> ReadExcelRecordByTag(
    string filePath,
    string sheetName,
    string tagNameColumn,
    string tagValue)
    {
        try
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(sheetName);

            // Normalize headers
            var headers = worksheet.Row(1).Cells()
                .Select(c => c.GetString().Trim().ToLower())
                .ToList();

            int tagIndex = headers.IndexOf(tagNameColumn.Trim().ToLower());
            if (tagIndex == -1)
            {
                Console.WriteLine($"Tag column '{tagNameColumn}' not found.");
                return null;
            }

            // Search rows
            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                string cellValue = row.Cell(tagIndex + 1).GetString().Trim();

                if (string.Equals(cellValue, tagValue, StringComparison.OrdinalIgnoreCase))
                {
                    var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                    for (int i = 0; i < headers.Count; i++)
                    {
                        rowData[headers[i]] = row.Cell(i + 1).GetString().Trim();
                    }

                    return rowData; // Return only the matching row
                }
            }

            Console.WriteLine($"No record found for tag '{tagValue}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading Excel file: {ex.Message}");
        }

        return null;
    }

    public static List<Dictionary<string, string>> ReadExcelForExportFuntionality(string filePath)
    {
        using var workbook = new XLWorkbook(filePath);

        // Find sheet that contains the expected header
        var worksheet = workbook.Worksheets
            .First(ws => ws.Row(1).Cells()
                .Any(c => c.GetString().Trim().Contains("id", StringComparison.OrdinalIgnoreCase)));

        var rows = new List<Dictionary<string, string>>();

        var headers = worksheet.Row(1).Cells()
            .Select(c => c.GetString().Trim().ToLower())
            .ToList();

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < headers.Count; i++)
            {
                          
                var cell = row.Cell(i + 1);
                rowData[headers[i]] = NormalizeCellValue(cell);
            }

            rows.Add(rowData);
        }

        Console.WriteLine($"Dynamic sheet detected: {worksheet.Name}");
        return rows;
    }


    public static List<Dictionary<string, string>> ReadExcelForExportFuntionality1(string filePath)
    {
        using var workbook = new XLWorkbook(filePath);

        // Find sheet that contains the expected header
        var worksheet = workbook.Worksheets
            .First(ws => ws.Row(1).Cells()
                .Any(c => c.GetString().Trim().Equals("batch id", StringComparison.OrdinalIgnoreCase)));

        var rows = new List<Dictionary<string, string>>();

        var headers = worksheet.Row(1).Cells()
            .Select(c => c.GetString().Trim().ToLower())
            .ToList();

        // Define headers you want to skip
        var skipHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "submitted date",
        "transfer count",
        "total transfer amount",
        "some other header if needed"
    };

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < headers.Count; i++)
            {
                var header = headers[i];

                // Skip by header name instead of index
                if (skipHeaders.Contains(header))
                    continue;

                rowData[header] = row.Cell(i + 1).GetString().Trim();
            }

            rows.Add(rowData);
        }

        Console.WriteLine($"Dynamic sheet detected: {worksheet.Name}");
        return rows;
    }


    private static string NormalizeCellValue(IXLCell cell)
    {
        // 1. Excel real DateTime
        if (cell.DataType == XLDataType.DateTime)
            return cell.GetDateTime().ToString("MM/dd/yyyy hh:mm tt");

        // 2. Excel numeric but formatted as date
        if (cell.DataType == XLDataType.Number &&
            DateTime.TryParse(cell.GetFormattedString(), out var dt))
            return dt.ToString("MM/dd/yyyy hh:mm tt");

        // 3. Excel numeric (integer or decimal)
        if (cell.DataType == XLDataType.Number)
        {
            double num = cell.GetDouble();

            // If whole number → return integer
            if (num % 1 == 0)
                return ((int)num).ToString();

            // Otherwise return decimal
            return num.ToString("0.##");
        }

        // 4. Text that might be currency ($9,756.00)
        string raw = cell.GetString().Trim();

        // Remove currency symbols and commas
        string cleaned = raw.Replace("$", "").Replace(",", "");

        if (double.TryParse(cleaned, out var numericValue))
        {
            // Normalize to plain number
            if (numericValue % 1 == 0)
                return ((int)numericValue).ToString();

           
        }

        // 5. Text that might be a date
        if (DateTime.TryParse(raw, out var dt2))
            return dt2.ToString("MM/dd/yyyy hh:mm tt");

        // 6. Default: plain string
        return raw;
    }





}




