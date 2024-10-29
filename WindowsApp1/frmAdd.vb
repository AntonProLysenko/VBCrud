Public Class frmAdd


    Private Sub frmAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim strSelectStates As String = ""
            Dim cmdSelectStates As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader
            Dim dtState As DataTable = New DataTable



            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()

            End If

            'Selecting States
            strSelectStates = "SELECT * FROM TStates"

            cmdSelectStates = New OleDb.OleDbCommand(strSelectStates, m_conAdministrator)
            drSourceTable = cmdSelectStates.ExecuteReader
            dtState.Load(drSourceTable)

            'Loadingg States results to a combobox
            cboStates.ValueMember = "intStateID"
            cboStates.DisplayMember = "strState"
            cboStates.DataSource = dtState

            drSourceTable.Close()

            CloseDatabaseConnection()

        Catch ex As Exception


            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub btnAddPassenger_Click(sender As Object, e As EventArgs) Handles btnAddPassenger.Click
        Dim blnValidInput As Boolean = False
        Dim strFirstName As String
        Dim strLastName As String
        Dim strAddress As String
        Dim strCity As String
        Dim strState As String
        Dim intStateID As Integer
        Dim strZip As String
        Dim strPhoneNum As String
        Dim strEmail As String

        Call ValidateInput(blnValidInput, strFirstName, strLastName, strAddress, strCity, strState, intStateID, strZip, strPhoneNum, strEmail)

        If blnValidInput Then
            MessageBox.Show("blnValidInput:" & blnValidInput & " strFirstName:" & strFirstName & " strLastName:" & strLastName & " strAddress:" & strAddress & " strCity:" & strCity & " strState:" & strState & " StateID:" & intStateID & " Zip:" & strZip & " PhoneNum:" & strPhoneNum & " Email:" & strEmail)
            PushToDB(strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNum, strEmail)
        End If
    End Sub

    Private Sub ValidateInput(ByRef blnValidInput As Boolean, ByRef strFirstName As String, ByRef strLastName As String, ByRef strAddress As String, ByRef strCity As String,
                              ByRef strState As String, ByRef intStateID As Integer, ByRef strZip As String, ByRef strPhoneNum As String, ByRef strEmail As String)
        Call ValidateName(blnValidInput, strFirstName, strLastName)

        If blnValidInput Then
            ValidateAddress(blnValidInput, strAddress, strCity, strState, intStateID, strZip)
        End If

        If blnValidInput Then
            ValidateContactInfo(blnValidInput, strPhoneNum, strEmail)
        End If
    End Sub

    Private Sub ValidateName(ByRef blnValidInput As Boolean, ByRef strFirstName As String, ByRef strLastName As String)
        Call ValidateString(txtFirstName, strFirstName, blnValidInput, "First Name")

        If blnValidInput Then
            Call ValidateString(txtLastName, strLastName, blnValidInput, "Last Name")
        End If
    End Sub

    Private Sub ValidateString(txtField As Object, ByRef strStringVar As String, ByRef blnValid As Boolean, strVarName As String)
        If txtField.Text = "" Then
            MessageBox.Show("Please, Enter your " & strVarName)
            blnValid = False
            txtField.Focus()
            Exit Sub
        ElseIf ContainsNumber(txtField.Text) Then
            MessageBox.Show("Please, Enter a Valid " & strVarName)
            blnValid = False
            txtField.Focus()
            Exit Sub
        Else
            blnValid = True
            strStringVar = txtField.Text.Trim()
        End If
    End Sub

    Private Function ContainsNumber(str As String)
        Dim arrNums() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}
        Dim blnContainsNumber As Boolean = False


        For index As Integer = 0 To arrNums.Length - 1
            If str.Contains(arrNums(index).ToString()) Then
                blnContainsNumber = True
                Exit For
            End If
        Next

        Return blnContainsNumber
    End Function

    Private Sub ValidateAddress(ByRef blnValidInput As Boolean, ByRef strAddress As String, ByRef strCity As String, ByRef strState As String, ByRef intStateID As Integer, ByRef strZip As String)
        If txtAddress.Text.Length <= 0 Then
            MessageBox.Show("Please, Enter A Valid Address")
            blnValidInput = False
            txtAddress.Focus()
            Exit Sub
        ElseIf Not ContainsNumber(txtAddress.Text) Then
            MessageBox.Show("Please, Enter A Valid Address")
            blnValidInput = False
            txtAddress.Focus()
            Exit Sub
        Else
            blnValidInput = True
            strAddress = txtAddress.Text
        End If

        If blnValidInput Then
            Call ValidateString(txtCity, strCity, blnValidInput, "City")
        End If



        If blnValidInput Then

            If txtZip.Text.Length = 5 Then
                If IsNumeric(txtZip.Text) Then
                    blnValidInput = True
                    strZip = txtZip.Text
                Else
                    MessageBox.Show("Please, Enter A Valid Zip Code")
                    blnValidInput = False
                    txtZip.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Please, Enter A Valid Zip Code")
                blnValidInput = False
                txtZip.Focus()
                Exit Sub
            End If
        End If

        If blnValidInput Then
            Call ValidateString(cboStates, strState, blnValidInput, "State")
        End If

        If blnValidInput Then
            If cboStates.SelectedValue Then

                If cboStates.Items.Contains(cboStates.SelectedItem) Then
                    intStateID = cboStates.SelectedValue
                Else

                End If
            Else
                MessageBox.Show("Please, choose an existing State from the menu")
                blnValidInput = False
                cboStates.Focus()
                Exit Sub
            End If
        End If

    End Sub

    Private Sub ValidateContactInfo(ByRef blnValid As Boolean, ByRef strPhoneNum As String, ByRef strEmail As String)
        If IsNumeric(txtPhoneNumber.Text) Then
            If txtPhoneNumber.Text.Length = 10 Then
                blnValid = True
                strPhoneNum = txtPhoneNumber.Text
            Else
                MessageBox.Show("Please, Enter A Valid Phone Number")
                blnValid = False
                txtPhoneNumber.Focus()
                Exit Sub
            End If
        Else
            MessageBox.Show("Please, Enter A Valid Phone Number")
            blnValid = False
            txtPhoneNumber.Focus()
            Exit Sub
        End If

        If blnValid Then
            If txtEmail.Text = "" Then
                MessageBox.Show("Please, Enter A Valid Email")
                blnValid = False
                txtEmail.Focus()
                Exit Sub
            ElseIf Not txtEmail.Text.Contains("@") Then
                MessageBox.Show("Please, Enter A Valid Email")
                blnValid = False
                txtEmail.Focus()
                Exit Sub
            Else
                strEmail = txtEmail.Text
                blnValid = True

            End If
        End If
    End Sub




    Private Function PushToDB(strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNum, strEmail)
        Try
            Dim intNextPrimaryKey As Integer
            Dim strInsert As String
            Dim cmdInsert As New OleDb.OleDbCommand
            Dim intRowsAffected As Integer


            intNextPrimaryKey = DetectNextPK()



            strInsert = "INSERT INTO TPassengers (intPassengerID, strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNumber, strEmail)" &
                " VALUES (" & intNextPrimaryKey & ", '" & strFirstName & "', '" & strLastName & "', '" & strAddress & "', '" & strCity & "', " & intStateID & ", '" & strZip & "', '" & strPhoneNum & "', '" & strEmail & "')"

            MessageBox.Show(strInsert)


            cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)


            intRowsAffected = cmdInsert.ExecuteNonQuery()


            If intRowsAffected > 0 Then
                MessageBox.Show("Passenger has been added")

            End If


            CloseDatabaseConnection()
            Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Private Function DetectNextPK()
        Dim strSelectNextPK As String
        Dim cmdSelectNextPk As New OleDb.OleDbCommand
        Dim drNextPk As OleDb.OleDbDataReader
        Dim intNextPrimaryKey As Integer


        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show(Me, "Database connection error." & vbNewLine &
                            "The application will now close.",
                            Me.Text + " Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()

        End If

        strSelectNextPK = "SELECT MAX(intPassengerID) + 1 AS intNextPrimaryKey FROM TPassengers"

        cmdSelectNextPk = New OleDb.OleDbCommand(strSelectNextPK, m_conAdministrator)
        drNextPk = cmdSelectNextPk.ExecuteReader

        drNextPk.Read()

        If drNextPk.IsDBNull(0) = True Then
            intNextPrimaryKey = 1
        Else
            intNextPrimaryKey = CInt(drNextPk("intNextPrimaryKey"))
        End If

        Return intNextPrimaryKey
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class