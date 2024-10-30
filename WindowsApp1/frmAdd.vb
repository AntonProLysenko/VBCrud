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

        'Called With Module Reference for easier find
        Call modFormInputValidation.ValidateInput(blnValidInput, strFirstName, strLastName, strAddress, strCity, strState, intStateID, strZip, strPhoneNum, strEmail,
                                                  txtFirstName, txtLastName, txtAddress, txtCity, txtZip, cboStates, txtPhoneNumber, txtEmail)

        If blnValidInput Then
            PushToDB(strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNum, strEmail)
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

            'MessageBox.Show(strInsert)


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