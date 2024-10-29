Module modFormInputValidation
    Public Sub ValidateInput(ByRef blnValidInput As Boolean, ByRef strFirstName As String, ByRef strLastName As String, ByRef strAddress As String, ByRef strCity As String,
                             ByRef strState As String, ByRef intStateID As Integer, ByRef strZip As String, ByRef strPhoneNum As String, ByRef strEmail As String,
                             txtFirstName As Object, txtLastName As Object, txtAddress As Object, txtCity As Object, txtZip As Object, cboStates As Object, txtPhoneNumber As Object, txtEmail As Object)
        Call ValidateName(blnValidInput, strFirstName, strLastName, txtFirstName, txtLastName)

        If blnValidInput Then
            ValidateAddress(blnValidInput, strAddress, strCity, strState, intStateID, strZip, txtAddress, txtCity, txtZip, cboStates)
        End If

        If blnValidInput Then
            ValidateContactInfo(blnValidInput, strPhoneNum, strEmail, txtPhoneNumber, txtEmail)
        End If
    End Sub

    Private Sub ValidateName(ByRef blnValidInput As Boolean, ByRef strFirstName As String, ByRef strLastName As String,
                             txtFirstName As Object, txtLastName As Object)
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

    Private Sub ValidateAddress(ByRef blnValidInput As Boolean, ByRef strAddress As String, ByRef strCity As String, ByRef strState As String, ByRef intStateID As Integer, ByRef strZip As String,
                                txtAddress As Object, txtCity As Object, txtZip As Object, cboStates As Object)
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

    Private Sub ValidateContactInfo(ByRef blnValid As Boolean, ByRef strPhoneNum As String, ByRef strEmail As String,
                                    txtPhoneNumber As Object, txtEmail As Object)
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
End Module
