Module mdlDBUtilities

    Public m_conAdministrator As OleDb.OleDbConnection

    Private m_strDatabaseConnectionStringSQLServer As String = "Provider=SQLOLEDB;" &
                                                            "Server=(Local);" &
                                                            "Database=dbFlyMe2theMoon;" &
                                                            "Connect Timeout=200;" &
                                                            "Integrated Security=SSPI;"

    Public Function OpenDatabaseConnectionSQLServer() As Boolean
        Dim blnResult As Boolean = False

        Try
            m_conAdministrator = New OleDb.OleDbConnection
            m_conAdministrator.ConnectionString = m_strDatabaseConnectionStringSQLServer
            m_conAdministrator.Open()
            'MessageBox.Show(m_conAdministrator.DataSource)

            blnResult = True
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & " Utils, openDb Func", vbCritical)
        End Try

        Return blnResult
    End Function





    Public Function CloseDatabaseConnection() As Boolean

        Dim blnResult As Boolean = False

        Try

            ' Anything there?
            If m_conAdministrator IsNot Nothing Then

                ' Open?
                If m_conAdministrator.State <> ConnectionState.Closed Then

                    ' Yes, close it
                    m_conAdministrator.Close()

                End If

                ' Clean up
                m_conAdministrator = Nothing

            End If

            ' Success
            blnResult = True

        Catch excError As Exception

            ' Log and display error message
            MessageBox.Show(excError.Message)

        End Try

        Return blnResult
    End Function







End Module
