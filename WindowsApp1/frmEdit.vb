Public Class frmEdit
    Private Sub frmEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dtState As DataTable = New DataTable
        Dim dtPassengers As DataTable = New DataTable



        Try
            For Each cntrl As Control In Controls
                If TypeOf cntrl Is TextBox Then
                    cntrl.Text = String.Empty
                End If
            Next


            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()

            End If

            'Selecting States
            strSelect = "SELECT * FROM TStates"

            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtState.Load(drSourceTable)

            'Loadingg States results to a combobox
            cboStates.ValueMember = "intStateID"
            cboStates.DisplayMember = "strState"
            cboStates.DataSource = dtState



            'Selecting Passengers
            strSelect = "SELECT intPassengerID, strFirstName + ' ' + strLastName as PassengerFullName FROM TPassengers"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' load table from data reader
            dtPassengers.Load(drSourceTable)

            cboPassengers.ValueMember = "intStudentID"
            cboPassengers.DisplayMember = "PassengerFullName"
            cboPassengers.DataSource = dtPassengers

            ' Select the first item in the list by default
            If cboPassengers.Items.Count > 0 Then cboPassengers.SelectedIndex = 0




            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

            drSourceTable.Close()

            CloseDatabaseConnection()

        Catch ex As Exception


            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

End Class