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
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub cboPassengers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPassengers.SelectedIndexChanged

        Dim strSelect As String
        Dim cmdSelect As OleDb.OleDbCommand
        Dim drSourceTable As OleDb.OleDbDataReader


        Try
            If OpenDatabaseConnectionSQLServer() = False Then


                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                Me.Close()
            End If

            ' Selectin passenger's info
            strSelect = "SELECT intPassengerID, strFirstName, strLastName, strAddress, strCity, TStates.intStateID, strState, strZip, strPhoneNumber, strEmail
                        FROM TPassengers 
                        Join TStates
                        ON TStates.intStateID = TPassengers.intStateID
                        Where intPassengerID = " & cboPassengers.SelectedValue
            'MessageBox.Show(strSelect)

            ' Retrieve and poulate all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            drSourceTable.Read()



            txtFirstName.Text = drSourceTable("strFirstName")
            txtLastName.Text = drSourceTable("strLastName")
            txtAddress.Text = drSourceTable("strAddress")
            txtCity.Text = drSourceTable("strCity")
            cboStates.SelectedValue = drSourceTable("intStateID")
            txtZip.Text = drSourceTable("strZip")
            txtPhoneNumber.Text = drSourceTable("strPhoneNumber")
            txtEmail.Text = drSourceTable("strEmail")

            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
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

        'Declared With Module Reference for easier find
        Call modFormInputValidation.ValidateInput(blnValidInput, strFirstName, strLastName, strAddress, strCity, strState, intStateID, strZip, strPhoneNum, strEmail,
                                                  txtFirstName, txtLastName, txtAddress, txtCity, txtZip, cboStates, txtPhoneNumber, txtEmail)

        If blnValidInput Then
            UpdateRecord(strFirstName, strLastName, strAddress, strCity, strState, intStateID, strZip, strPhoneNum, strEmail)
        End If

    End Sub

    Private Sub UpdateRecord(strFirstName, strLastName, strAddress, strCity, strState, intStateID, strZip, strPhoneNumber, strEmail)
        Dim strUpdate As String
        Dim cmdUpdate As New OleDb.OleDbCommand
        Dim intRowsAffected As Integer

        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()

            End If




            'update query
            strUpdate = "UPDATE Tpassengers SET " &
                    "strFirstName = '" & strFirstName & "', " &
                    "strLastName = '" & strLastName & "', " &
                    "strAddress = '" & strAddress & "', " &
                    "strCity = '" & strCity & "', " &
                    "intStateID = " & intStateID & ", " &
                    "strZip = '" & strZip & "', " &
                    "strPhoneNumber = '" & strPhoneNumber & "', " &
                    "strEmail = '" & strEmail & "' " &
                    "WHERE intPassengerID = " & cboPassengers.SelectedValue.ToString


            'MessageBox.Show(strUpdate)


            cmdUpdate = New OleDb.OleDbCommand(strUpdate, m_conAdministrator)

            intRowsAffected = cmdUpdate.ExecuteNonQuery()


            If intRowsAffected = 1 Then
                MessageBox.Show("Update successful")
            Else
                MessageBox.Show("Update failed")
            End If

            CloseDatabaseConnection()

            Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class