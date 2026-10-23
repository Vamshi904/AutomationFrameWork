
using ClosedXML.Excel;

public static class ExcelHelper
{
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

}




