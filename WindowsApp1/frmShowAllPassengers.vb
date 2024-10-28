Public Class frmShowAllPassengers
    Private Sub frmShowAllPassengers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader

            If OpenDatabaseConnectionSQLServer() = False Then
                MsgBox(Me, "Database Connection Error!" &
                       vbNewLine & "The Application will be Closed!", vbCritical)
                Close()
            End If




            strSelect = "SELECT * FROM TPassengers
                        JOIN TStates 
                        ON TStates.intStateID = TPassengers.intStateID"


            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            lstResults.Items.Add("Roster Of All Passengers")
            lstResults.Items.Add("=============================")

            While drSourceTable.Read()
                lstResults.Items.Add("  ")

                lstResults.Items.Add("ID: " & vbTab & vbTab & drSourceTable("intPassengerID"))
                lstResults.Items.Add("First Name: " & vbTab & drSourceTable("strFirstName"))
                lstResults.Items.Add("Last Name: " & vbTab & drSourceTable("strLastName"))
                lstResults.Items.Add("Address: " & vbTab & vbTab & drSourceTable("strAddress"))
                lstResults.Items.Add("City: " & vbTab & vbTab & drSourceTable("strCity"))
                lstResults.Items.Add("State: " & vbTab & vbTab & drSourceTable("strState"))
                lstResults.Items.Add("Phone Number: " & vbTab & drSourceTable("strPhoneNumber"))
                lstResults.Items.Add("Email: " & vbTab & vbTab & drSourceTable("strEmail"))

                lstResults.Items.Add("  ")
                lstResults.Items.Add("=============================")
            End While


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