Public Class ind2
    Private x As Single, y As Single
    Private lim As Int16, cost1 As Int16, product1 As Int16

    Sub New(ByVal x1 As Single, ByVal y1 As Single, c1 As Int16)
        x = x1
        y = y1

        product1 = 10
        cost1 = c1
        lim = 100
    End Sub

    Public Function get_cost()
        Return cost1
    End Function
    Public Function get_x()
        Return x
    End Function
    Public Function get_y()
        Return y
    End Function
    Public Function get_lim()
        Return lim
    End Function
    'Public Function get_product_1y()
    'Return product_1y
    'End Function
    Public Function get_product()
        Return product1
    End Function
    Public Function set_x(ByVal x1)
        If x1 >= 0 Then
            x = x1
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Function set_y(ByVal y1)
        If y1 >= 0 Then
            y = y1
            Return 1
        Else
            Return 0
        End If
    End Function
    Public Function set_lim(ByVal lim1)
        If lim1 >= 0 Then
            lim = lim1
            Return 1
        Else
            Return 0
        End If
    End Function
    'Public Function set_product_1y(ByVal product_1y1)
    'If product_1y1 >= 0 Then
    '       product_1y = product_1y1
    'Return 1
    'Else
    'Return 0
    'End If
    'End Function
    Public Function set_product(ByVal product2)
        If product2 >= 0 Then
            product1 += product2
            Return 1
        Else
            Return 0
        End If
    End Function

    Public Function product(ByRef resource_map(,) As Int16, ByRef dis_map(,) As Int16)
        Dim product_1y As Int16 = 0
        dis_map(x, y) = 0

        Dim a(4, 1) As Int16, best(0, 1) As Int16
        'x   x-1  x+1  x   x
        'y    y    y  y-1  y+1
        a(0, 0) = x
        a(0, 1) = y
        a(1, 0) = x - 1
        a(1, 1) = y
        a(2, 0) = x + 1
        a(2, 1) = y
        a(3, 0) = x
        a(3, 1) = y - 1
        a(4, 0) = x
        a(4, 1) = y + 1

        best = findmax(resource_map, a, dis_map)
        x = best(0, 0)
        y = best(0, 1)
        dis_map(x, y) = 1

        If product_1y + resource_map(x, y) < lim Then
            If resource_map(x, y) > 0 Then
                product_1y += resource_map(x, y)
                resource_map(x, y) *= 0.6
            End If
            If resource_map(x, y) < 0 Then
                resource_map(x, y) = 0
            End If
            Return product_1y
        Else
            If resource_map(x, y) > 0 Then
                product_1y += lim
                resource_map(x, y) -= lim
            End If
            If resource_map(x, y) < 0 Then
                resource_map(x, y) = 0
            End If
            Return lim
        End If
    End Function
    Public Function cost()
        If product1 > 0 Then
            product1 -= cost1
            Return 1
        Else
            Return 0
        End If
    End Function
    Private Function findmax(ByVal resource_map(,) As Int16, ByVal a(,) As Int16, ByVal dis_map(,) As Int16)
        Dim m(0, 1) As Int16
        m(0, 0) = a(0, 0)
        m(0, 1) = a(0, 1)
        For i = 0 To UBound(a)
            If a(i, 0) >= 0 And a(i, 0) <= UBound(dis_map) And a(i, 1) >= 0 And a(i, 1) <= UBound(dis_map) Then  '防止越界
                If resource_map(a(i, 0), a(i, 1)) > resource_map(m(0, 0), m(0, 1)) Then
                    If dis_map(a(i, 0), a(i, 1)) <> 1 Then '防止有人
                        m(0, 0) = a(i, 0)
                        m(0, 1) = a(i, 1)
                    End If
                End If
            End If
        Next
        Return m
    End Function

End Class

