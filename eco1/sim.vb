Imports ind
Public Class sim
    Public Const border = 99
    Dim arr_ind(border) As ind.ind1
    Dim resource_map(border, border) As Int16
    Dim land_map(border, border) As Int16
    Dim g1 As Graphics, g2 As Graphics
    Dim last(border, border) As Int16
    Dim p As Boolean
    Dim stat As New stat
    Dim Pen1 As New Pen(Color.White)
    Dim rec1 As New Rectangle()
    Dim bmp As Bitmap


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        stat.Show()
        Dim r As System.Random, r2 As System.Random
        r = New Random()
        r2 = New Random()
        For i = 0 To border
            For j = 0 To border
                'Randomize()
                resource_map(i, j) = r.Next(1, 15)
            Next
        Next
        For i = 0 To UBound(arr_ind)
            arr_ind(i) = New ind.ind1(i, i, r2.Next(1, 7))
            land_map(i, i) = 1
        Next
        bmp = New Bitmap(1000, 1000)
        g1 = Graphics.FromImage(bmp)
        g2 = Me.CreateGraphics()
        Scale1(g1)
        'Scale1(g2)
        'printland(g1, resource_map)

        p = False

    End Sub

    Private Sub Scale1(ByRef g As Graphics)
        g.TranslateTransform(0, 460)
        g.ScaleTransform(1, -1)
        g.ScaleTransform(2.2, 2.2)
    End Sub

    Public Sub printind(ByRef g As Graphics, ByVal arr_ind() As ind.ind1, ByVal resource_map(,) As Int16, ByVal land_map(,) As Int16)
        Dim c As Integer
        For k = 0 To UBound(arr_ind)
            rec1.X = last(k, 0) * 2
            rec1.Y = last(k, 1) * 2
            rec1.Height = 1
            rec1.Width = 1
            c = 255 * (1 - resource_map(last(k, 0), last(k, 1)) / 15)
            Pen1.Color = Color.FromArgb(c, c, c)
            g.DrawRectangle(Pen1, rec1)

            rec1.X = arr_ind(k).get_x * 2
            rec1.Y = arr_ind(k).get_y * 2
            rec1.Height = 1
            rec1.Width = 1
            If arr_ind(k).get_product <= 200 Then
                c = 255 * (arr_ind(k).get_product / 200)
                Pen1.Color = Color.FromArgb(c, 0, 0, 255)
                'Pen1.Color = Color.FromArgb(255, 0, 0, 255)
            Else
                Pen1.Color = Color.FromArgb(255, 0, 0, 255)
            End If
            If arr_ind(k).get_product = 0 Then
                Pen1.Color = Color.FromArgb(200, 250, 0, 0)
            End If
            g.DrawRectangle(Pen1, rec1)
        Next
    End Sub

    Public Sub printland(ByRef g As Graphics, ByVal resource_map(,) As Int16)
        For i = 0 To border
            For j = 0 To border
                Dim c As Integer
                c = 255 * (1 - resource_map(i, j) / 15)
                Pen1.Color = Color.FromArgb(c, c, c)
                rec1.X = i * 2
                rec1.Y = j * 2
                rec1.Height = 1
                rec1.Width = 1
                g.DrawRectangle(Pen1, rec1)
                'g2.DrawImage(bmp, 0, 0)
            Next
        Next
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

        printland(g1, resource_map)
        g2.DrawImage(bmp, 0, 0)
        stat.Show()
        Me.Show()
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        For i = 0 To UBound(arr_ind)

            last(i, 0) = arr_ind(i).get_x
            last(i, 1) = arr_ind(i).get_y

            arr_ind(i).move(resource_map, land_map)
            'resource_map(arr_ind(i).get_x, arr_ind(i).get_y) *= 1.5
            'If resource_map(arr_ind(i).get_x, arr_ind(i).get_y) >= 10 Then resource_map(arr_ind(i).get_x, arr_ind(i).get_y) = 10
        Next
        For k = 0 To UBound(arr_ind)
            product(k) = arr_ind(k).get_product()
            'line += Str(arr_ind(k).get_product()) + ","
            'total += arr_ind(k).get_product()
        Next
        printland(g1, resource_map)
        printind(g1, arr_ind, resource_map, land_map)
        g2.DrawImage(bmp, 0, 0)
    End Sub
End Class

