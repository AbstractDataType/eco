
Public Class stat
    Dim myPen As New Pen(Color.Black, 0.01)
    Dim g1 As Graphics, g2 As Graphics
    Dim bmp As Bitmap
    Private Sub Scale1(ByRef g As Graphics)
        g.TranslateTransform(10, 450)
        g.ScaleTransform(1, -1)
        g.ScaleTransform(2.2, 1)
    End Sub

    Private Sub stat_Load(sender As Object, e As EventArgs) Handles Me.Load
        bmp = New Bitmap(Me.Width, Me.Height)
        g1 = Graphics.FromImage(bmp)
        g2 = Me.CreateGraphics()
        Scale1(g1)
    End Sub

    Private Sub stat_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        g1.DrawLine(myPen, 0, 0, 500, 0)
        g1.DrawLine(myPen, 0, 0, 0, 500)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        g1.Clear(Color.White)
        g1.DrawLine(myPen, 0, 0, 500, 0)
        g1.DrawLine(myPen, 0, 0, 0, 500)
        Array.Sort(product)
        Dim j As Int16 = 0
        Dim s As Single

        For i = UBound(product) To LBound(product) Step -1
            s += product(i)
            g1.DrawLine(myPen, j * 2, 0, j * 2, product(i))
            j += 1
        Next
        g2.DrawImage(bmp, 0, 0)
        Label1.Text = "人均产出" + Str(s / 99)
    End Sub
End Class