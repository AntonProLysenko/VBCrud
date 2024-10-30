Public Class frmDelete
    Private Sub frmDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader
        Dim dtPassengers As DataTable = New DataTable



        Try

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()

            End If

            'Selecting Passengers
            strSelect = "SELECT intPassengerID, strFirstName + ' ' + strLastName as PassengerFullName FROM TPassengers"


            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtPassengers.Load(drSourceTable)

            cboPassengers.ValueMember = "intPassengerID"
            cboPassengers.DisplayMember = "PassengerFullName"
            cboPassengers.DataSource = dtPassengers

            ' Selecting firs Passenger by default
            If cboPassengers.Items.Count > 0 Then cboPassengers.SelectedIndex = 0

            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim result As DialogResult
        Dim strDelete As String
        Dim cmbDelete As OleDb.OleDbCommand
        Dim intRowsAffected As Integer
        Try

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                            "The application will now close.",
        Me.Text + " Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()

            End If


            result = MessageBox.Show("Are you sure you want to Delete Passenger: " & cboPassengers.Text & "?", "Confirm Deletion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)


            If result = DialogResult.Cancel Then
                MessageBox.Show("Action Canceled")
                CloseDatabaseConnection()
                Close()
            ElseIf result = DialogResult.No Then
                MessageBox.Show("Action Canceled")
                CloseDatabaseConnection()
                Close()
            ElseIf result = DialogResult.Yes Then

                strDelete = "DELETE FROM TPassengers
                            WHERE intpassengerID = " & cboPassengers.SelectedValue.ToString

                cmbDelete = New OleDb.OleDbCommand(strDelete, m_conAdministrator)
                intRowsAffected = cmbDelete.ExecuteNonQuery()

                If intRowsAffected > 0 Then
                    MessageBox.Show("Deleted Successfuly")
                Else
                    MessageBox.Show("Ooops!" & vbNewLine & "Something Went Wrong!", "Unsuccess!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End If
            CloseDatabaseConnection()
            Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

End Class