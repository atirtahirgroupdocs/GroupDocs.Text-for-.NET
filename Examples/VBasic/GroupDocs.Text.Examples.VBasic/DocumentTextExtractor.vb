﻿Imports GroupDocs.Text.Extractors.Text
Imports GroupDocs.Text.Formatters.Html
Imports GroupDocs.Text.Formatters.Plain
Imports GroupDocs.Text.Formatters.Markdown
Imports System.IO
Imports System.Text

Public Class DocumentTextExtractor

    Public Class EmailsExtractor


        Public Shared Sub ExtractEmailAttachments(fileName As String)
            'ExStart:ExtractEmailAttachments
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New EmailTextExtractor(filePath)
            Dim factory As New ExtractorFactory()
            For i As Integer = 0 To extractor.AttachmentCount - 1
                Console.WriteLine(extractor.GetContentType(i).Name)
                Dim stream As Stream = extractor.GetStream(i)
                Dim attachmentExtractor As TextExtractor = factory.CreateTextExtractor(filePath)
                Try
                    Console.WriteLine(If(attachmentExtractor Is Nothing, "Document format is not supported", attachmentExtractor.ExtractAll()))
                Finally
                    If attachmentExtractor IsNot Nothing Then
                        attachmentExtractor.Dispose()
                    End If
                End Try
            Next
            'ExEnd:ExtractEmailAttachments
        End Sub

    End Class

    Public Class OneNoteDocument
        ''' <summary>
        ''' Extract text from onenote file/document
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractOneNoteDocument(fileName As String)
            'ExStart:ExtractOneNoteDocument
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            'Set page index
            Dim pageIndex As Integer = 1
            Dim extractor As New NoteTextExtractor(filePath)
            Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractPage(pageIndex), extractor.PageCount)
            'Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractAll(), extractor.PageCount);
            'ExEnd:ExtractOneNoteDocument
        End Sub
    End Class

    Public Class PdfDocument
        ''' <summary>
        ''' Extract text from pdf documents
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractPdfDocument(fileName As String)
            'ExStart:ExtractPdfDocument
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            'Set page index
            Dim pageIndex As Integer = 1
            Dim extractor As New PdfTextExtractor(filePath)
            Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractPage(pageIndex), extractor.PageCount)
            'Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractAll(), extractor.PageCount);
            'ExEnd:ExtractPdfDocument
        End Sub
    End Class

    Public Class PresentationDocument
        ''' <summary>
        ''' Extract text from presentatoin documents
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractPresentationDocument(fileName As String)
            'ExStart:ExtractPresentationDocument
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            'Set slide index
            Dim slideIndex As Integer = 1
            Dim extractor As New SlidesTextExtractor(filePath)
            Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractSlide(slideIndex), extractor.SlideCount)
            'Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractAll(), extractor.SlideCount);
            'ExEnd:ExtractPresentationDocument
        End Sub
    End Class

    Public Class SpreadsheetDocument
        ''' <summary>
        ''' Extract text from spreadsheet documents
        ''' </summary>
        Public Shared Sub ExtractEntireSheet(fileName As String)
            'ExStart:ExtractEntireSheet
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            'Set slide index
            Dim sheetIndex As Integer = 1
            Dim extractor As New CellsTextExtractor(filePath)
            Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractSheet(sheetIndex), extractor.SheetCount)
            'Console.WriteLine("{0} Page Count : {1} ", extractor.ExtractAll(), extractor.SheetCount);
            'ExEnd:ExtractEntireSheet
        End Sub
        ''' <summary>
        ''' Extracting the sheet by the rows
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractSheetByRows(fileName As String)
            'ExStart:ExtractSheetByRows
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New CellsTextExtractor(filePath)
            Dim sheetIndex As Integer = 0
            Dim sheetInfo As CellsSheetInfo = extractor.GetSheetInfo(sheetIndex)
            Console.WriteLine(sheetInfo.ExtractSheetHeader())
            For rowIndex As Integer = 2 To sheetInfo.RowCount - 1
                Console.WriteLine(sheetInfo.ExtractRow(rowIndex))
            Next
            'ExEnd:ExtractSheetByRows
        End Sub
        ''' <summary>
        ''' Extracting the selected columns
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractSelectedColumns(fileName As String)
            'ExStart:ExtractSelectedColumns
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New CellsTextExtractor(filePath)
            Dim sheetIndex As Integer = 0
            Dim sheetInfo As CellsSheetInfo = extractor.GetSheetInfo(sheetIndex)
            Console.WriteLine(sheetInfo.ExtractSheet("B1", "C1"))
            'ExEnd:ExtractSelectedColumns
        End Sub
        ''' <summary>
        ''' Extracting the selected columns from selected rows
        ''' </summary>
        Public Shared Sub ExtractSelectedColumnsAndRows(fileName As String)
            'ExStart:ExtractSelectedColumnsAndRows
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New CellsTextExtractor(filePath)
            Dim sheetIndex As Integer = 0
            Dim sheetInfo As CellsSheetInfo = extractor.GetSheetInfo(sheetIndex)
            Console.WriteLine(sheetInfo.ExtractSheetHeader())
            For rowIndex As Integer = 2 To sheetInfo.RowCount - 1
                Console.WriteLine(sheetInfo.ExtractRow(rowIndex, "B1", "C1"))
            Next
            'ExEnd:ExtractSelectedColumnsAndRows
        End Sub

        ''' <summary>
        ''' Create the concrete extractor by hand
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ConcreteExtractor(fileName As String)
            'ExStart:ConcreteExtractor
            'get file actual path
            Dim filePath As String = Common.getFilePath(fileName)
            Using stream As Stream = File.OpenRead(filePath)
                Using extractor As New CellsTextExtractor(stream)
                    Console.WriteLine(extractor.ExtractAll())
                End Using
            End Using
            'ExEnd:ConcreteExtractor
        End Sub


    End Class

    Public Class TextDocument
        ''' <summary>
        ''' Extract formatted text from word
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractEntireWordPage(fileName As String)
            'ExStart:ExtractEntireWordPage
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim pageIndex As Integer = 0
            Dim extractor As New WordsFormattedTextExtractor(filePath)
            Console.WriteLine(extractor.ExtractPage(pageIndex))
            'ExEnd:ExtractEntireWordPage
        End Sub
        ''' <summary>
        ''' Extract text from word by defining a table format
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub FormattingTable(fileName As String)
            'ExStart:FormattingTable
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New WordsFormattedTextExtractor(filePath)
            Dim frame As New PlainTableFrame(PlainTableFrameAngle.ASCII, PlainTableFrameEdge.ASCII, PlainTableFrameIntersection.ASCII, New PlainTableFrameConfig(True, True, True, False))
            extractor.DocumentFormatter = New PlainDocumentFormatter(frame)
            Console.WriteLine(extractor.ExtractAll())
            'ExEnd:FormattingTable
        End Sub
        ''' <summary>
        ''' Extract text with markdown text format
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub ExtractingWithMarkdown(fileName As String)
            'ExStart:ExtractingWithMarkdown
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New WordsFormattedTextExtractor(filePath)
            extractor.DocumentFormatter = New MarkdownDocumentFormatter()
            Console.WriteLine(extractor.ExtractAll())
            'ExEnd:ExtractingWithMarkdown
        End Sub

        ''' <summary>
        ''' Extract a text with HTML text formatter
        ''' </summary>
        ''' <param name="fileName"></param>
        Public Shared Sub HtmlTextFormating(fileName As String)
            'ExStart:HtmlTextFormating
            'get file actual path
            Dim filePath As [String] = Common.getFilePath(fileName)
            Dim extractor As New WordsFormattedTextExtractor(filePath)
            extractor.DocumentFormatter = New HtmlDocumentFormatter()
            Console.WriteLine(extractor.ExtractAll())
            'ExEnd:HtmlTextFormating
        End Sub
    End Class

    Public Shared Sub PassEncodingToCreatedExtractor(fileName As String)
        'ExStart:PassEncodingToCreatedExtractor
        'get file actual path
        Dim filePath As String = Common.getFilePath(fileName)
        Dim loadOptions As New LoadOptions("text/plain", Encoding.UTF8)
        Dim factory As New ExtractorFactory()
        Using extractor As TextExtractor = factory.CreateTextExtractor(filePath, loadOptions)
            Console.WriteLine(If(extractor IsNot Nothing, extractor.ExtractAll(), "The document format is not supported"))
        End Using
        'ExEnd:PassEncodingToCreatedExtractor
    End Sub

End Class
