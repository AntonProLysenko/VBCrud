﻿Public Class frmMain
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim frmShowAllPassengers As New frmShowAllPassengers
        frmShowAllPassengers.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frmAddNewPassenger As New frmAdd
        frmAddNewPassenger.ShowDialog()
    End Sub

    Private Sub btnUpdata_Click(sender As Object, e As EventArgs) Handles btnUpdata.Click
        Dim frmUpdatePassenger As New frmEdit
        frmUpdatePassenger.ShowDialog()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim frmDeletePassenger As New frmDelete
        frmDelete.ShowDialog()
    End Sub
End Class
