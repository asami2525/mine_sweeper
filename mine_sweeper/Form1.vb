Public Class Form1


    Enum Tile_Kind
        Number_0
        Number_1
        Number_2
        Number_3
        Number_4
        Number_5
        Number_6
        Number_7
        Number_8
        Mine
    End Enum
    Private Structure Tile
        Dim button As System.Windows.Forms.Button
        Dim label As System.Windows.Forms.Label
        Dim tile_kind As Tile_Kind
    End Structure
    Dim test_tiles(7, 7) As Tile

    Private Structure position
        Dim x As Integer
        Dim y As Integer
    End Structure
    Dim mine_pos(19) As position

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim FirstLocation As Point
        FirstLocation = New Point(10, 10)

        For i As Integer = 0 To 19
            Dim pos As position
            pos.x = -1
            pos.y = -1
            mine_pos(i) = pos
        Next

        Dim k As Integer = 0
        While k < 20

            Dim x As Integer = Int((7 * Rnd()) + 0)
            Dim y As Integer = Int((7 * Rnd()) + 0)

            Dim _rnd As Random = New Random

            Dim pos As position
            pos.x = _rnd.Next(0, 8) 'Int((7 * Rnd()) + 0)
            pos.y = _rnd.Next(0, 8) 'Int((7 * Rnd()) + 0)

            If Array.IndexOf(mine_pos, pos) = -1 Then
                mine_pos(k) = pos
                k = k + 1
            End If


        End While
        For i As Integer = 0 To 7
            For j As Integer = 0 To 7
                test_tiles(i, j).button = New System.Windows.Forms.Button()
                Dim xPos As Integer
                xPos = FirstLocation.X + 20 * i
                Dim yPos As Integer
                yPos = FirstLocation.Y + 20 * j
                test_tiles(i, j).button.Location = New Point(xPos, yPos)
                test_tiles(i, j).button.Size = New System.Drawing.Size(20, 20)
                AddHandler test_tiles(i, j).button.Click, AddressOf Button_Click
                Me.Controls.Add(test_tiles(i, j).button)

                test_tiles(i, j).label = New System.Windows.Forms.Label()
                test_tiles(i, j).label.Location = New Point(xPos, yPos)
                test_tiles(i, j).label.Size = New System.Drawing.Size(20, 20)
                test_tiles(i, j).label.BackColor = Color.Black
                Dim pos As position
                pos.x = i
                pos.y = j
                If Array.IndexOf(mine_pos, pos) <> -1 Then
                    test_tiles(i, j).label.Text = "M"
                    test_tiles(i, j).label.ForeColor = Color.Red
                    test_tiles(i, j).tile_kind = Tile_Kind.Mine
                End If

                Me.Controls.Add(test_tiles(i, j).label)
            Next
        Next

        For i As Integer = 0 To 7
            For j As Integer = 0 To 7

                Dim count As Integer = 0

                If test_tiles(i, j).tile_kind <> Tile_Kind.Mine Then

                    For z As Integer = -1 To 1
                        For l As Integer = -1 To 1
                            If (i + z) < 8 And (i + z) >= 0 And (j + l) < 8 And (j + l) >= 0 Then
                                Dim pos As position
                                pos.x = i + z
                                pos.y = j + l

                                If test_tiles(pos.x, pos.y).tile_kind = Tile_Kind.Mine Then
                                    count = count + 1
                                End If

                            End If
                        Next
                    Next


                    Select Case count
                        Case Tile_Kind.Number_0
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_0
                            test_tiles(i, j).label.Text = "0"
                            test_tiles(i, j).label.ForeColor = Color.White
                        Case Tile_Kind.Number_1
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_1
                            test_tiles(i, j).label.Text = "1"
                            test_tiles(i, j).label.ForeColor = Color.LightBlue
                        Case Tile_Kind.Number_2
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_2
                            test_tiles(i, j).label.Text = "2"
                            test_tiles(i, j).label.ForeColor = Color.Green
                        Case Tile_Kind.Number_3
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_3
                            test_tiles(i, j).label.Text = "3"
                            test_tiles(i, j).label.ForeColor = Color.Yellow
                        Case Tile_Kind.Number_4
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_4
                            test_tiles(i, j).label.Text = "4"
                            test_tiles(i, j).label.ForeColor = Color.DeepPink
                        Case Tile_Kind.Number_5
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_5
                            test_tiles(i, j).label.Text = "5"
                            test_tiles(i, j).label.ForeColor = Color.Orange
                        Case Tile_Kind.Number_6
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_6
                            test_tiles(i, j).label.Text = "6"
                            test_tiles(i, j).label.ForeColor = Color.Cyan
                        Case Tile_Kind.Number_7
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_7
                            test_tiles(i, j).label.Text = "7"
                            test_tiles(i, j).label.ForeColor = Color.Magenta
                        Case Tile_Kind.Number_8
                            test_tiles(i, j).tile_kind = Tile_Kind.Number_8
                            test_tiles(i, j).label.Text = "8"
                            test_tiles(i, j).label.ForeColor = Color.YellowGreen
                    End Select

                End If

            Next
        Next


    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs)
        Dim button = DirectCast(sender, Button)
        Me.Controls.Remove(button)

        For i As Integer = 0 To 7
            For j As Integer = 0 To 7
                If test_tiles(i, j).button.Equals(button) Then
                    If test_tiles(i, j).tile_kind = Tile_Kind.Mine Then
                        test_tiles(i, j).label.BackColor = Color.Red
                        test_tiles(i, j).label.ForeColor = Color.White
                        For k As Integer = 0 To 7
                            For l As Integer = 0 To 7

                                If test_tiles(k, l).tile_kind = Tile_Kind.Mine Then
                                    Me.Controls.Remove(test_tiles(k, l).button)
                                End If

                                test_tiles(k, l).button.Enabled = False

                            Next
                        Next

                    End If
                    If test_tiles(i, j).tile_kind = Tile_Kind.Number_0 Then
                        Dim pos_list = New List(Of position)()

                    End If

                End If
            Next
        Next

    End Sub

End Class
